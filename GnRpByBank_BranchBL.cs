using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA
{
    public class GnRpByBank_BranchBL
    {
        private GnRpByBank_BranchDA gnRpByBank_BranchDA;

        public GnRpByBank_BranchBL()
        {
            gnRpByBank_BranchDA = new GnRpByBank_BranchDA();
        }
        //public string GenerateReportByBankAndBranch(string selectedBankName, string selectedBranchName, string cname, DateTime nextProcessDate)
        //{

        //    //return gnRpByBank_BranchDA.GenerateEmployeeBankInfoReport(selectedBankName, selectedBranchName, cname, nextProcessDate );
        //}

        public void GetBankBranches()
        {

            List<string> branchNames = gnRpByBank_BranchDA.BankBranches();


            foreach (string branchName in branchNames)
            {
                Console.WriteLine(branchName);

            }
        }
        public void GetBank()
        {

             gnRpByBank_BranchDA.GetBank_Name();
        }

    }
}
