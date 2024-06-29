using BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class Salary_Slip_Generation_DA
    {
        EmployeeBO employeeBO = new EmployeeBO();
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        private List<string> selectedBranches;

        public void SetSelectedBranches(List<string> branches)
        {
            selectedBranches = branches;
        }
        public void GenerateSalarySlip(int empNo, string directoryPath, string selectedBranchNames,string description)
        {
            SqlConnection conn = new SqlConnection(connStr);
            EmployeeBO employeeBO = new EmployeeBO();
            BranchDetails branchDetails = GetBranchDetails();
            List<string> branchNames = BindBranches();

            conn.Open();
            using (SqlCommand command = new SqlCommand("HRMS_SalarySlipGeneration", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "S");
                command.Parameters.AddWithValue("@FromEmpNo", empNo);
                command.Parameters.AddWithValue("@ToEmpNo", empNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeBO.EmpNo = Convert.ToInt32(reader["Emp_No"]);
                        employeeBO.EmpName = reader["Emp_Name"].ToString();
                        employeeBO.Designation = reader["Designation"].ToString();
                    }
                }

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Action", "E");
                command.Parameters.AddWithValue("@FromEmpNo", empNo);
                command.Parameters.AddWithValue("@ToEmpNo", empNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeBO.ED_Desc = reader["ED_Desc"].ToString();
                        employeeBO.CuTrAmt = Convert.ToDecimal(reader["Cu_Tr_Amt"]);
                        employeeBO.Cu_Tr_Code = Convert.ToInt32(reader["Cu_Tr_Code"]);
                        switch (employeeBO.ED_Desc)
                        {
                            case "PF":
                                employeeBO.PF = employeeBO.CuTrAmt;
                                break;
                            case "INCOME TAXF":
                                employeeBO.IT = employeeBO.CuTrAmt;
                                break;
                            case "P.TAX":
                                employeeBO.ProofTax = employeeBO.CuTrAmt;
                                break;
                            case "FEST ADV RECOVERY 2022":
                                employeeBO.FESTADVRECOVERY2022 = employeeBO.CuTrAmt;
                                break;
                            default:
                                break;
                        }
                    }
                }

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Action", "D");
                command.Parameters.AddWithValue("@FromEmpNo", empNo);
                command.Parameters.AddWithValue("@ToEmpNo", empNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeBO.ED_Desc = reader["ED_Desc"].ToString();
                        employeeBO.CuTrAmt = Convert.ToDecimal(reader["Cu_Tr_Amt"]);
                        employeeBO.Cu_Tr_Code = Convert.ToInt32(reader["Cu_Tr_Code"]);
                        switch (employeeBO.ED_Desc)
                        {
                            case "BASIC PAY":
                                employeeBO.CurrentBasic = employeeBO.CuTrAmt;
                                break;
                            case "DA":
                                employeeBO.DA = employeeBO.CuTrAmt;
                                break;
                            case "HRA":
                                employeeBO.HRA = employeeBO.CuTrAmt;
                                break;
                            case "TECHNICAL ALLOWANCE":
                                employeeBO.TECHNICALAllownace = employeeBO.CuTrAmt;
                                break;
                            case "CONVEYANCE":
                                employeeBO.CovAllownance = employeeBO.CuTrAmt;
                                break;
                            default:
                                break;
                        }
                    }
                }


            }



            decimal totalEarnings = employeeBO.CurrentBasic + employeeBO.HRA + employeeBO.DA +employeeBO.TECHNICALAllownace + employeeBO.CovAllownance;// employeeBO.TechnicalAllowance;


            decimal totalDeductions = employeeBO.PF + employeeBO.IT + employeeBO.ProofTax + employeeBO.FESTADVRECOVERY2022;
            decimal GrossEarning = totalEarnings;
            decimal GrossDedcut = totalDeductions;

            decimal netSalary = totalEarnings - totalDeductions;

           

            StringBuilder salarySlip = new StringBuilder();


            // Calculate padding for the "PF" label
            int pfPadding = Math.Max(0, "PF".Length - 2); // Subtracting 2 for the space after "PF"

            // Calculate padding for the "P.Tax" label
            int ptaxPadding = Math.Max(0, "P.Tax".Length - 5); // Subtracting 5 for the space after "P.Tax"

            // Calculate padding for the "Fest ADV Recovery" label
            int festPadding = Math.Max(0, "Fest ADV Recovery".Length - 17); // Subtracting 17 for the space after "Fest ADV Recovery"

            salarySlip.AppendLine($"{Tab(1)}-----------***Salary slip for the month of " + DateTime.Now.ToString("MMMM - yyyy") + "***----------------------------------------");
            salarySlip.AppendLine();
            salarySlip.AppendLine($"{Tab(1)}{TrimAndUppercase(branchDetails.BrName)}{Tab(96)}");
            salarySlip.AppendLine($"{Tab(1)}{TrimAndUppercase(branchDetails.BrAdd1)}");
            salarySlip.AppendLine($"{Tab(1)}{TrimAndUppercase(branchDetails.BrAdd2)}");
            salarySlip.AppendLine($"{Tab(1)}{TrimAndUppercase(branchDetails.BrPin)}");
            salarySlip.AppendLine($"{Tab(1)}{"".PadLeft(47)}EmpNo      : {employeeBO.EmpNo}");
            salarySlip.AppendLine($"{Tab(1)}{"".PadLeft(47)}EmpName    : {employeeBO.EmpName}");
            salarySlip.AppendLine($"{Tab(1)}{"".PadLeft(47)}Designation: {employeeBO.Designation}");
            salarySlip.AppendLine($"{Tab(1)}{"".PadLeft(47)}Dept       : {selectedBranchNames}");

            salarySlip.AppendLine();
            salarySlip.AppendLine($"{Tab(1)}---------------------------------------------------------------------------------------------------");
            salarySlip.AppendLine($"{Tab(1)} Earnings                              Amount    {Tab(1)}      Deductions              Amount  ");
            salarySlip.AppendLine($"{Tab(1)}---------------------------------------------------------------------------------------------------");

            // Add earning details
            salarySlip.AppendLine($"{Tab(1)}BasicPay{Tab(3)}{employeeBO.CurrentBasic.ToString("0.00").PadLeft(15)}     | {Tab(1)}PF{Tab(2)}       {employeeBO.PF.ToString("0.00").PadLeft(15 - pfPadding)}");
            salarySlip.AppendLine($"{Tab(1)}DA    {Tab(3)}        {employeeBO.DA.ToString("0.00").PadLeft(15)}     | {Tab(1)}P.Tax{Tab(2)}      {employeeBO.ProofTax.ToString("0.00").PadLeft(15 - ptaxPadding)}");
            salarySlip.AppendLine($"{Tab(1)}HRA{Tab(3)}       {employeeBO.HRA.ToString("0.00").PadLeft(15)}      |{Tab(1)}Fest ADV Recovery      {employeeBO.FESTADVRECOVERY2022.ToString("0.00").PadLeft(15 - festPadding)}");
            salarySlip.AppendLine($"{Tab(1)}CovAllownance {Tab(2)}    {employeeBO.CovAllownance.ToString("0.00").PadLeft(15)}         |{Tab(1)}IT{Tab(1)}            { employeeBO.IT.ToString("0.00").PadLeft(15 - pfPadding)}");
            salarySlip.AppendLine($"{Tab(1)}TECHNICALAllownace {Tab(2)}{employeeBO.TECHNICALAllownace.ToString("0.00").PadLeft(15)}     |                                                                             ");
            salarySlip.AppendLine($"{Tab(1)}  {Tab(2)}                                    |                                                  ");
            salarySlip.AppendLine($"{Tab(1)}  {Tab(2)}                                    |                                                  ");
            salarySlip.AppendLine($"{Tab(1)}  {Tab(2)}                                    |                                                  ");
            salarySlip.AppendLine($"{Tab(1)}  {Tab(2)}                                    |                                                  ");

            salarySlip.AppendLine($"{Tab(1)}---------------------------------------------------------------------------------------------------");
          
            salarySlip.AppendLine($"{Tab(1)}Gross Earning   {Tab(3)}{AddSpace(GrossEarning.ToString("0.00"), 12, "L")}|     Gross Deductions{Tab(2)}{AddSpace(GrossDedcut.ToString("0.00"), 12, "L")}");
            salarySlip.AppendLine($"{Tab(1)}---------------------------------------------------------------------------------------------------");
            salarySlip.AppendLine($"{Tab(1)} NetSalary Rs: {Tab(4)}{AddSpace(netSalary.ToString("0.00"), 12, "L")}|     Payable on : {Tab(2)}{DateTime.Now.ToString("MMMM - yyyy")}");
            salarySlip.AppendLine($"{Tab(1)}---------------------------------------------------------------------------------------------------");
            salarySlip.AppendLine($"{Tab(1)}---------------------------------*****{description}********---------------------------------------------------");
            //salarySlip.AppendLine($"{Tab(5)}Gross Earnings {Tab(34)}{AddSpace(GrossEarning.ToString("0.00"), 12, "L")}        |     Gross Deductions {Tab(80)}{AddSpace(GrossDedcut.ToString("0.00"), 12, "L")}");

            string fileName = "";
            string filePath = "";
            //string directoryPath = @"D:\Pramodh Mishra\Report";
            // Write salary slip content to file
            if (employeeBO != null && employeeBO.EmpNo != null && employeeBO.EmpName != null)
            {
                //fileName = $"SalarySlip_Emp{employeeBO.EmpNo}_{employeeBO.EmpName.Replace(" ", "_")}.txt";
                // filePath = Path.Combine(directoryPath ?? "", fileName ?? "");
                // Ensure directoryPath and fileName are not null before combining
                // Set the directory path
                string rootDirectoryPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/SalarySlip");
                 fileName = $"SalarySlip_Emp{employeeBO.EmpNo}_{employeeBO.EmpName.Replace(" ", "_")}.txt";
                 filePath = Path.Combine(rootDirectoryPath, fileName);



            }
            else
            {
                // Handle the case where employeeBO or its properties are null
            }
            if (string.IsNullOrEmpty(filePath))
            {
                // Log an error or handle the situation appropriately
                Console.WriteLine("Error: filePath is empty or null.");
            }
            else
            {
                // If filePath is valid, write to the file using StreamWriter
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(salarySlip.ToString());
                }
            }


        }
        public List<string> BindBranches()
        {
            List<string> branchNames = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr        ))
                {
                    using (SqlCommand command = new SqlCommand("GetCompBranches", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string cbrCode = reader["CBr_Code"].ToString();
                                string cbrName = reader["CBr_Name"].ToString();
                                branchNames.Add(cbrCode + "\t" + cbrName); // Add branch name to the list
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting branch names: " + ex.Message);
            }
            return branchNames;
        }

        public BranchDetails GetBranchDetails()
        {
            BranchDetails branchDetails = new BranchDetails();
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("GetBranchDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                branchDetails.BrName = reader["Br_Name"].ToString();
                                branchDetails.BrAdd1 = reader["Br_Add1"].ToString();
                                branchDetails.BrAdd2 = reader["Br_Add2"].ToString();
                                branchDetails.BrAdd3 = reader["Br_Add3"].ToString();
                                branchDetails.BrCity = reader["Br_City"].ToString();
                                branchDetails.BrPin = reader["Br_Pin"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting branch details", ex);
            }
            return branchDetails;
        }

        public string GetNextMonthAndYear()
        {
            string nextMonthAndYear = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("GetNextMonthAndYear", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            int month = Convert.ToInt32(reader["Mn"]);
                            int year = Convert.ToInt32(reader["Yr"]);
                            nextMonthAndYear = $"{month}/{year}";
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting next month and year", ex);
            }
            return nextMonthAndYear;
        }
        // Define a function for tabulation
        string Tab(int count)
        {
            return new string('\t', count);
        }

        // Define a function for trimming and converting to uppercase
        string TrimAndUppercase(string input)
        {
            return input.Trim().ToUpper();
        }

        public static string AddSpace(string input, int desiredLength, string alignment)
        {
            if (input.Length >= desiredLength)
                return input;

            int spacesToAdd = desiredLength - input.Length;
            string spaces = new string(' ', spacesToAdd);

            if (alignment == "L")
                return input + spaces;
            else if (alignment == "R")
                return spaces + input;
            else // Default to left alignment if alignment is not specified or incorrect
                return input + spaces;
        }

        public DataTable SalaryRegister(EmployeeBO employeeBO)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlDataAdapter dAd = new SqlDataAdapter("HRMS_SalaryRegisterNew", conn);
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
            dAd.SelectCommand.Parameters.Add("@Branch", SqlDbType.SmallInt).Value = employeeBO.BranchCode;
            dAd.SelectCommand.Parameters.Add("@Action", SqlDbType.Char).Value = employeeBO.Action;
            dAd.SelectCommand.Parameters.Add("@FromEmp", SqlDbType.Int).Value = employeeBO.fromNo;
            dAd.SelectCommand.Parameters.Add("@ToEmp", SqlDbType.Int).Value = employeeBO.ToNo;

            dAd.SelectCommand.Parameters.Add("@Emp", SqlDbType.Int).Value = employeeBO.EmpNo;

            DataSet dSet = new DataSet();
            try
            {
                dAd.Fill(dSet, "HRMS_SALARY_REGISTER");
                return dSet.Tables["HRMS_SALARY_REGISTER"];
            }
            catch
            {
                throw;
            }
            finally
            {
                dSet.Dispose();
                dAd.Dispose();
                conn.Close();
                conn.Dispose();
            }

        }
    }
    public class BranchDetails
    {
        public string BrName { get; set; }
        public string BrAdd1 { get; set; }
        public string BrAdd2 { get; set; }
        public string BrAdd3 { get; set; }
        public string BrCity { get; set; }
        public string BrPin { get; set; }
    }
}

