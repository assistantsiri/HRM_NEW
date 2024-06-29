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
   public class SalaryUpdatationDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        public DataTable FetchSalaryUpdate()
        {
            DataTable dataTable = new DataTable();

            SqlConnection con = new SqlConnection(connStr);
            con.Open();

            SqlCommand command = new SqlCommand("SalaryUpdateProcessing", con);
            command.CommandType = CommandType.StoredProcedure;

            //SqlDataAdapter dAd = new SqlDataAdapter("SalaryUpdateProcessing", con);
            //dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
           

            //DataSet dSet = new DataSet();
            try
            {
                command.ExecuteNonQuery();
                //dAd.Fill(dSet, "HRMS_Salary_Update");
                //return dSet.Tables["HRMS_Salary_Update"];
            }
            catch(Exception error)
            {
                var ErrorMessage = error.Message;
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dataTable;
        }
    }
}
