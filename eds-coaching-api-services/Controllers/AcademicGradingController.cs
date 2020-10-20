using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eds_coaching_api_services.BLL.Services;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Dependencies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eds_coaching_api_services.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AcademicGradingController : ControllerBase
    {
        private readonly AcademicGradingService AcademicGradingService = new AcademicGradingService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Get()
        {
            List<AcademicGrading> AcademicGradings = AcademicGradingService.FindAll(User.Identity.Name);
            return Ok(AcademicGradings);
        }

        [HttpGet("gradingsystem")]
        public ActionResult GetByGradingSystem(string SystemName)
        {
            List<AcademicGrading> AcademicGradings = AcademicGradingService.FindByGradingSystemName(SystemName, User.Identity.Name);
            return Ok(AcademicGradings);
        }

        [Authorize(Roles = "Admin, Superadmin")]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            AcademicGrading AcademicGrading = AcademicGradingService.FindById(id, User.Identity.Name);
            if (AcademicGrading != null)
            {
                return Ok(AcademicGrading);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [Authorize(Roles = "Admin, Superadmin")]
        [HttpPost]
        public ActionResult Post([FromBody] AcademicGrading AcademicGrading)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = AcademicGradingService.Save(AcademicGrading, User.Identity.Name);
                if (errorResponse.Message == null)
                {
                    errorResponse.Message = Messages.Saved;
                    return Created(HttpContext.Request.Scheme, errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [Authorize(Roles = "Admin, Superadmin")]
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] AcademicGrading AcademicGrading)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = AcademicGradingService.Update(AcademicGrading, User.Identity.Name);
                if (errorResponse.Message == null)
                {
                    errorResponse.Message = Messages.Updated;
                    return Ok(errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [Authorize(Roles = "Admin, Superadmin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            errorResponse.Message = AcademicGradingService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }
    }
}