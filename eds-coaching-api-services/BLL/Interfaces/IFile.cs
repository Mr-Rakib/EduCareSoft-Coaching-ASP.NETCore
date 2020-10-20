using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Interfaces
{
    interface IFile
    {
        public Dictionary<int, string> UploadImage(IFormFile Image);
        public Dictionary<int, string> UploadFile(IFormFile File);
        public string Delete(string imagePath);

    }
}
