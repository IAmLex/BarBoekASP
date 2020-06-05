using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Models
{
    public class generateReportTotalListsViewModel
    {
        public List<string> usedTables { get; set; }
        public List<string> usedColumns { get; set; }
        public string NameSQLSpecifier { get; set; }
        public string ShiftDateSQLSpecifier { get; set; }
        public string AgeSQLSpecifier { get; set; }
        public string AbsenceSQLSpecifier { get; set; }
        public string GroupSQLSpecifier { get; set; }
        public generateReportSpecifierViewModel specifiersToAdd {get; set;}
        public string selectedTable { get; set; }
        public List<string> columns { get; set; }
        public IConfiguration config { get; set; }
    }
}
