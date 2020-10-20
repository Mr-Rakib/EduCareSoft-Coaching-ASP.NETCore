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
    public class ExamInformationController : ControllerBase
    {
        private readonly ExamInformationService ExamInformationService = new ExamInformationService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Get()
        {
            List<ExamInformation> ExamInformations = ExamInformationService.FindAll(User.Identity.Name);
            return Ok(ExamInformations);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            ExamInformation ExamInformation = ExamInformationService.FindById(id, User.Identity.Name);
            if (ExamInformation != null)
            {
                return Ok(ExamInformation);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ExamInformation ExamInformation)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = ExamInformationService.Save(ExamInformation, User.Identity.Name);
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
        public ActionResult Put([FromBody] ExamInformation ExamInformation)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = ExamInformationService.Update(ExamInformation, User.Identity.Name);
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
            errorResponse.Message = ExamInformationService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }
    }
}
