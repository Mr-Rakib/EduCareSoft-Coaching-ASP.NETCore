using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eds_coaching_api_services.BLL.Services;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Dependencies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eds_coaching_api_services.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentAdmissionController : ControllerBase
    {
        private readonly StudentAdmissionService StudentAdmissionService = new StudentAdmissionService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Get()
        {
            List<StudentAdmission> StudentAdmissions = StudentAdmissionService.FindAll(User.Identity.Name);
            return Ok(StudentAdmissions);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            StudentAdmission StudentAdmission = StudentAdmissionService.FindById(id, User.Identity.Name);
            if (StudentAdmission != null)
            {
                return Ok(StudentAdmission);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpGet("student/{id}")]
        public ActionResult GetByStudentId(int id)
        {
            List<StudentAdmission> StudentAdmissions = StudentAdmissionService.FindByStudentId(id, User.Identity.Name);
            if (StudentAdmissions != null)
            {
                return Ok(StudentAdmissions);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpGet("institution/{id}")]
        public ActionResult GetByInstitutionId(int id)
        {
            List<StudentAdmission> StudentAdmissions = StudentAdmissionService.FindByInstitutionId(id, User.Identity.Name);
            if (StudentAdmissions != null)
            {
                return Ok(StudentAdmissions);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }


        [HttpPost]
        public ActionResult Post([FromBody] StudentAdmission StudentAdmission)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = StudentAdmissionService.Save(StudentAdmission, User.Identity.Name);
                if (errorResponse.Message == null)
                {
                    errorResponse.Message = Messages.Saved;
                    return Created(HttpContext.Request.Scheme, errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] StudentAdmission StudentAdmission)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = StudentAdmissionService.Update(StudentAdmission, User.Identity.Name);
                if (errorResponse.Message == null)
                {
                    errorResponse.Message = Messages.Updated;
                    return Ok(errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            errorResponse.Message = StudentAdmissionService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }
    }
}
