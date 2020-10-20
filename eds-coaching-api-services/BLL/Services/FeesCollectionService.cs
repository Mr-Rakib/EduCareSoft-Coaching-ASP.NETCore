using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Utility.Dependencies;
using eds_coaching_api_services.Utility.Database.Tables;

namespace eds_coaching_api_services.BLL.Services
{
    public class FeesCollectionService : IFeesCollection
    {
        private readonly TracerService tracerService = new TracerService();
        private readonly StudentService StudentService = new StudentService();
        private readonly StudentAdmissionService studentAdmissionService = new StudentAdmissionService();
        private readonly FeesCollectionRepository FeesCollectionRepository = new FeesCollectionRepository();

        public string GenerateFeesForStudent(string month, int year, int institutionId, string currentUsername)
        {
            List<StudentAdmission> studentAdmissionList = studentAdmissionService.FindAll(currentUsername);
            List<FeesCollection> FeesCollectionList = new List<FeesCollection>();
            string message = null;

            Login LoginStaff = Authorization.GetCurrentLoginUser(currentUsername);
            //Get ALL Active Students
            studentAdmissionList = studentAdmissionList.Where(stad => StudentService.FindById(stad.Student_id, currentUsername).EntryInformation.IsActive == Status.Enable).ToList();
            month = (String.IsNullOrEmpty(month)) ? DateTime.Now.ToString("MMMMM") : month;
            year            = (year <= 0 ) ? DateTime.Now.Year : year;
            // get All Active Student
            if (Authorization.IsSuperAdmin(LoginStaff.Username))
            {
                if (institutionId != 0)
                {
                    studentAdmissionList = studentAdmissionService.FindByInstitutionId(institutionId, currentUsername);
                }
                else return Messages.InstitutionNotExist;
            }
            //Superadmin Have to select the institution
            if (studentAdmissionList.Count > 0)
            {
                studentAdmissionList = studentAdmissionList.FindAll(std => std.AdmissionDate <= DateTime.Now);

                foreach (var feesCollection in studentAdmissionList)
                {
                    FeesCollection GeneratedFeesCollection = new FeesCollection()
                    {
                        StudentId = feesCollection.Student_id,
                        Month = month,
                        Year = year
                    };

                    Save(GeneratedFeesCollection, currentUsername);
                }
            }
            else message = Messages.Empty;
            return message;
        }
        
