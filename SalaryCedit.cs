using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public  class SalaryCedit
    {
        public int EmpNo { get; set; }
        public string EmpName { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public Decimal NetSalary { get; set; }
        public string BankBranch { get; set; }
        public string Description { get; set; }
        public string ED_Desc { get; set; }
        public int Cu_Tr_Code { get; set; }
        public string Hrm_Desc { get; set; }
        public int fromNo { get; set; }
        public int ToValue { get; set; }
        public DateTime PayableDate { get; set; }
        public decimal OriginalBasic { get; set; }
        public decimal CurrentBasic { get; set; }
        public DateTime DOj { get; set; }
        public decimal DA { get; set; }
        public decimal HRA { get; set; }
        public decimal CovAllownance { get; set; }
        public decimal AdAllownance { get; set; }
        public decimal otherEarning { get; set; }
        public decimal GrossEarning { get; set; }
        public decimal ProofTax { get; set; }
        public decimal PF { get; set; }
        public decimal IT { get; set; }
        public decimal LIC { get; set; }
        public decimal OtherDeduction { get; set; }
        public int GrossDeduction { get; set; }
        public decimal Netsalary { get; set; }
        public decimal FESTADVRECOVERY2022 { get; set; }
        public string Designation { get; set; }
        public decimal Amount { get; set; }
        public int Code { get; set; }
        public string ED_Description { get; set; }
        public decimal CuTrAmt { get; set; }
        public decimal CCA { get; set; }
        public string CName { get; set; }
        public string CAdd1 { get; set; }
        public string CAdd2 { get; set; }
        public string CAdd3 { get; set; }
        public string CCity { get; set; }
        public string CPin { get; set; }

        public string BranchName { get; set; }
        public decimal GrossDedcut { get; set; }
        public decimal TECHNICALAllownace { get; set; }
    }
}
