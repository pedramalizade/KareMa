using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Service.BaseService
{
    public interface IBaseSevices
    {
        DateTime PersianToGregorian(string persianDateString);
        Task<string> UploadImage(IFormFile image);
    }
}
