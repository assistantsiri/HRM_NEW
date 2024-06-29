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
    public class DAP_DA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        DApointBO dApointBO = new DApointBO();

        public DataTable FetchDAPoint(DApointBO dApointBO)
        {
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlDataAdapter dAd = new SqlDataAdapter("sp_DAPointsFetchDetails", con);
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
            // dAd.SelectCommand.Parameters.Add("@FromDate", SqlDbType.Date).Value = dApointBO.DAP_FROM;
            //dAd.SelectCommand.Parameters.Add("@ToDate", SqlDbType.Date).Value = dApointBO.DAP_TO;

            DataSet dSet = new DataSet();
            try
            {
                dAd.Fill(dSet, "HRMS_DAPoint");
                return dSet.Tables["HRMS_DAPoint"];
            }
            catch
            {
                throw;
            }
            finally
            {
                dSet.Dispose();
                dAd.Dispose();
                con.Close();
                con.Dispose();
            }

        }
        public void EditDAP(DApointBO dApointBO)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_DAPointsDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = "EDIT";
                    cmd.Parameters.Add("@DAP_SlNo", SqlDbType.Int).Value = dApointBO.DAP_SINo;
                    cmd.Parameters.Add("@DAP_From", SqlDbType.Date).Value = dApointBO.DAP_FROM;
                    cmd.Parameters.Add("@DAP_To", SqlDbType.Date).Value = dApointBO.DAP_TO;
                    cmd.Parameters.Add("@DAP_Points", SqlDbType.Decimal).Value = dApointBO.DAP_Point;
                    cmd.Parameters.Add("@DAP_PER", SqlDbType.Decimal).Value = dApointBO.DAP_PER;
                    cmd.Parameters.Add("@DAP_CrDate", SqlDbType.Decimal).Value = dApointBO.DAP_CrDate;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void InsertDAPoint(DApointBO dAPBO)
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //SqlCommand getMaxSlNoCmd = new SqlCommand("SELECT ISNULL(MAX(DAP_SlNo), 0) FROM [Hrm_DA_Points]", conn);
                //conn.Open();
                //int maxSlNo = (int)getMaxSlNoCmd.ExecuteScalar();

                //// Increment the maximum DAP_SlNo by 1
                //dAPBO.DAP_SINo = maxSlNo + 1;
                SqlCommand cmd = new SqlCommand("sp_Hrm_DA_Points", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "CREATE");
                cmd.Parameters.AddWithValue("@DAP_SlNo", dAPBO.DAP_SINo); // Provide the value of DAP_SlNo if required
                cmd.Parameters.AddWithValue("@DAP_From", dAPBO.DAP_FROM);
                cmd.Parameters.AddWithValue("@DAP_To", dAPBO.DAP_TO);
                cmd.Parameters.AddWithValue("@DAP_Points", dAPBO.DAP_Point);
                cmd.Parameters.AddWithValue("@DAP_PER", dAPBO.DAP_PER);
                cmd.Parameters.AddWithValue("@DAP_CrDate", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

        }
        public void UpdateDAPoint(DApointBO dAPBO)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_Hrm_DA_Points", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@DAP_SlNo", dAPBO.DAP_SINo); // Provide the value of DAP_SlNo if required
                cmd.Parameters.AddWithValue("@DAP_From", dAPBO.DAP_FROM);
                cmd.Parameters.AddWithValue("@DAP_To", dAPBO.DAP_TO);
                cmd.Parameters.AddWithValue("@DAP_Points", dAPBO.DAP_Point);
                cmd.Parameters.AddWithValue("@DAP_PER", dAPBO.DAP_PER);
                cmd.Parameters.AddWithValue("@DAP_CrDate", dAPBO.DAP_CrDate);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteDAPoint(DApointBO dAPBO)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_ManageDAPoints", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@DAP_SlNo", dAPBO.DAP_SINo);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}

