using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class Special_Earn_And_DedBO
    {
        public int EmpNo { get; set; }
        public int Code { get; set; }
        public string action { get; set; }
        public string ED { get; set; }
        public decimal Amount { get; set; }
        public string SpEDInd { get; set; }
        public string CodeDesc { get; set; }
        public decimal SpEDAmt { get; set; }
        public string SpEDPayable { get; set; }
        public int SpEDNoDays { get; set; }
        public DateTime SpEDProcessDt { get; set; }
    }
}
