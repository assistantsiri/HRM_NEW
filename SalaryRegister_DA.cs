using BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SalaryRegister_DA
    {
        EmployeeBO employeeBO = new EmployeeBO();
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        private List<string> selectedBranches;
        public DataTable SalaryRegister(EmployeeBO employeeBO)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlDataAdapter dAd = new SqlDataAdapter("[sp_CurSalReg]", conn);
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
            dAd.SelectCommand.Parameters.Add("@BrCode", SqlDbType.SmallInt).Value = employeeBO.BranchCode;
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
        public void SetSelectedBranches(List<string> branches)
        {
            selectedBranches = branches;
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
        public List<string> BindBranches()
        {
            List<string> branchNames = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("GetCompBranches", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //string cbrCode = reader["CBr_Code"].ToString();
                                string cbrName = reader["CBr_Name"].ToString();
                                branchNames.Add(cbrName); // Add branch name to the list
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

    }
   

}
