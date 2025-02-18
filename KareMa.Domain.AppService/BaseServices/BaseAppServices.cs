using KareMa.Domain.Core.Contracts.AppService.BaseServices;
using KareMa.Domain.Core.Contracts.Service.BaseService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.AppService.BaseAppServices
{
    public class BaseAppServices : IBaseAppServices
    {
        private readonly IBaseSevices _baseSevices;
        public BaseAppServices(IBaseSevices baseSevices)
        {
            _baseSevices = baseSevices;
        }
        public async Task<string> UploadImage(IFormFile image)
          => await _baseSevices.UploadImage(image);
    }

}
