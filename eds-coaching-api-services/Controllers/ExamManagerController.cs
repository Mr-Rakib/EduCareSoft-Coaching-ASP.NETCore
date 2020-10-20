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
    public class ExamManagerController : ControllerBase
    {
        private readonly ExamManagerService ExamManagerService = new ExamManagerService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Get()
        {
            List<ExamManager> ExamManagers = ExamManagerService.FindAll(User.Identity.Name);
            return Ok(ExamManagers);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            ExamManager ExamManager = ExamManagerService.FindById(id, User.Identity.Name);
            if (ExamManager != null)
            {
                return Ok(ExamManager);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ExamManager ExamManager)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = ExamManagerService.Save(ExamManager, User.Identity.Name);
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
        //public ActionResult Put([FromBody] ExamManager ExamManager)
        public ActionResult Put([FromBody] ExamManager ExamManager)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = ExamManagerService.Update(ExamManager, User.Identity.Name);
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
            errorResponse.Message = ExamManagerService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }
    }
}
