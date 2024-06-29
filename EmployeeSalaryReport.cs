using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class EmployeeSalaryReport
    {
        public int EmpNo { get; set; } 
        public string EmpName { get; set; } 
        public string EmpDesig { get; set; }
        public int Month { get; set; }
        public int Year { get; set; } 
        public decimal PayBasic { get; set; } 
        public decimal PayDA { get; set; } 
        public decimal PayHRA { get; set; }
        public decimal PayCCA { get; set; } 
        public decimal PayConveyance { get; set; } 
        public decimal PayAdhoc { get; set; }
        public decimal PayPF { get; set; } 
        public decimal PayPT { get; set; } 
        public decimal PayLIC { get; set; } 
        public decimal PayIncomeTax { get; set; } 
        public decimal PayOtherEarn { get; set; } 
        public decimal PayOtherDed { get; set; } 
        public decimal PayGrossEarn { get; set; } 
        public decimal PayGrossDed { get; set; } 
        public decimal PayNet { get; set; } 
    }

}
