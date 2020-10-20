using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eds_coaching_api_services.BLL.Services;
using eds_coaching_api_services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eds_coaching_api_services.Controllers
{
    [Authorize(Roles ="Admin, Superadmin")]
    [Route("[controller]")]
    [ApiController]
    public class TracerController : ControllerBase
    {
        private readonly TracerService TracerService = new TracerService();

        [HttpGet]
        public ActionResult Get()
        {
            List<Tracer> Tracers = TracerService.FindAll(User.Identity.Name);
            return Ok(Tracers);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            List<Tracer> TracersById = TracerService.FindById(id, User.Identity.Name);
            return Ok(TracersById);
        }

        [HttpGet("user/{id}")]
        public ActionResult GetByUser(int id)
        {
            List<Tracer> TracersById = TracerService.FindByActorID(id, User.Identity.Name);
            return Ok(TracersById);
        }

        [Authorize(Roles = "Superadmin")]
        [HttpGet("Institution/{id}")]
        public ActionResult GetByInstitution(int id, int userId)
        {
            List<Tracer> TracersByInstitution = TracerService.FindByInstitution(id, userId, User.Identity.Name);
            return Ok(TracersByInstitution);
        }

    }
}
