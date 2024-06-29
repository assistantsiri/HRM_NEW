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
   public class Special_Earning_And_DeductionDA
    {
        string constr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        Special_Earn_And_DedBO special_Earn_And_DedBO = new Special_Earn_And_DedBO();
        public DataTable GetEmployees()
        {
            DataTable dtEmployees = new DataTable();

            try
            {

                using (SqlConnection connection = new SqlConnection(constr))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_EmpDet", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dtEmployees);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error retrieving employee data: " + ex.Message);
            }

            return dtEmployees;
        }
        public DataTable GetEmployeeSpecialEarnDed(int empNo)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_Special_EarnDed", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SpED_EmpNo", empNo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public void InsertSpecialEarningDeduction(Special_Earn_And_DedBO special_Earn_And_DedBO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    SqlCommand command = new SqlCommand("InsertSpecialEarningDeduction", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@SpED_EmpNo", special_Earn_And_DedBO.EmpNo);  // Changed from @@SpED_EmpNo to @SpED_EmpNo
                    command.Parameters.AddWithValue("@SpED_Ind", special_Earn_And_DedBO.SpEDInd);
                    command.Parameters.AddWithValue("@SpED_Code", special_Earn_And_DedBO.Code);
                    command.Parameters.AddWithValue("@SpED_Amt", special_Earn_And_DedBO.SpEDAmt);
                    command.Parameters.AddWithValue("@SpED_Payable", special_Earn_And_DedBO.SpEDPayable);
                    command.Parameters.AddWithValue("@SpED_NoDays", special_Earn_And_DedBO.SpEDNoDays);
                    command.Parameters.AddWithValue("@SpED_ProcessDt", special_Earn_And_DedBO.SpEDProcessDt);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("Rows affected: " + rowsAffected);  // Log number of rows affected

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No rows affected. Insert operation failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);  // Log exception message
                throw;  // Rethrow the exception to propagate it up the call stack
            }
        }



    }
}
