using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Database.Tables;
using eds_coaching_api_services.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Services
{
    public class StaffAttendanceService: IAttendance
    {
        private readonly StaffAttendanceRepository AttendanceRepository = new StaffAttendanceRepository();
        private readonly StaffService StaffService = new StaffService();
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
                    tracerService.Delete(DBTables.StaffAttendance.ToString(), staff.Id, Attendance.Id, GetInstitutionId(Attendance.User_id, currentUsername));
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
            List<Attendance> Attendances = FindAll(currentUsername).Where(sta => DateTime.Equals(sta.TimeIn.Date, date.Date)).ToList();
            return Attendances;
        }

        public Attendance FindById(int id, string currentUsername)
        {
            Attendance Attendance = FindAll(currentUsername).Find(d => d.Id == id);
            return Attendance;
        }

        public List<Attendance> FindByUserId(int id, string currentUsername)
        {
            Staff Staff = StaffService.FindById(id, currentUsername);
            List<Attendance> Attendances = (Staff == null) ? null : FindAll(currentUsername).FindAll(sta => sta.User_id == id);
            return Attendances;
        }

        public string Save(Attendance Attendance, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            Attendance foundedAttendance = FindById(Attendance.Id, currentUsername);
            string message = IsAuthonticateAttendance(Attendance, staff);
            
            Attendance.EntryBy_id = staff.Id;

            if (foundedAttendance == null)
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
                        tracerService.Update(DBTables.StaffAttendance.ToString(), staff.Id, Attendance.Id, GetInstitutionId(foundedAttendance.Id, currentUsername));
                        return null;
                    }
                    else return Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.NotFound;
        }

        //---------------------------PRIVATE METHODS-----------------------------//
        private string IsAuthonticateAttendance(Attendance attendance, Staff staff)
        {
            string message = IsAuthonticate(attendance, staff);
            Staff Staff = StaffService.FindById(attendance.User_id, staff.Username);
            if (String.IsNullOrEmpty(message))
            {
                if (Staff != null)
                {
                    return null;
                }
                return Messages.StaffNotExist;
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
            Staff Staff = StaffService.FindById(id, username);
            int Institution_id = Staff == null ? 0 : Staff.Login.InstitutionProfile_id;
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
