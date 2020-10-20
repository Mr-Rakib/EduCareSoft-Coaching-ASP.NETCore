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
    public class LoginController : ControllerBase
    {
        private readonly LoginService loginServices = new LoginService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult LoginInformations()
        {
            List<Login> loginLists = loginServices.FindAll(User.Identity.Name);
            return Ok(loginLists);
        }

        [HttpGet("{username}")]
        public ActionResult LoginInformations(string Username)
        {
            Login login = loginServices.FindByUsername(Username, User.Identity.Name);
            if (login != null)
            {
                return Ok(login);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpGet("Staffs")]
        public ActionResult StaffsLoginInformations()
        {
            List<Login> loginLists = loginServices.FindAllStaffs(User.Identity.Name);
            return Ok(loginLists);
        }
        
        [HttpGet("Staffs/{username}")]
        public ActionResult StaffsLoginInformationsByUsername(string Username)
        {
            Login login = loginServices.FindStaffByUsername(Username, User.Identity.Name);
            if(login != null)
            {
                return Ok(login);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpGet("Students")]
        public ActionResult StudentsLoginInformations()
        {
            List<Login> loginLists = loginServices.FindAllStudents(User.Identity.Name);
            return Ok(loginLists);
        }

        [HttpGet("Students/{username}")]
        public ActionResult StudentsLoginInformationsByUsername(string Username)
        {
            Login login = loginServices.FindStudentByUsername(Username, User.Identity.Name);
            if (login != null)
            {
                return Ok(login);
            }
            else errorResponse.Message =  Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpPost("Enable/{username}")]
        public ActionResult EnableById(string username)
        {
            errorResponse.Message = loginServices.Enable(username, User.Identity.Name);
            if (String.IsNullOrEmpty(errorResponse.Message))
            {
                errorResponse.Message = Messages.Enable;
                return Ok(errorResponse);
            }
            else return BadRequest(errorResponse);
        }

        [HttpPost("Disable/{username}")]
        public ActionResult DisableById(string username)
        {
            errorResponse.Message = loginServices.Disable(username, User.Identity.Name);
            if (String.IsNullOrEmpty(errorResponse.Message))
            {
                errorResponse.Message = Messages.Disable;
                return Ok(errorResponse);
            }
            else return BadRequest(errorResponse);
        }

        [HttpPost("ChangePassword/{username}")]
        public ActionResult ChangePassword(string username, string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                errorResponse.Message = loginServices.ChangePassword(username, password, User.Identity.Name);
                if (String.IsNullOrEmpty(errorResponse.Message))
                {
                    errorResponse.Message = Messages.PasswordUpdated;
                    return Ok(errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }
    }
}
