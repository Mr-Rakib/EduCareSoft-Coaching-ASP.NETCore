using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Services
{
    public class InstitutionService : IInstitution
    {
        private readonly InstitutionRepository institutionRepository = new InstitutionRepository();
        
        public List<Institution> FindAll()
        {
            List<Institution> institutions = institutionRepository.FindAll();
            return institutions;
        }

        public Institution FindById(int id)
        {
            Institution institution = new Institution();
            institution = FindAll().Find(ins => ins.Id == id);
            return institution;
        }

        public string Save(Institution institution)
        {
            if (FindById(institution.Id) == null)
            {
                if (FindByEmail(institution.ContactInformation.Email) == null)
                {
                    institution.RegistrationDate = DateTime.Now;
                    return (institutionRepository.Save(institution)) ?
                        null : Messages.issueInDatabase;
                }
                else return Messages.emailExist;
            }
            else return Messages.Exist;
        }

        public Institution FindByEmail(string email)
        {
            Institution institution = new Institution();
            institution = FindAll().Find(ins => ins.ContactInformation.Email == email);
            return institution;
        }

        public string UpdateById(Institution institution)
        {
            Institution findByInstitution =  FindById(institution.Id);
            if (findByInstitution != null)
            {
                //set contact information id
                institution.ContactInformation.Id = findByInstitution.ContactInformation.Id;
                //find email is unique or not 
                if (IsUniqueEmail(institution))
                {
                    return (institutionRepository.Update(institution)) ?
                        null : Messages.issueInDatabase;
                }
                else return Messages.emailExist;
            }
            else return Messages.Exist;
        }

        public string Disable(int id)
        {
            Institution institution = FindById(id);
            if (institution != null)
            {
                institution.IsActive = Status.Disable;
                return UpdateById(institution);
            }
            else return Messages.NotFound;
        }

        public string Enable(int id)
        {
            Institution institution = FindById(id);
            if (institution != null)
            {
                institution.IsActive = Status.Enable;
                return UpdateById(institution);
            }
            else return Messages.NotFound;
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }
       
        private bool IsUniqueEmail(Institution institution)
        {
            Institution existWithEmail = new Institution();
            existWithEmail = FindExcludeId(institution.Id).
                                Find(user => user.ContactInformation.Email == institution.ContactInformation.Email );
            return (existWithEmail == null) ? true : false ;
        }

        private List<Institution> FindExcludeId(int id)
        {
            List<Institution> excludeId = FindAll().FindAll(user => user.Id != id);
            return excludeId;
        }
    }
}
