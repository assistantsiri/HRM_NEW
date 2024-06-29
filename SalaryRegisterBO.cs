using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class SalaryRegisterBO
    {
        public int Emp_No { get; set; }
        public string Emp_Name { get; set; }
        public string Designation { get; set; }
        public decimal OriBasic { get; set; }
        public DateTime DOJ { get; set; }
        public decimal Basic { get; set; }
        public decimal DA { get; set; }
        public decimal Hra { get; set; }
        public decimal Convey { get; set; }
        public decimal ADHoc { get; set; }
        public decimal OthEarn { get; set; }
        public decimal GrEarn { get; set; }
        public decimal PF { get; set; }
        public decimal PT { get; set; }
        public decimal EPF { get; set; }
        public decimal EPS { get; set; }
        public decimal LIC { get; set; }
        public decimal IT { get; set; }
        public decimal OthDed { get; set; }
        public decimal GrDed { get; set; }
        public decimal Net { get; set; }
    }
}
