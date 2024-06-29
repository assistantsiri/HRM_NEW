using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class IncomeTax_UpdatationBO
    {
        public int EmpNo { get; set; }
        public string EmpName { get; set; }
        public decimal ItAmount { get; set; }
        public decimal Exixting_Monthly_Ded { get; set; }
        public decimal Monthly_Deduction { get; set; }
        public decimal Existing_Current_Month_Ded { get; set; }
        public int Mth_Ded_CurrentMonth { get; set; }
    }
    public class ImcomeTax
    {
        public DateTime Caldate { get; set; }
    }
}
