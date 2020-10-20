using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Services
{
    public class TracerService : ITracer
    {
        public readonly TracerRepository tracerRepository = new TracerRepository();
        public readonly LoginService loginService = new LoginService();
        public readonly StaffService staffService = new StaffService();

        public List<Tracer> FindAll(string currentUsername)
        {
            List<Tracer> tracers = tracerRepository.FindAll();
            Staff currentStaff = staffService.FindByUsername(currentUsername, currentUsername);
            if (currentStaff != null) 
            {
                tracers = tracerRepository.FindAll();
                if (currentStaff.Login.UserRole.ToLower() == Roles.Admin.ToString().ToLower())
                {
                    tracers = tracers.FindAll(st => st.Institution_id == currentStaff.Login.InstitutionProfile_id);
                }
            }
            return tracers;
        }

        internal List<Tracer> FindById(int id, string currentUsername)
        {
            List<Tracer> tracers = FindAll(currentUsername).FindAll(tr => tr.Id == id);
            return tracers;
        }

        public List<Tracer> FindByActorID(int id, string currentUsername)
        {
            List<Tracer> tracers = FindAll(currentUsername).FindAll(tr => tr.Actor_id == id);
            return tracers;
        }

        public bool Delete(string table, int staffID, int id , int institutionID )
        {
            Tracer tracer = new Tracer
            {
                Actor_id            = staffID,
                ActionApplied_id    = id,
                ActionName          = "Delete",
                TableName           = table,
                ActionTime          = DateTime.Now,
                Institution_id      = institutionID
            };

            return tracerRepository.Save(tracer);
        }

        public bool Update(string table, int staffID, int id, int institutionID)
        {
            Tracer tracer = new Tracer
            {
                Actor_id            = staffID,
                ActionApplied_id    = id,
                ActionName          = "Update",
                TableName           = table,
                ActionTime          = DateTime.Now,
                Institution_id      = institutionID
            };

            return tracerRepository.Save(tracer);
        }

        public List<Tracer> FindByInstitution(int id, int userId, string currentUsername)
        {
            List<Tracer> tracers = FindAll(currentUsername).FindAll(tr => tr.Institution_id == id);
            tracers = userId != 0 ? tracers.FindAll(tr => tr.Actor_id == userId) : tracers;
            return tracers;
        }

    }


}
