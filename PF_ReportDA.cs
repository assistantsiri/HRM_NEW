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
    public class PF_ReportDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        EmployeeBO employeeBO = new EmployeeBO();
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
                                // cbrCode = reader["CBr_Code"].ToString();
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


        public DataTable GetEmpPFrepoByBranch(string branchName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("GetEmpPFrepoByBranch", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CBr_Name", branchName);
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting PF report data by branch: " + ex.Message);
            }
            return dt;
        }

        public string GetBranchName()
        {
            string branchName = "";
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
                                branchName = reader["Br_Name"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting branch details", ex);
            }
            return branchName;
        }


        private int GetBranchId(string branchName)
        {
            int branchId = -1;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();


                using (SqlCommand command = new SqlCommand("GetCBrCode", conn))
                {

                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@CBr_Name", branchName);


                    var result = command.ExecuteScalar();


                    if (result != null)
                    {

                        branchId = Convert.ToInt32(result);
                    }
                }
            }

            return branchId;
        }
        public Tuple<decimal, decimal> GetPFandEDLICharges()
        {
            decimal pfAdminCharges = 0;
            decimal edliCharges = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("GetPFandEDLICharges", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Read the values from the result set
                            pfAdminCharges = Convert.ToDecimal(reader["Br_PFAdminCharges"]);
                            edliCharges = Convert.ToDecimal(reader["Br_EDLICharges"]);
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting PF and EDLI charges", ex);
            }

            return Tuple.Create(pfAdminCharges, edliCharges);
        }
    }
   
}
