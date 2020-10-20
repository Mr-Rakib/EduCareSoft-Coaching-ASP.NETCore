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
    public class SubjectManagerController : ControllerBase
    {
        private readonly SubjectManagerService SubjectManagerService = new SubjectManagerService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Get()
        {
            List<SubjectManager> SubjectManagers = SubjectManagerService.FindAll(User.Identity.Name);
            return Ok(SubjectManagers);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            SubjectManager SubjectManager = SubjectManagerService.FindById(id, User.Identity.Name);
            if (SubjectManager != null)
            {
                return Ok(SubjectManager);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SubjectManager SubjectManager)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = SubjectManagerService.Save(SubjectManager, User.Identity.Name);
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
        public ActionResult Put([FromBody] SubjectManager SubjectManager)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = SubjectManagerService.Update(SubjectManager, User.Identity.Name);
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
            errorResponse.Message = SubjectManagerService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }
    }
}
