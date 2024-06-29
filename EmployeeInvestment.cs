using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class EmployeeInvestment
    {
        public int EmpNo { get; set; }
        public int MCode { get; set; }
        public string SCode { get; set; }
        public int SubNo { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public int ITCode { get; set; }
        public string Action { get; set; }
        public int EmpInvYear { get; set; }
    }

}
