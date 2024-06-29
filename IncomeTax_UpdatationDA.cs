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
    public class IncomeTax_UpdatationDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        IncomeTax_UpdatationBO incomeTax_UpdatationBO = new IncomeTax_UpdatationBO();
        public DataTable FetchIncomeTax_Updatation(IncomeTax_UpdatationBO incomeTax_UpdatationBO)
        {
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlDataAdapter dAd = new SqlDataAdapter("Sp_IT_Entry_FillGrid", con);
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;


            DataSet dSet = new DataSet();
            try
            {
                dAd.Fill(dSet, "HRMS_IncomeTax_Updatation");
                return dSet.Tables["HRMS_IncomeTax_Updatation"];
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
        public void EditIT(incomeTAx incomeTAx)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("IT_Updation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SpED_Amt", SqlDbType.Decimal).Value = incomeTAx.MonthlyDeductionamt;
                    //cmd.Parameters.Add("@SpED_EmpNo", SqlDbType.Int).Value = incomeTAx.SpED_EmpNo;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateDAPoint(incomeTAx incomeTAx)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("IT_Updation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SpED_Amt", SqlDbType.Decimal).Value = incomeTAx.MonthlyDeductionamt;
                   // cmd.Parameters.Add("@SpED_EmpNo", SqlDbType.Int).Value = incomeTAx.SpED_EmpNo;

                    
                    cmd.ExecuteNonQuery();
                   
                }
            }
        }
    }
}
