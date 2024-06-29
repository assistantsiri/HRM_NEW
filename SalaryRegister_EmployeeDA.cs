using BO;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class SalaryRegister_EmployeeDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        EmployeeBO employeeBO = new EmployeeBO();
        public DataTable FetchEmployeeInformation()
        {
            DataTable employeeData = new DataTable();
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                using (SqlCommand employeeCommand = new SqlCommand("GetEmployeeDetailsByEmpNo", connection))
                {
                    employeeCommand.CommandType = CommandType.StoredProcedure;
                    employeeCommand.Parameters.AddWithValue("@Emp_No", employeeBO.EmpNo);
                    using (SqlDataReader reader = employeeCommand.ExecuteReader())
                    {
                        // Add columns to the DataTable
                        employeeData.Columns.Add("EmpNo", typeof(int));
                        employeeData.Columns.Add("EmpName", typeof(string));
                        employeeData.Columns.Add("EmpDesig", typeof(string));

                        // Read data from the SqlDataReader and populate the DataTable
                        while (reader.Read())
                        {
                            DataRow row = employeeData.NewRow();
                            row["EmpNo"] = Convert.ToInt32(reader["Emp_No"]);
                            row["EmpName"] = reader["Emp_Name"].ToString();
                            row["EmpDesig"] = reader["Hrm_Desc"].ToString();
                            employeeData.Rows.Add(row);
                        }
                    }
                }
            }
            return employeeData;

        }

        public string GetBranchName()
        {
            string branchName = null;
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                using (SqlCommand employeeCommand = new SqlCommand("GetBranchNames", connection))
                {
                    using (SqlDataReader reader = employeeCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            branchName = reader["Br_Name"].ToString();
                        }
                    }
                }

            }
            return branchName;
        }
        public (int EmpNo, string EmpName) GetEmployeeDetails(int empNo)
        {
            int empNumber = 0;
            string empName = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                using (SqlCommand employeeCommand = new SqlCommand("GetEmployeeDetailsByEmp_No", connection))
                {
                    employeeCommand.CommandType = CommandType.StoredProcedure;
                    employeeCommand.Parameters.AddWithValue("@EmpNo", empNo);
                    using (SqlDataReader reader = employeeCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            empNumber = Convert.ToInt32(reader["Emp_No"]);
                            empName = reader["Emp_Name"].ToString();
                        }
                    }
                }
            }

            return (empNumber, empName);
        }
        string Tab(int count)
        {
            return new string('\t', count);
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
        // Define a function for trimming and converting to uppercase
        string TrimAndUppercase(string input)
        {
            return input.Trim().ToUpper();
        }
        public string GenerateSalarySlip(int empNo, int pageNumber, string takenBy, DateTime currentDate, DateTime fromDateValue, DateTime toDateValue, string fromDateFormatted, string toDateFormatted)
        {
            string branchName = GetBranchName();
            SqlConnection conn = new SqlConnection(connStr);
            EmployeeBO employeeBO = new EmployeeBO();
            string nextMonthAndYear = GetNextMonthAndYear();
            string[] parts = nextMonthAndYear.Split('/');
            int month = int.Parse(parts[0]);
            int year = int.Parse(parts[1]);

            decimal totalCurrentBasic = 0;
            decimal totalDA = 0;
            decimal totalHRA = 0;
            decimal totalCCA = 0;
            decimal totalCovAllowance = 0;
            decimal totalAdAllowance = 0;
            decimal totalOtherEarnings = 0;
            decimal totalGrossEarnings = 0;
            decimal totalProfTax = 0;
            decimal totalPF = 0;
            decimal totalIT = 0;
            decimal totalLIC = 0;
            decimal totalOtherDeduction = 0;
            decimal totalGrossDeduction = 0;
            decimal totalNetSalary = 0;


            string currentTime = DateTime.Now.ToString("dd-MM-yyyy");

            conn.Open();
            using (SqlCommand command = new SqlCommand("HRMS_EmployeeReportByDate", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "S");
                command.Parameters.AddWithValue("@Emp_No", empNo);
                command.Parameters.AddWithValue("@FromMonth", fromDateValue.Month);
                command.Parameters.AddWithValue("@FromYear", fromDateValue.Year);
                command.Parameters.AddWithValue("@ToMonth", toDateValue.Month);
                command.Parameters.AddWithValue("@ToYear", toDateValue.Year);


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
                command.Parameters.AddWithValue("@Emp_No", empNo);
                command.Parameters.AddWithValue("@FromMonth", fromDateValue.Month);
                command.Parameters.AddWithValue("@FromYear", fromDateValue.Year);
                command.Parameters.AddWithValue("@ToMonth", toDateValue.Month);
                command.Parameters.AddWithValue("@ToYear", toDateValue.Year);
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
                            case "OTHERS":
                                employeeBO.OtherDeduction = employeeBO.CuTrAmt;
                                break;

                            default:
                                break;
                        }
                    }
                }

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Action", "D");
                command.Parameters.AddWithValue("@Emp_No", empNo);
                command.Parameters.AddWithValue("@FromMonth", fromDateValue.Month);
                command.Parameters.AddWithValue("@FromYear", fromDateValue.Year);
                command.Parameters.AddWithValue("@ToMonth", toDateValue.Month);
                command.Parameters.AddWithValue("@ToYear", toDateValue.Year);

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
                                break;
                            case "CONVEYANCE":
                                employeeBO.CovAllownance = employeeBO.CuTrAmt;
                                break;
                            case "ADHOC ALLOWANCE":
                                employeeBO.AdAllownance = employeeBO.CuTrAmt;
                                break;
                            case "OTHER EARNINGS":
                                employeeBO.otherEarning = employeeBO.CuTrAmt;
                                break;
                            case "CCA":
                                employeeBO.CCA = employeeBO.CuTrAmt;
                                break;

                            default:
                                break;
                        }
                    }
                }


            }

            decimal totalEarnings = employeeBO.CurrentBasic + employeeBO.HRA + employeeBO.DA;// employeeBO.TechnicalAllowance;


            decimal totalDeductions = employeeBO.PF + employeeBO.IT + employeeBO.ProofTax + employeeBO.FESTADVRECOVERY2022;
            decimal GrossEarning = totalEarnings;
            decimal GrossDedcut = totalDeductions;

            decimal netSalary = totalEarnings - totalDeductions;


            totalCurrentBasic += employeeBO.CurrentBasic;
            totalDA += employeeBO.DA;
            totalHRA += employeeBO.HRA;
            totalCCA += employeeBO.CCA;
            totalCovAllowance += employeeBO.CovAllownance;
            totalAdAllowance += employeeBO.AdAllownance;
            totalOtherEarnings += employeeBO.otherEarning;
            totalGrossEarnings += totalEarnings;
            totalProfTax += employeeBO.ProofTax;
            totalPF += employeeBO.PF;
            totalIT += employeeBO.IT;
            totalLIC += employeeBO.LIC;
            totalOtherDeduction += employeeBO.OtherDeduction;
            totalGrossDeduction += totalDeductions;
            totalNetSalary += netSalary;


            string rootDirectoryPath = HttpContext.Current.Server.MapPath("~/App_Data/Report"); 

            string fileName = "SalaryRegisterEmployee.txt"; 

          
            string filePath = Path.Combine(rootDirectoryPath, fileName);
          


            using (StreamWriter salarySlip = new StreamWriter(filePath))
            {


                salarySlip.WriteLine($"{Tab(1)}{TrimAndUppercase(branchName)}{Tab(96)}");
                salarySlip.WriteLine($"{Tab(1)}-----------Salary Register of {fromDateFormatted} to {toDateFormatted}----------------------------------------");
                salarySlip.WriteLine($"{Tab(1)}{"".PadLeft(47)}                                                 PageNumber      : {pageNumber}");
                salarySlip.WriteLine($"{Tab(1)}{"".PadLeft(47)}                                                 TakenBy      : {takenBy}");
                salarySlip.WriteLine($"{Tab(1)}{"".PadLeft(47)}                                                 Date      : {currentDate}");
                salarySlip.WriteLine($"{Tab(1)}Employee No:{(employeeBO.EmpNo)}");
                salarySlip.WriteLine($"{Tab(1)}Employee Name:{(employeeBO.EmpName)}");
                salarySlip.WriteLine($"{Tab(1)}--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                salarySlip.WriteLine($"{Tab(1)} Month         Year        Basic      DA               HRA        CCA          Conv         Adhoc         PF         PT        LIC          IT          OtherEarning        OtherDeduction      GrrossEarning        GrossDeduction        NetSalary   ");
                salarySlip.WriteLine($"{Tab(1)}--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                salarySlip.WriteLine($"{Tab(1)} {month}             {year}       {employeeBO.CurrentBasic}    {employeeBO.DA}        {employeeBO.HRA}      {employeeBO.CCA}             {employeeBO.CovAllownance}           {employeeBO.AdAllownance}           {employeeBO.PF}     {employeeBO.ProofTax}     {employeeBO.LIC}           {employeeBO.IT}            {employeeBO.otherEarning}                   {employeeBO.OtherDeduction}                    {GrossEarning}            {GrossDedcut}             {netSalary}");
                salarySlip.WriteLine($"{Tab(1)}--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                salarySlip.WriteLine($"{Tab(1)} Total:                   {totalCurrentBasic}    {totalDA}        {totalHRA}      {totalCCA}             {totalCovAllowance}           {totalAdAllowance}           {totalPF}      {totalProfTax}    { totalLIC}           {totalIT}            {totalOtherEarnings}                   { totalOtherDeduction}                    {totalGrossEarnings}            {totalGrossDeduction}             {totalNetSalary} ");
                salarySlip.WriteLine($"{Tab(1)}--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                // salarySlip.AppendLine($"{Tab(1)}--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");





            }

            
            return rootDirectoryPath;

        }
        public DataTable SalaryRegisterForemployee(EmployeeBO employeeBO)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlDataAdapter dAd = new SqlDataAdapter("Sp_SalRegEmpRepo", conn);
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
            dAd.SelectCommand.Parameters.Add("@EmpNo", SqlDbType.Int).Value = employeeBO.EmpNo;
            dAd.SelectCommand.Parameters.Add("@FromDate", SqlDbType.Date).Value = employeeBO.FromDate;
            dAd.SelectCommand.Parameters.Add("@ToDate", SqlDbType.Date).Value = employeeBO.ToDate;
            DataSet dSet = new DataSet();
            try
            {
                dAd.Fill(dSet, "HRMS_SALARY_REGISTER_EMPLOYEE");
                return dSet.Tables["HRMS_SALARY_REGISTER_EMPLOYEE"];
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
        public void nextProcessDate()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("GetBranchLastProcessDates", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string cname = reader["Br_Name"].ToString();
                                DateTime nextProcessDate = Convert.ToDateTime(reader["Dt"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting branch names: " + ex.Message);
            }


        }
        public DataTable GetEmployeeDetails()
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand("sp_SalaryRegEmpDet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable employeeDetails = new DataTable();
                        adapter.Fill(employeeDetails);
                        return employeeDetails;
                    }
                }
            }
        }
        public DataTable GetEmployee()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("GetEmployeeDetailsByEmp_No", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

    }
}