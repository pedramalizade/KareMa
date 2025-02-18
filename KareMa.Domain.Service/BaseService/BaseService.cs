using KareMa.Domain.Core.Contracts.Service.BaseService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Service.BaseService
{
    public class BaseService : IBaseSevices
    {
        public async Task<string> UploadImage(IFormFile image)
        {
            string filePath;
            if (image != null && image.Length > 0)
            {
                filePath = @"E:\Project\KareMa\KareMa.Endpoint.MVC.UI\wwwroot\uploads\" + image.FileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                return filePath;
            }
            return null;
        }
    }
}
