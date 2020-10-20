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
    [Authorize (Roles ="Superadmin, Admin")]
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService studentService = new StudentService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Students()
        {
            List<Student> students = studentService.FindAll(GetLoggedinUserId());
            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Student student = studentService.FindById(id, GetLoggedinUserId());
            if (student != null)
            {
                return Ok(student);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Student student)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = studentService.Save(student, GetLoggedinUserId());
                if (errorResponse.Message == null)
                {
                    return Created(HttpContext.Request.Scheme, student);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Student student)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = studentService.Update(student, GetLoggedinUserId());
                if (errorResponse.Message == null)
                {
                    return Ok(student);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        
        [HttpPost("Enable/{id}")]
        public ActionResult EnableById(int id)
        {
            errorResponse.Message = studentService.Enable(id, GetLoggedinUserId());
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
            errorResponse.Message = studentService.Disable(id, GetLoggedinUserId());
            if (String.IsNullOrEmpty(errorResponse.Message))
            {
                errorResponse.Message = Messages.Disable;
                return Ok(errorResponse);
            }
            else return BadRequest(errorResponse);
        }
        private string GetLoggedinUserId()
        {
            return User.Identity.Name;
        }
    }
}
