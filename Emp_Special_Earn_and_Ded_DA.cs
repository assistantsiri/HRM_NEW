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
    public class Emp_Special_Earn_and_Ded_DA
    {
        string constr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        Special_Earn_And_DedBO special_Earn_And_DedBO = new Special_Earn_And_DedBO();
        public bool CheckEntryExists(Special_Earn_And_DedBO special_Earn_And_DedBO)
        {
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("sp_Special_Earn_Ded_Check", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmpNo", special_Earn_And_DedBO.EmpNo);
                    command.Parameters.AddWithValue("@Code", special_Earn_And_DedBO.Code);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return reader.HasRows;
                }
            }
        }

        public DataTable FetchSpecialEarningsDeductions(int empNo)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("sp_Special_EarnDed", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SpED_EmpNo", empNo));


                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Load the data into the DataTable
                        dataTable.Load(reader);
                    }
                }
            }

            return dataTable;
        }
        public void UpdateSpecialEarnDedWithSelect(Special_Earn_And_DedBO special_Earn_And_DedBO)
        {
            using (SqlConnection connection = new SqlConnection(constr))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SpecialEarn_Ded_Update", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        //command.Parameters.Add(new SqlParameter("@Action", SqlDbType.VarChar)).Value = special_Earn_And_DedBO.action;
                        command.Parameters.Add(new SqlParameter("@SpED_EmpNo", SqlDbType.Int)).Value = special_Earn_And_DedBO.EmpNo;
                        command.Parameters.Add(new SqlParameter("@SpED_Ind", SqlDbType.VarChar)).Value = special_Earn_And_DedBO.SpEDInd;
                        command.Parameters.Add(new SqlParameter("@SpED_Amt", SqlDbType.Decimal)).Value = special_Earn_And_DedBO.SpEDAmt;
                        command.Parameters.Add(new SqlParameter("@SpED_Payable", SqlDbType.VarChar)).Value = special_Earn_And_DedBO.SpEDPayable;
                        command.Parameters.Add(new SqlParameter("@SpED_Code", SqlDbType.Int)).Value = special_Earn_And_DedBO.Code;
                        command.Parameters.Add(new SqlParameter("@SpED_ProcessDt", SqlDbType.DateTime)).Value = special_Earn_And_DedBO.SpEDProcessDt;

                        // Execute the command
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions
                    Console.WriteLine($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Handle general exceptions
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        public void EditDAP(Special_Earn_And_DedBO special_Earn_And_DedBO)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Update_Special_EarnDed_With_Select", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SpED_EmpNo", special_Earn_And_DedBO.EmpNo);
                    cmd.Parameters.AddWithValue("@SpED_Ind", special_Earn_And_DedBO.ED);
                    cmd.Parameters.AddWithValue("@Code", special_Earn_And_DedBO.Code);
                    cmd.Parameters.AddWithValue("@SpED_Amt", special_Earn_And_DedBO.Amount);
                    cmd.Parameters.AddWithValue("@SpED_Payable", special_Earn_And_DedBO.SpEDPayable);
                    cmd.Parameters.AddWithValue("@SpED_ProcessDt", special_Earn_And_DedBO.SpEDProcessDt);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateSpecialEarnDed(int empNo, string ind, decimal amount, string payable, int code, DateTime processDate)
        {
            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlCommand command = new SqlCommand("Update_Special_EarnDed_With_Select", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SpED_EmpNo", empNo);
                command.Parameters.AddWithValue("@SpED_Ind", ind);
                command.Parameters.AddWithValue("@SpED_Amt", amount);
                command.Parameters.AddWithValue("@SpED_Payable", payable);
                command.Parameters.AddWithValue("@Code", code);
                command.Parameters.AddWithValue("@SpED_ProcessDt", processDate);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public DataTable GetEmployeeDetails( int SpED_EmpNo)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetSpEDDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SpED_EmpNo", SpED_EmpNo);

                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (log or rethrow as needed)
                        throw new Exception("Error fetching employee details: " + ex.Message);
                    }
                }
            }

            return dt;
        }


    }
}