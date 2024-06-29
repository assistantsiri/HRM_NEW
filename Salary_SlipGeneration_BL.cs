using System;
using System.Collections.Generic;
using System.IO;

namespace BL
{
    public class SalaryPaySlipBL
    {
        DAL.Salary_Slip_Generation_DA salaryPaySlipDA = new DAL.Salary_Slip_Generation_DA();

        public void GenerateAndSaveSalarySlip(int employeeNumber, string directoryPath, string selectedBranchNames,string description)
        {
            try
            {
                // Declaration and initialization of selectedBranches

                salaryPaySlipDA.GenerateSalarySlip(employeeNumber, directoryPath, selectedBranchNames, description);
                Console.WriteLine("Salary slip generated and saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating or saving salary slip: " + ex.Message);
            }
        }

        public void GetBranchNames()
        {
            try
            {
                 salaryPaySlipDA.BindBranches();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting branch names: " + ex.Message);
                
            }
        }

        public DAL.BranchDetails GetBranchDetails()
        {
            try
            {
                return salaryPaySlipDA.GetBranchDetails();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting branch details: " + ex.Message);
                return new DAL.BranchDetails();
            }
        }
    }
}
