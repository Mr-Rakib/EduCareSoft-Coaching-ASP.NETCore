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
    public class ResultController : ControllerBase
    {
        private readonly ResultService ResultService = new ResultService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Get()
        {
            List<Result> Results = ResultService.FindAll(User.Identity.Name);
            return Ok(Results);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Result Result = ResultService.FindById(id, User.Identity.Name);
            if (Result != null)
            {
                return Ok(Result);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [Authorize(Roles = "Superadmin, Admin, Teacher")]
        [HttpPost]
        public ActionResult Post([FromBody] Result Result)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = ResultService.Save(Result, User.Identity.Name);
                if (errorResponse.Message == null)
                {
                    errorResponse.Message = Messages.Saved;
                    return Created(HttpContext.Request.Scheme, errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [Authorize(Roles = "Superadmin, Admin, Teacher")]
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Result Result)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = ResultService.Update(Result, User.Identity.Name);
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
            errorResponse.Message = ResultService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }

        [HttpGet("GPA/{ExamInformationId}")]
        public ActionResult FindAllStudnentGPA(int ExamInformationId, int StudentId, int Year)
        {
            List<ResultGrade> resultGrades = ResultService.CalculateGPA(ExamInformationId, StudentId, Year, User.Identity.Name);
            return Ok(resultGrades);
        }
    }
}