using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.AppService.BaseServices
{
    public interface IBaseAppServices
    {
        Task<string> UploadImage(IFormFile image);
    }

}
