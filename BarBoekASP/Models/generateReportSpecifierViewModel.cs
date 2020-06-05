using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Models
{
    public struct generateReportSpecifierViewModel
    {
        public string vanafTextInput { get; set; }
        public string totTextInput { get; set; }
        public DateTime vanafDateTimeInput { get; set; }
        public DateTime totDateTimeInput { get; set; }
        public string bevatTextInput { get; set; }
    }
}
