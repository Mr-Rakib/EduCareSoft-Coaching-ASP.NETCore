using eds_coaching_api_services.BLL.Services;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Dependencies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Controllers
{
    [Authorize(Roles = "Admin, Superadmin")]
    [Route("[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly StudentAttendanceService StudentAttendanceService = new StudentAttendanceService();
        private readonly StaffAttendanceService StaffAttendanceService = new StaffAttendanceService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            Attendance StudentAttendance = StudentAttendanceService.FindById(id, User.Identity.Name);
            if (StudentAttendance != null)
            {
                return Ok(StudentAttendance);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpGet("students")]
        public ActionResult GetAllStudents()
        {
            List<Attendance> StudentAttendances = StudentAttendanceService.FindAll(User.Identity.Name);
            return Ok(StudentAttendances);
        }

        [HttpGet("students/{id}")]
        public ActionResult GetByStudentId(int id)
        {
            List<Attendance> StudentAttendances = StudentAttendanceService.FindByUserId(id, User.Identity.Name);
            if (StudentAttendances != null)
            {
                return Ok(StudentAttendances);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpGet("students{date}")]
        public ActionResult GetStudentsByDate(DateTime date)
        {
            List<Attendance> StudentAttendances = StudentAttendanceService.FindByDate(date, User.Identity.Name);
            if (StudentAttendances != null)
            {
                return Ok(StudentAttendances);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }


        [HttpPost("students")]
        public ActionResult PostStudents([FromBody] Attendance StudentAttendance)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = StudentAttendanceService.Save(StudentAttendance, User.Identity.Name);
                if (errorResponse.Message == null)
                {
                    errorResponse.Message = Messages.Saved;
                    return Created(HttpContext.Request.Scheme, errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [HttpPut("students/{id}")]
        public ActionResult PutStudents([FromBody] Attendance StudentAttendance)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = StudentAttendanceService.Update(StudentAttendance, User.Identity.Name);
                if (errorResponse.Message == null)
                {
                    errorResponse.Message = Messages.Updated;
                    return Ok(errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [HttpDelete("students/{id}")]
        public ActionResult DeleteStudents(int id)
        {
            errorResponse.Message = StudentAttendanceService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }

        //Staff Begin

        [HttpGet("staffs")]
        public ActionResult GetAllStaffs()
        {
            List<Attendance> StaffAttendances = StaffAttendanceService.FindAll(User.Identity.Name);
            return Ok(StaffAttendances);
        }

        [HttpGet("staffs/{id}")]
        public ActionResult GetByStaffId(int id)
        {
            List<Attendance> StaffAttendances = StaffAttendanceService.FindByUserId(id, User.Identity.Name);
            if (StaffAttendances != null)
            {
                return Ok(StaffAttendances);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpGet("staffs{date}")]
        public ActionResult GetStaffsByDate(DateTime date)
        {
            List<Attendance> StaffAttendances = StaffAttendanceService.FindByDate(date, User.Identity.Name);
            if (StaffAttendances != null)
            {
                return Ok(StaffAttendances);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }


        [HttpPost("staffs")]
        public ActionResult PostStaff([FromBody] Attendance StaffAttendance)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = StaffAttendanceService.Save(StaffAttendance, User.Identity.Name);
                if (errorResponse.Message == null)
                {
                    errorResponse.Message = Messages.Saved;
                    return Created(HttpContext.Request.Scheme, errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [HttpPut("staffs/{id}")]
        public ActionResult PutStaff([FromBody] Attendance StaffAttendance)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = StaffAttendanceService.Update(StaffAttendance, User.Identity.Name);
                if (errorResponse.Message == null)
                {
                    errorResponse.Message = Messages.Updated;
                    return Ok(errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [HttpDelete("staffs/{id}")]
        public ActionResult DeleteStaff(int id)
        {
            errorResponse.Message = StaffAttendanceService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }




    }
}