        public string DeleteById(int id, string currentUsername)
        {
            FeesCollection FeesCollection = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(FeesCollection, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (FeesCollectionRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.FeesCollection.ToString(), staff.Id, FeesCollection.Id, Authorization.GetInstitution(FeesCollection.StudentId, currentUsername));
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }
        
        public List<FeesCollection> FindAll(string currentUsername)
        {
            List<FeesCollection> FeesCollections = FeesCollectionRepository.FindAll();
            Login login = Authorization.GetCurrentLoginUser(currentUsername);

            if (login != null)
            {
                if (Authorization.IsAdmin(currentUsername))
                {
                    FeesCollections = FeesCollections
                                      .FindAll(fsc => Authorization.GetInstitution(fsc.StudentId, currentUsername) 
                                      == login.InstitutionProfile_id);
                }

                else if (Authorization.IsStudent(currentUsername))
                {
                    Student student = Authorization.GetCurrentStudent(currentUsername);
                    FeesCollections = FeesCollections.FindAll(fc => fc.StudentId == student.Id);
                }
            }
            return FeesCollections;
        }
        
        public FeesCollection FindById(int id, string currentUsername)
        {
            FeesCollection FeesCollection = FindAll(currentUsername).Find(d => d.Id == id);
            return FeesCollection;
        }

        public string Save(FeesCollection FeesCollection, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticateSaveFeesCollection(FeesCollection, staff);
            

            if (FindById(FeesCollection.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    SetSaveInformation(FeesCollection,staff);
                    return FeesCollectionRepository.Save(FeesCollection) ? null : Messages.issueInDatabase; 
                }
                else return message;
            }
            else return Messages.IdExist;
        }

        public List<FeesCollection> FindAllDiscount(string username, string month, int year)
        {
            List<FeesCollection> feesCollections = FindYearAndMonth(username, year, month);
            
            feesCollections = FindAll(username).Where(st => st.Discount > 0).ToList();
            return feesCollections;
        }

        public float FindTotalMonthlyFeesOfStudent(FeesCollection FeesCollection, string username)
        {
            float fees = 0;
            var admission = studentAdmissionService.FindByStudentId(FeesCollection.StudentId, username);
            DateTime FeesCollectionDate = DateTime.Parse(string.Concat($"{Generator.GetMonth(FeesCollection.Month)}, 1,  {FeesCollection.Year}"));
            foreach(var item in  admission)
            {
                if(item.AdmissionDate <= FeesCollectionDate)
                {
                    fees += item.MonthlyFees;
                }
            }
            return fees;
        }
        
        public string Update(FeesCollection FeesCollection, string currentUsername)
        {
            FeesCollection foundedFeesCollection = FindById(FeesCollection.Id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            SetUpdateInformation(foundedFeesCollection, FeesCollection);
            string message = IsAuthonticateUpdateFeesCollection(FeesCollection, staff);

            if (foundedFeesCollection != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (FeesCollectionRepository.Update(FeesCollection))
                    {
                        tracerService.Update(DBTables.FeesCollection.ToString(), staff.Id, FeesCollection.Id, Authorization.GetInstitution(FeesCollection.StudentId, currentUsername));
                        return null;
                    }
                    else return Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.NotFound;
        }

        public List<FeesCollection> FindAllPaidStudent(string username, string month, int year)
        {
            List<FeesCollection> feesCollections = FindYearAndMonth(username, year, month);

            feesCollections = feesCollections.FindAll(st => st.Status == Status.Paid);
            return feesCollections;
        }
        
        public List<FeesCollection> FindAllUnpaidStudent(string username, string month, int year)
        {
            List<FeesCollection> feesCollections = FindYearAndMonth(username, year, month);

            feesCollections = feesCollections.FindAll(st => st.Status == Status.Unpaid);
            return feesCollections;
        }

        public List<FeesCollection> FindAllHistory(string month, int year, string currentUsername)
        {
            List<FeesCollection> feesCollections = FindYearAndMonth(currentUsername, year, month);

            return feesCollections;
        }

        public List<FeesCollection> FindFeesCollectedByStaffId(int id, string currentUsername, string month, int year)
        {
            year = year <= 0 ? DateTime.Now.Date.Year : year;

            List<FeesCollection> feesCollections = FindYearAndMonth(currentUsername, year, month).FindAll(fsc => fsc.EntryById == id);
            feesCollections = (String.IsNullOrEmpty(month)) ? feesCollections : feesCollections.FindAll(fsc => fsc.Month.ToLower() == month.ToLower());

            return feesCollections;
        }
        
        public List<FeesCollection> FindMonthlyFeesByStudentId(int id, string currentUsername, string month, int year)
        {
            year = year <= 0 ? DateTime.Now.Date.Year : year;

            List<FeesCollection> feesCollection = FindYearAndMonth(currentUsername, year, month).FindAll(fsc => fsc.StudentId == id);
            feesCollection = (String.IsNullOrEmpty(month)) ? feesCollection : feesCollection.FindAll(fsc => fsc.Month.ToLower() == month.ToLower());

            return feesCollection;
        }

        //Private Methods
        private string IsAuthonticate(FeesCollection FeesCollection, Staff staff)
        {
            if (FeesCollection != null)
            {
                if (staff != null)
                {
                    return null;
                }
                else return Messages.AccessDenied;
            }
            else return Messages.invalidField;
        }
        
        private void SetSaveInformation(FeesCollection FeesCollection, Staff staff)
        {
            FeesCollection.InvoiceNumber = Generator.InvoiceNumber();
            FeesCollection.EntryById = staff.Id;
        }

        private List<FeesCollection> FindYearAndMonth(string username, int year, string month)
        {
            List<FeesCollection> feesCollections = FindAll(username);
            feesCollections = (year <= 0) ? feesCollections : feesCollections.Where(fc => fc.Date.Date.Year == year).ToList();
            feesCollections = (String.IsNullOrEmpty(month)) ? feesCollections : feesCollections.Where(fc => fc.Month.ToLower() == month.ToLower()).ToList();

            return feesCollections;
        }
       
        private string IsAuthonticateFeesCollection(FeesCollection FeesCollection, Staff Staff)
        {
            string message  = IsAuthonticate(FeesCollection, Staff);
            float paid      = (FeesCollection.Fees + FeesCollection.Discount);
            if (String.IsNullOrEmpty(message))
            {
                if (StudentService.FindById(FeesCollection.StudentId, Staff.Username) != null)
                {
                    float feestoPay = FindTotalMonthlyFeesOfStudent(FeesCollection, Staff.Username);
                    if (feestoPay > 0)
                    {
                        FeesCollection.Remain = feestoPay - paid;
                        FeesCollection.Status = (feestoPay == paid) ? Status.Paid : Status.Unpaid;
                        FeesCollection.Date = DateTime.Now;
                        return null;
                    }
                    else return Messages.InvalidInformation;
                }
                else return Messages.StudentNotExist;
            }
            else return message;
        }
       
        private string IsAuthonticateSaveFeesCollection(FeesCollection feesCollection, Staff staff)
        {
            string message = IsAuthonticateFeesCollection(feesCollection, staff);
            List<FeesCollection> FeesCollectionOfThisStudent = FindAll(staff.Username).FindAll(st => st.StudentId == feesCollection.StudentId && st.Month == feesCollection.Month && st.Year == feesCollection.Year);
            if(String.IsNullOrEmpty(message))
            {
                if (Authorization.IsActiveStudent(feesCollection.StudentId, staff.Username))
                {
                    if (FeesCollectionOfThisStudent.Count == 0)
                    {
                        return null;
                    }
                    else return Messages.DuplicateFeesCollection;
                }
                else return Messages.NotActive;
            }
            return message;
        }

        private string IsAuthonticateUpdateFeesCollection(FeesCollection feesCollection, Staff staff)
        {
            string message = IsAuthonticateFeesCollection(feesCollection, staff);
            List<FeesCollection> FeesCollectionOfThisStudent = FindAll(staff.Username).FindAll(st => st.StudentId == feesCollection.StudentId && st.Month == feesCollection.Month && st.Year == feesCollection.Year);
            FeesCollectionOfThisStudent = FeesCollectionOfThisStudent.Where(st => st.Id != feesCollection.Id).ToList();
            if (String.IsNullOrEmpty(message))
            {
                if (FeesCollectionOfThisStudent.Count == 0)
                {
                    return null;
                }
                else return Messages.DuplicateFeesCollection;
            }
            return message;
        }
        
        private void SetUpdateInformation(FeesCollection foundedFeesCollection, FeesCollection FeesCollection)
        {
            FeesCollection.Date = foundedFeesCollection.Date;
            FeesCollection.StudentId = foundedFeesCollection.StudentId;
            FeesCollection.EntryById = foundedFeesCollection.EntryById;
            FeesCollection.InvoiceNumber = foundedFeesCollection.InvoiceNumber;
        }

    }
}
