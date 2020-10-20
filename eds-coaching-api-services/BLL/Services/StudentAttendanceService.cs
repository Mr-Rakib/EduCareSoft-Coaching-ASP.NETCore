using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Database.Tables;
using eds_coaching_api_services.Utility.Dependencies;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Services
{
    public class StudentAttendanceService : IAttendance
    {
        private readonly StudentAttendanceRepository AttendanceRepository = new StudentAttendanceRepository();
        private readonly StudentService studentService = new StudentService();
        private readonly TracerService tracerService = new TracerService();

        public string DeleteById(int id, string currentUsername)
        {
            Attendance Attendance = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(Attendance, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (AttendanceRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.StudentAttendance.ToString(), staff.Id, Attendance.Id, GetInstitutionId(Attendance.User_id, currentUsername));
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }

        public List<Attendance> FindAll()
        {
            List<Attendance> Attendances = AttendanceRepository.FindAll();
            return Attendances;
        }

        public List<Attendance> FindAll(string currentUsername)
        {
            List<Attendance> Attendances = FindAll();
            Staff logedinStaff = Authorization.GetCurrentUser(currentUsername);
            if (logedinStaff != null)
            {
                if (Authorization.IsAdmin(logedinStaff))
                {
                    Attendances = Attendances.FindAll(sta => GetInstitutionId(sta.User_id, currentUsername) == logedinStaff.Login.InstitutionProfile_id);
                }
            }
            return Attendances;
        }

        public List<Attendance> FindByDate(DateTime date, string currentUsername)
        {
            List<Attendance> Attendances = FindAll(currentUsername).Where(sta => DateTime.Equals(sta.TimeIn.Date,date.Date)).ToList();
            return Attendances;
        }

        public Attendance FindById(int id, string currentUsername)
        {
            Attendance Attendance = FindAll(currentUsername).Find(d => d.Id == id);
            return Attendance;
        }

        public List<Attendance> FindByUserId(int id, string currentUsername)
        {
            Student student = studentService.FindById(id, currentUsername);
            List<Attendance> Attendances = (student == null)? null : FindAll(currentUsername).FindAll(sta => sta.User_id == id);
            return Attendances;
        }

        public string Save(Attendance Attendance, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticateAttendance(Attendance, staff);
            Attendance.EntryBy_id = staff.Id;

            if (FindById(Attendance.Id, currentUsername) == null)
            {
                if (!IsPresentToday(Attendance, currentUsername))
                {
                    if (String.IsNullOrEmpty(message))
                    {
                        return AttendanceRepository.Save(Attendance) ? null : Messages.issueInDatabase;
                    }
                    else return message;
                }
                else return Messages.AttendanceExist;
            }
            else return Messages.IdExist;
        }

        public string Update(Attendance Attendance, string currentUsername)
        {
            Attendance foundedAttendance = FindById(Attendance.Id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticateAttendance(Attendance, staff);

            Attendance.EntryBy_id = foundedAttendance.EntryBy_id;

            if (foundedAttendance != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (AttendanceRepository.Update(Attendance))
                    {
                        tracerService.Update(DBTables.StudentAttendance.ToString(), staff.Id, Attendance.Id, GetInstitutionId(foundedAttendance.Id, currentUsername));
                        return null;
                    }
                    else return Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.NotFound;
        }

        private string IsAuthonticateAttendance(Attendance attendance, Staff staff)
        {
            string message  = IsAuthonticate(attendance, staff);
            Student student = studentService.FindById(attendance.User_id, staff.Username);
            if (String.IsNullOrEmpty(message))
            {
                if(student != null)
                {
                    return null;
                }
                return Messages.StudentNotExist;
            }
            else return message;
        }

        private string IsAuthonticate(Attendance Attendance, Staff staff)
        {
            if (Attendance != null)
            {
                if (staff != null)
                {
                    return null;
                }
                else return Messages.AccessDenied;
            }
            else return Messages.NotFound;
        }
        private int GetInstitutionId(int id, string username)
        {
            Student student = studentService.FindById(id, username);
            int Institution_id = student == null ? 0 : student.Login.InstitutionProfile_id;
            return Institution_id;
        }
        private bool IsPresentToday(Attendance attendance, string currentUsername)
        {
            Attendance isPresent = FindByUserId(attendance.User_id, currentUsername)
                                    .Find(at => attendance.TimeIn.Date == attendance.TimeIn.Date);
            return (isPresent != null) ? true : false;
        }
    }
}
