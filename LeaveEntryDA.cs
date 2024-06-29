using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DA
{
    public class LeaveEntryDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        public DataTable FetchDetails(LeaveEntryBO leave)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlDataAdapter dad = new SqlDataAdapter("HRMS_LeaveParticulars", conn);
            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
            dad.SelectCommand.Parameters.Add("@Action", SqlDbType.Char).Value = leave.Action;
            dad.SelectCommand.Parameters.Add("@Sl_No", SqlDbType.Int).Value = leave.Sl_No;
            dad.SelectCommand.Parameters.Add("@SV", SqlDbType.SmallInt).Value = leave.SV;
            dad.SelectCommand.Parameters.Add("@Emp_No", SqlDbType.Int).Value = leave.Emp_No;
            dad.SelectCommand.Parameters.Add("@Year", SqlDbType.Int).Value = leave.Year;
            DataSet set = new DataSet();
            try
            {
                dad.Fill(set, "HRMSLeaveEntry");
                return set.Tables["HRMSLeaveEntry"];
            }
            catch
            {
                throw;
            }
            finally
            {
                set.Dispose();
                dad.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }
        public string AUDDetails(LeaveEntryBO leave)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("HRMS_LeaveParticulars_CRD", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Sl_No", SqlDbType.Int).Value = leave.Sl_No;
            cmd.Parameters.Add("@Action", SqlDbType.Char).Value = leave.Action;
            cmd.Parameters.Add("@Emp_No", SqlDbType.Int).Value = leave.Emp_No;
            cmd.Parameters.Add("@Code", SqlDbType.Int).Value = leave.Code;

            cmd.Parameters.Add("@AppDt", SqlDbType.Date).Value = leave.AppDt;
            cmd.Parameters.Add("@ToDt", SqlDbType.Date).Value = leave.ToDt;
            cmd.Parameters.Add("@FrmDt", SqlDbType.Date).Value = leave.FrmDt;
            cmd.Parameters.Add("@Days", SqlDbType.Int).Value = leave.Days;
            cmd.Parameters.Add("@LS", SqlDbType.SmallInt).Value = leave.Lea_Sanc;
            cmd.Parameters.Add("@LT", SqlDbType.Char).Value = leave.Lea_Ty;
            cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = leave.Reason;
            cmd.Parameters.Add("@Sanc", SqlDbType.Char).Value = leave.Sanction;
            cmd.Parameters.Add("@Result", SqlDbType.VarChar, 80).Direction = ParameterDirection.Output;
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

        public DataTable AUDDetail(LeaveEntryBO leave)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            // Use SqlDataAdapter and configure it for the stored procedure
            SqlDataAdapter dAd = new SqlDataAdapter("LEAVEREPORT", conn);
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add parameters to the SqlCommand

            dAd.SelectCommand.Parameters.Add("@Action", SqlDbType.Char).Value = leave.Action;
            dAd.SelectCommand.Parameters.Add("@LVAPP_EMPNO", SqlDbType.Int).Value = leave.Emp_No;
            dAd.SelectCommand.Parameters.Add("@LVAPP_FROMDT", SqlDbType.Date).Value = leave.FrmDt;
            // dAd.SelectCommand.Parameters.Add("@AppDt", SqlDbType.Date).Value = leave.AppDt;
            dAd.SelectCommand.Parameters.Add("@LVAPP_TODT", SqlDbType.Date).Value = leave.ToDt;
            // dAd.SelectCommand.Parameters.Add("@@LVAPP_FROMDT", SqlDbType.Date).Value = leave.FrmDt;

            DataSet dSet = new DataSet();

            try
            {
                // Fill the dataset with the result of the stored procedure
                dAd.Fill(dSet, "HRMS_LEAVE_PARTICULARS");

                // Return the result as a DataTable
                return dSet.Tables["HRMS_LEAVE_PARTICULARS"];
            }
            catch
            {
                // Rethrow any exceptions that occur
                throw;
            }
            finally
            {
                // Dispose of the dataset and data adapter, and close the connection
                dSet.Dispose();
                dAd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        public DataTable LEAVEREPORT(LeaveEntryBO leave)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            // Use SqlDataAdapter and configure it for the stored procedure
            SqlDataAdapter dAd = new SqlDataAdapter("LEAVEREPORT", conn);
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add parameters to the SqlCommand

           
            dAd.SelectCommand.Parameters.Add("@LVAPP_EMPNO", SqlDbType.Int).Value = leave.Emp_No;

            DataSet dSet = new DataSet();

            try
            {
                // Fill the dataset with the result of the stored procedure
                dAd.Fill(dSet, "HRMS_LEAVE_PARTICULARS");

                // Return the result as a DataTable
                return dSet.Tables["HRMS_LEAVE_PARTICULARS"];
            }
            catch
            {
                // Rethrow any exceptions that occur
                throw;
            }
            finally
            {
                // Dispose of the dataset and data adapter, and close the connection
                dSet.Dispose();
                dAd.Dispose();
                conn.Close();
                conn.Dispose();
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
