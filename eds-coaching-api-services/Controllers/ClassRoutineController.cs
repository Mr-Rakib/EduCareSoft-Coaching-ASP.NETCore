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
    public class ClassRoutineController : ControllerBase
    {
        private readonly ClassRoutineService ClassRoutineService = new ClassRoutineService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Get()
        {
            List<ClassRoutine> ClassRoutines = ClassRoutineService.FindAll(User.Identity.Name);
            return Ok(ClassRoutines);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            ClassRoutine ClassRoutine = ClassRoutineService.FindById(id, User.Identity.Name);
            if (ClassRoutine != null)
            {
                return Ok(ClassRoutine);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpGet("class/{id}")]
        public ActionResult GetByClassId(int id)
        {
            List<ClassRoutine> ClassRoutines = ClassRoutineService.FindByClass(id, User.Identity.Name);
            if (ClassRoutines != null)
            {
                return Ok(ClassRoutines);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpGet("student/{id}")]
        public ActionResult GetByStudentId(int id)
        {
            List<ClassRoutine> ClassRoutines = ClassRoutineService.FindByStudentId(id, User.Identity.Name);
            if (ClassRoutines != null)
            {
                return Ok(ClassRoutines);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [Authorize(Roles = "Superadmin, Admin, Teacher")]
        [HttpGet("staff/{id}")]
        public ActionResult GetByStaffId(int id)
        {
            List<ClassRoutine> ClassRoutines = ClassRoutineService.FindByStaffId(id, User.Identity.Name);
            if (ClassRoutines != null)
            {
                return Ok(ClassRoutines);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpGet("day/{day}")]
        public ActionResult GetByDay(string day)
        {
            List<ClassRoutine> ClassRoutines = ClassRoutineService.FindByDay(day, User.Identity.Name);
            if (ClassRoutines != null)
            {
                return Ok(ClassRoutines);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [Authorize(Roles = "Admin, Superadmin")]
        [HttpPost]
        public ActionResult Post([FromBody] ClassRoutine ClassRoutine)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = ClassRoutineService.Save(ClassRoutine, User.Identity.Name);
                if (String.IsNullOrEmpty(errorResponse.Message))
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
        public ActionResult Put([FromBody] ClassRoutine ClassRoutine)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = ClassRoutineService.Update(ClassRoutine, User.Identity.Name);
                if (String.IsNullOrEmpty(errorResponse.Message))
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
            errorResponse.Message = ClassRoutineService.DeleteById(id, User.Identity.Name);
            if (String.IsNullOrEmpty(errorResponse.Message))
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }
    }
}
