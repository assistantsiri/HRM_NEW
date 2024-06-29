using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace DAL
{
    public class SalarySlipEmailDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        public DataTable FetchDetails(SalarySlipEmailBO emp)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlDataAdapter dad = new SqlDataAdapter("HRMS_SalarySlip_EMail", conn);
            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
            dad.SelectCommand.Parameters.Add("@Action", SqlDbType.Char).Value = emp.Action;
            dad.SelectCommand.Parameters.Add("@Branch", SqlDbType.SmallInt).Value = emp.Branches;
            dad.SelectCommand.Parameters.Add("@EmpNo", SqlDbType.Int).Value = emp.EmpNo;
            DataSet set = new DataSet();
            try
            {
                dad.Fill(set, "HRMS_Sal_Sl_Em");
                return set.Tables["HRMS_Sal_Sl_Em"];
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
    }
}
