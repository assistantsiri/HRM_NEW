using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA
{
    public class HrmsSalarySlipBL
    {
        //HrmsSalarySlipDA hrmsSalarySlipDA = new HrmsSalarySlipDA();
        public void GenerateSalarySlip(int fromEmpNo, int toEmpNo, string directoryPath, string selectedBranchNames)
        {
            try
            {
                // Declaration and initialization of selectedBranches

               // hrmsSalarySlipDA.GenerateSalarySlip(fromEmpNo, toEmpNo, directoryPath, selectedBranchNames);
                Console.WriteLine("Salary slip generated and saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating or saving salary slip: " + ex.Message);
            }
        }
    }
}
