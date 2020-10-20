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
    public class FeesCollectionController : ControllerBase
    {
        private readonly FeesCollectionService FeesCollectionService = new FeesCollectionService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult Get()
        {
            List<FeesCollection> FeesCollections = FeesCollectionService.FindAll(User.Identity.Name);
            return Ok(FeesCollections);
        }

        

        [Authorize(Roles = "Superadmin, Admin")]
        [HttpGet("paid")]
        public ActionResult GetPaidStudent(string month, int year)
        {
            List<FeesCollection> FeesCollections = FeesCollectionService.FindAllPaidStudent(User.Identity.Name, month, year);
            return Ok(FeesCollections);
        }

        [Authorize(Roles = "Superadmin, Admin")]
        [HttpGet("unpaid")]
        public ActionResult GetUnpaid(string month, int year)
        {
            List<FeesCollection> FeesCollections = FeesCollectionService.FindAllUnpaidStudent(User.Identity.Name, month, year);
            return Ok(FeesCollections);
        }

        [Authorize(Roles = "Superadmin, Admin")]
        [HttpGet("discount")]
        public ActionResult GetDiscount(string month, int year)
        {
            List<FeesCollection> FeesCollections = FeesCollectionService.FindAllDiscount(User.Identity.Name, month , year);
            return Ok(FeesCollections);
        }

        [Authorize(Roles = "Superadmin, Admin")]
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            FeesCollection FeesCollection = FeesCollectionService.FindById(id, User.Identity.Name);
            if (FeesCollection != null)
            {
                return Ok(FeesCollection);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [HttpGet("history")]
        public ActionResult History(int year, string month)
        {
            List<FeesCollection> FeesCollections = FeesCollectionService.FindAllHistory(month, year, User.Identity.Name);
            if (FeesCollections != null)
            {
                return Ok(FeesCollections);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [Authorize(Roles = "Superadmin, Admin")]
        [HttpGet("student/{id}")]
        public ActionResult GetByStudentId(int id, string month, int year)
        {
            if (ModelState.IsValid)
            {
                List<FeesCollection> FeesCollection = FeesCollectionService.FindMonthlyFeesByStudentId(id, User.Identity.Name, month, year);
                if (FeesCollection != null)
                {
                    return Ok(FeesCollection);
                }
                else errorResponse.Message = Messages.NotFound;
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [Authorize(Roles="Superadmin, Admin")]
        [HttpGet("staff/{id}")]
        public ActionResult GetByStaff(int id, string month, int year)
        {
            List<FeesCollection> FeesCollections = FeesCollectionService.FindFeesCollectedByStaffId(id, User.Identity.Name, month, year);
            if (FeesCollections != null)
            {
                return Ok(FeesCollections);
            }
            else errorResponse.Message = Messages.NotFound;
            return BadRequest(errorResponse);
        }

        [Authorize(Roles="Superadmin, Admin")]
        [HttpPost]
        public ActionResult Post([FromBody] FeesCollection FeesCollection)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = FeesCollectionService.Save(FeesCollection, User.Identity.Name);
                if (errorResponse.Message == null)
                {
                    errorResponse.Message = Messages.Saved;
                    return Created(HttpContext.Request.Scheme, errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [Authorize(Roles = "Superadmin, Admin")]
        [HttpPost("generate")]
        public ActionResult GenerateFeesCollection(string month, int year, int institutionId)
        {
            errorResponse.Message = FeesCollectionService.GenerateFeesForStudent(month, year, institutionId, User.Identity.Name);
            if (String.IsNullOrEmpty(errorResponse.Message))
            {
                return Ok();
            }
            else return BadRequest(errorResponse);
        }

        [Authorize(Roles="Superadmin, Admin")]
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] FeesCollection FeesCollection)
        {
            if (ModelState.IsValid)
            {
                errorResponse.Message = FeesCollectionService.Update(FeesCollection, User.Identity.Name);
                if (errorResponse.Message == null)
                {
                    errorResponse.Message = Messages.Updated;
                    return Ok(errorResponse);
                }
            }
            else errorResponse.Message = Messages.invalidField;
            return BadRequest(errorResponse);
        }

        [Authorize(Roles="Superadmin, Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            errorResponse.Message = FeesCollectionService.DeleteById(id, User.Identity.Name);
            if (errorResponse.Message == null)
            {
                errorResponse.Message = Messages.Deleted;
                return Ok(errorResponse);
            }
            return BadRequest(errorResponse);
        }
    }
}
