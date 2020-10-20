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
    [Authorize(Roles = "Admin, Superadmin")]
    [Route("[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly BatchService BatchService = new BatchService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Get()
        {
            List<Batch> Batchs = BatchService.FindAll(User.Identity.Name);
            return Ok(Batchs);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Batch Batch = BatchService.FindById(id, User.Identity.Name);
            if (Batch != null)
            {
                return Ok(Batch);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Batch Batch)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = BatchService.Save(Batch, User.Identity.Name);
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
        public ActionResult Put([FromBody] Batch Batch)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = BatchService.Update(Batch, User.Identity.Name);
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
            errorResponse.Message = BatchService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }
    }
}
