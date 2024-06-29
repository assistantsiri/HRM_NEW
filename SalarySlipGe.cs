using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public  class SalarySlipGe
    {
        public int EmpNo { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public decimal PF { get; set; }
        public decimal BAsicPAy { get; set; }
        public decimal HRA { get; set; }
        public decimal DA { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal FestandRecovery { get; set; }
        public decimal PTax { get; set; }
        public string ED_Description { get; set; }
        public decimal Amount { get; set; }
        public int Code { get; set; }
    }
}
