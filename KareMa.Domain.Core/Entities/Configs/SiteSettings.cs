using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Entities.Configs
{
    public class SiteSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public string ApiKey { get; set; }
    }

}
