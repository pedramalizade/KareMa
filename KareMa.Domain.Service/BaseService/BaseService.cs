using KareMa.Domain.Core.Contracts.Service.BaseService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Service.BaseService
{
    public class BaseService : IBaseSevices
    {
        public async Task<string> UploadImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream); 
                }

                return "/uploads/" + uniqueFileName;
            }
            return null;
        }

        public DateTime PersianToGregorian(string persianDateString)
        {
            PersianCalendar pc = new PersianCalendar();
            string[] separatedTimeAndDate = persianDateString.Split(' ');

            string[] persianDateParts = separatedTimeAndDate[0].Split('/');
            string[] persianTimePart = separatedTimeAndDate[1].Split(':');

            int year = int.Parse(persianDateParts[0]);
            int month = int.Parse(persianDateParts[1]);
            int day = int.Parse(persianDateParts[2]);
            int hour = int.Parse(persianTimePart[0]);
            int minute = int.Parse(persianTimePart[1]);
            int second = int.Parse(persianTimePart[2]);

            var gregorianDate = pc.ToDateTime(year, month, day, hour, minute, second, 0);
            return gregorianDate;
        }
    }
}
