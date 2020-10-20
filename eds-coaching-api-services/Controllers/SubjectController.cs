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
    [Authorize(Roles ="Admin, Superadmin")]
    [Route("[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly SubjectService SubjectService = new SubjectService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Get()
        {
            List<Subject> Subjects = SubjectService.FindAll(User.Identity.Name);
            return Ok(Subjects);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Subject Subject = SubjectService.FindById(id, User.Identity.Name);
            if (Subject != null)
            {
                return Ok(Subject);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Subject Subject)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = SubjectService.Save(Subject, User.Identity.Name);
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
        public ActionResult Put([FromBody] Subject Subject)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = SubjectService.Update(Subject, User.Identity.Name);
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
            errorResponse.Message = SubjectService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }
    }
}
