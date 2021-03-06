﻿using System;
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
    public class FileHandlerController : ControllerBase
    {
        private readonly FileService FileService = new FileService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpGet("UploadImage")]
        [HttpPost("UploadImage")]
        public ActionResult UploadImage(IFormFile Image)
        {
            Dictionary<int, string> Keyvalue = FileService.UploadImage(Image);

            if (String.IsNullOrEmpty(Keyvalue[2]))
            {
                if (!String.IsNullOrEmpty(Keyvalue[1]))
                {
                    File file = new File()
                    {
                        url = Keyvalue[1]
                    };
                    return Ok(file);
                }
            }
            else errorResponse.Message = Keyvalue[2];
            return BadRequest(errorResponse);
        }

        [HttpGet("UploadFile")]
        [HttpPost("UploadFile")]
        public ActionResult UploadFile(IFormFile File)
        {
            Dictionary<int, string> Keyvalue = FileService.UploadFile(File);
            if (String.IsNullOrEmpty(Keyvalue[2]))
            {
                if (!String.IsNullOrEmpty(Keyvalue[1]))
                {
                    File file = new File()
                    {
                        url = Keyvalue[1]
                    };
                    return Ok(file);
                }
            }
            else errorResponse.Message = Keyvalue[2];
            return BadRequest(errorResponse);
        }

        [HttpDelete]
        public ActionResult Delete(string url)
        {
            string message = FileService.Delete(url);
            if (String.IsNullOrEmpty(message))
            {
                return Ok();
            }
            else errorResponse.Message = message;
            return BadRequest(errorResponse);
        }
    }
}
