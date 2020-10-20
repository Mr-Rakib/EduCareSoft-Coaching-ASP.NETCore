using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using eds_coaching_api_services.BLL.Services;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Dependencies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eds_coaching_api_services.Controllers
{
    [Authorize(Roles = "Superadmin")]
    [Route("[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly InstitutionService institutionService = new InstitutionService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult GetAll()
        {
            List<Institution> institutions = institutionService.FindAll();
            return Ok(institutions);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            Institution institution = institutionService.FindById(id);
            return (institution != null) ? Ok(institution) : (ActionResult)NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostById([FromBody] Institution institution)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = institutionService.Save(institution);
                if (String.IsNullOrEmpty(errorResponse.Message))
                {
                    errorResponse.Message = Messages.Saved;
                    return Created(HttpContext.Request.Scheme, errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [HttpPut("{id}")]
        public ActionResult PutById(int id, [FromBody] Institution institution)
        {
            if (ModelState.IsValid && id != 0)
            {
                institution.Id = id;
                errorResponse.Message = institutionService.UpdateById(institution);
                if (String.IsNullOrEmpty(errorResponse.Message))
                {
                    errorResponse.Message = Messages.Updated;
                    return Created(HttpContext.Request.Scheme,errorResponse);
                }
            }
            return BadRequest(errorResponse);
        }

        [HttpPost("Enable/{id}")]
        public ActionResult EnableById(int id)
        {
            errorResponse.Message = institutionService.Enable(id);
            if (String.IsNullOrEmpty(errorResponse.Message))
            {
                errorResponse.Message = Messages.Enable;
                return Ok(errorResponse);
            }
            else return BadRequest(errorResponse);
        }

        [HttpPost("Disable/{id}")]
        public ActionResult DisableById(int id)
        {
            errorResponse.Message = institutionService.Disable(id);
            if (String.IsNullOrEmpty(errorResponse.Message))
            {
                errorResponse.Message = Messages.Disable;
                return Ok(errorResponse);
            }
            else return BadRequest(errorResponse);
        } 
    }
}
