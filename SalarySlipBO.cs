using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class SalarySlipBO
    {
        public SalarySlipBO()
        {
            Action = string.Empty;
        }
            
        public Int32 Emp_No { get; set; }
        public string Action { get; set; }
    }
}
