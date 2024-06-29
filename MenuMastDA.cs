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
    public class MenuMastDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();

        public MenuMastDA()
            {}
        public DataTable FetchMenuMast(MenuMastBO MenuMast)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlDataAdapter dAd = new SqlDataAdapter("FetchMenuItem", conn);                      
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
            dAd.SelectCommand.Parameters.Add("@V_OfficeTyp", SqlDbType.VarChar).Value = MenuMast.HMM_OFFICE_TYP;            
            dAd.SelectCommand.Parameters.Add("@Action", SqlDbType.Char).Value = MenuMast.Action;
            DataSet dSet = new DataSet();
            try
            {
                dAd.Fill(dSet, "HRMS_MENU_MAST");
                return dSet.Tables["HRMS_MENU_MAST"];
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
    }

}
