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
    [Authorize(Roles ="Superadmin, Admin")]
    [Route("[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly DesignationService DesignationService = new DesignationService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Get()
        {
            List<Designation> Designations = DesignationService.FindAll(User.Identity.Name);
            return Ok(Designations);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Designation Designation = DesignationService.FindById(id, User.Identity.Name);
            if (Designation != null)
            {
                return Ok(Designation);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Designation Designation)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = DesignationService.Save(Designation, User.Identity.Name);
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
        public ActionResult Put([FromBody] Designation Designation)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = DesignationService.Update(Designation, User.Identity.Name);
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
            errorResponse.Message = DesignationService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }
    }
}
