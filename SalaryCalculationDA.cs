using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BO;

namespace DA
{
    public class SalaryCalculationDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        public string SalCalcu()
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SalCalcu", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Result", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToString(cmd.Parameters["@Result"].Value);
            }
            catch
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }


        public string CurrentMonthYear()
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("GetNextMonthYear", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            string nextMonth = "";
            string nextYear = "";

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Get the month and year from the reader
                    nextMonth = reader["Mon"].ToString();
                    nextYear = reader["Yr"].ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection and dispose of objects
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }

            // Return the next month and year as a concatenated string
            return $"{nextMonth} - {nextYear}";
        }

    }

    //public class SalarySlipDA
    //{
    //    string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
    //    public DataTable FetchSalarySlip(SalarySlipBO salarySlip)
    //    {
    //        SqlConnection conn = new SqlConnection(connStr);
    //        conn.Open();
    //        SqlDataAdapter dAd = new SqlDataAdapter("HRMS_SalarySlipGen", conn);
    //        dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        dAd.SelectCommand.Parameters.Add("@Emp_No", SqlDbType.Int).Value = salarySlip.Emp_No;
    //        dAd.SelectCommand.Parameters.Add("@Action", SqlDbType.Char).Value = salarySlip.Action;
    //        DataSet dSet = new DataSet();
    //        try
    //        {
    //            dAd.Fill(dSet, "HRMS_SALARYSLIP");
    //            return dSet.Tables["HRMS_SALARYSLIP"];
    //        }
    //        catch
    //        {
    //            throw;
    //        }
    //        finally
    //        {
    //            dSet.Dispose();
    //            dAd.Dispose();
    //            conn.Close();
    //            conn.Dispose();
    //        }
    //    }
    //}

}
