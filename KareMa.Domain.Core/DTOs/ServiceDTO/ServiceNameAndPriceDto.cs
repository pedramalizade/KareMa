﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.DTOs.ServiceDTO
{
    public class ServiceNameAndPriceDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
    }

}
