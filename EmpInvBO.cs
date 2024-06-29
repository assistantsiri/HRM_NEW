using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class EmpInvBO
    {
        public EmpInvBO()
        {
            Action = string.Empty;
            Emp_Name = string.Empty;
        }

        public string Action { get; set; }

        public string Emp_Name { get; set; }
    }
    public class incomeTAx
    {

        public decimal MonthlyDeductionamt { get; set; }
        public int SpED_EmpNo { get; set; }
    }
}
