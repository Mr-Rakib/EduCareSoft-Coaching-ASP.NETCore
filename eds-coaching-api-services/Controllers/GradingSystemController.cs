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
    public class GradingSystemController : ControllerBase
    {
        private readonly GradingSystemService GradingSystemService = new GradingSystemService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Get()
        {
            List<GradingSystem> GradingSystems = GradingSystemService.FindAll(User.Identity.Name);
            return Ok(GradingSystems);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            GradingSystem GradingSystem = GradingSystemService.FindById(id, User.Identity.Name);
            if (GradingSystem != null)
            {
                return Ok(GradingSystem);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpPost]
        public ActionResult Post([FromBody] GradingSystem GradingSystem)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = GradingSystemService.Save(GradingSystem, User.Identity.Name);
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
        public ActionResult Put([FromBody] GradingSystem GradingSystem)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = GradingSystemService.Update(GradingSystem, User.Identity.Name);
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
            errorResponse.Message = GradingSystemService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }
    }
}
