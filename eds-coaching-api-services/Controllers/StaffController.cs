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
    [Authorize(Roles= "Admin, Superadmin")]
    [Route("[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly StaffService StaffService = new StaffService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Staffs()
        {
            List<Staff> Staffs = StaffService.FindAll(GetLoggedinUserId());
            return Ok(Staffs);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Staff Staff = StaffService.FindById(id, GetLoggedinUserId());
            if (Staff != null)
            {
                return Ok(Staff);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Staff Staff)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = StaffService.Save(Staff, GetLoggedinUserId());
                if (errorResponse.Message == null)
                {
                    return Created(HttpContext.Request.Scheme, Staff);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Staff Staff)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = StaffService.Update(Staff, GetLoggedinUserId());
                if (errorResponse.Message == null)
                {
                    return Ok(Staff);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }


        [HttpPost("Enable/{id}")]
        public ActionResult EnableById(int id)
        {
            errorResponse.Message = StaffService.Enable(id, GetLoggedinUserId());
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
            errorResponse.Message = StaffService.Disable(id, GetLoggedinUserId());
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
