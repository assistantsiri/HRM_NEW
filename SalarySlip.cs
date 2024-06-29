using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public  class SalarySlip
    {
        public string CName { get; set; }
        public string CAdd1 { get; set; }
        public string CAdd2 { get; set; }
        public string CAdd3 { get; set; }
        public string CCity { get; set; }
        public string CPin { get; set; }
        public int SalMn { get; set; }
        public int SalYr { get; set; }
        public double TotEarn { get; set; }
        public double TotDed { get; set; }
        public int NoOfEarn { get; set; }
        public int NoOfDed { get; set; }
        public object I { get; set; }
        public object TEarn { get; set; }
        public object TDed { get; set; }
        public object NetSalary { get; set; }
        public object LineNo { get; set; }
        public string LOPStr { get; set; }
        public int J { get; set; }
        public int LOPDays { get; set; }
        public byte NoOfSlips { get; set; }
        public string StrSlipMnYr { get; set; }
        public object Rec { get; set; }
        public string QuoteStr { get; set; }
        public bool NoRecords { get; set; } = false;
        public int CUTRNO { get; set; }


    }

    public class SalarySlipEmailBO
    {
        public SalarySlipEmailBO()
        {
            Action = string.Empty;
        }
        public string Action { get; set; }
        public Int16 Branches { get; set; }
        public Int32 EmpNo { get; set; }
    }
}
