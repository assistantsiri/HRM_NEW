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
    public class UserProfileDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();

        

        public DataTable FetchUserMast(UserProfileBO UserMast)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlDataAdapter dAd = new SqlDataAdapter("GetUserProfile", conn);
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
            dAd.SelectCommand.Parameters.Add("@V_Userid", SqlDbType.VarChar).Value = UserMast.UserId;
            dAd.SelectCommand.Parameters.Add("@V_PSW", SqlDbType.VarChar).Value = UserMast.Pwd;
            dAd.SelectCommand.Parameters.Add("@Action", SqlDbType.Char).Value = UserMast.Action;
            //dAd.SelectCommand.Parameters.Add("V_CUR", SqlDbType.).Direction = ParameterDirection.Output;
            DataSet dSet = new DataSet();
            try
            {
                dAd.Fill(dSet, "HRMS_USER_MAST2");
                return dSet.Tables["HRMS_USER_MAST2"];
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

    public class EmpDetailsDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        public DataTable FetchEmpDetails(EmpDetailsBO EmpMast)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlDataAdapter dAd = new SqlDataAdapter("FetchEmpDetails", conn);
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
            dAd.SelectCommand.Parameters.Add("@Emp_No", SqlDbType.Int).Value = EmpMast.Emp_No;
            dAd.SelectCommand.Parameters.Add("@MCode", SqlDbType.Int).Value = EmpMast.MCode;
            dAd.SelectCommand.Parameters.Add("@SV", SqlDbType.Int).Value = EmpMast.SV;
            dAd.SelectCommand.Parameters.Add("@Emp_Name", SqlDbType.VarChar).Value = EmpMast.Emp_Name;
            dAd.SelectCommand.Parameters.Add("@Action", SqlDbType.Char).Value = EmpMast.Action;
            DataSet dSet = new DataSet();
            try
            {
                dAd.Fill(dSet, "Hrm_Emp_Master");
                return dSet.Tables["Hrm_Emp_Master"];
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

        public string AUDEmpDetails(EmpDetailsBO EmpMast)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("AUDEmpDetails", conn);
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Emp_No", SqlDbType.Int).Value = EmpMast.Emp_No;
            cmd.Parameters.Add("@Emp_Name", SqlDbType.VarChar).Value = EmpMast.Emp_Name;
            cmd.Parameters.Add("@Emp_Sex", SqlDbType.Char).Value = EmpMast.Emp_Sex;
            cmd.Parameters.Add("@Emp_Desig", SqlDbType.TinyInt).Value = EmpMast.Emp_Desig;
            cmd.Parameters.Add("@Emp_DOB", SqlDbType.Date).Value = EmpMast.Emp_DOB;
            cmd.Parameters.Add("@Emp_Dept", SqlDbType.TinyInt).Value = EmpMast.Emp_Dept;
            cmd.Parameters.Add("@Emp_JoinDt", SqlDbType.Date).Value = EmpMast.Emp_JoinDt;
            cmd.Parameters.Add("@Emp_ConfDt", SqlDbType.Date).Value = EmpMast.Emp_ConfDt;
            cmd.Parameters.Add("@Emp_IncrementDt", SqlDbType.Date).Value = EmpMast.Emp_IncrementDt;
            cmd.Parameters.Add("@Emp_PromotionDt", SqlDbType.Date).Value = EmpMast.Emp_PromotionDt;
            cmd.Parameters.Add("@Emp_Marital_Status", SqlDbType.Char).Value = EmpMast.Emp_Marital_Status;
            //cmd.Parameters.Add("@Emp_Children", SqlDbType.TinyInt).Value = EmpMast.Emp_Children;
            //cmd.Parameters.Add("@Emp_Child_Allow", SqlDbType.Char).Value = EmpMast.Emp_Child_Allow;
            cmd.Parameters.Add("@Emp_Grade", SqlDbType.Int).Value = EmpMast.Emp_Grade;

            cmd.Parameters.Add("@Emp_PF_Nom", SqlDbType.VarChar).Value = EmpMast.Emp_PF_Nom;
            cmd.Parameters.Add("@Emp_PF_No", SqlDbType.VarChar).Value = EmpMast.Emp_PF_No;
            cmd.Parameters.Add("@Emp_EPS_No", SqlDbType.VarChar).Value = EmpMast.Emp_EPS_No;
            cmd.Parameters.Add("@Emp_OT_Ind", SqlDbType.Char).Value = EmpMast.Emp_OT_Ind;
            cmd.Parameters.Add("@Emp_SalBank", SqlDbType.SmallInt).Value = EmpMast.Emp_SalBank;
            cmd.Parameters.Add("@Emp_SalBranch", SqlDbType.SmallInt).Value = EmpMast.Emp_SalBranch;
            cmd.Parameters.Add("@Emp_AccNo", SqlDbType.VarChar).Value = EmpMast.Emp_AccNo;
            cmd.Parameters.Add("@Emp_Res_Add1", SqlDbType.VarChar).Value = EmpMast.Emp_Res_Add1;
            cmd.Parameters.Add("@Emp_Res_Add2", SqlDbType.VarChar).Value = EmpMast.Emp_Res_Add2;
            cmd.Parameters.Add("@Emp_Res_Add3", SqlDbType.VarChar).Value = EmpMast.Emp_Res_Add3;
            cmd.Parameters.Add("@Emp_Res_City", SqlDbType.VarChar).Value = EmpMast.Emp_Res_City;
            cmd.Parameters.Add("@Emp_Res_State", SqlDbType.TinyInt).Value = EmpMast.Emp_Res_State;
            cmd.Parameters.Add("@Emp_Res_pin", SqlDbType.VarChar).Value = EmpMast.Emp_Res_pin;
            cmd.Parameters.Add("@Emp_PAdd1", SqlDbType.VarChar).Value = EmpMast.Emp_PAdd1;
            cmd.Parameters.Add("@Emp_PAdd2", SqlDbType.VarChar).Value = EmpMast.Emp_PAdd2;
            cmd.Parameters.Add("@Emp_PAdd3", SqlDbType.VarChar).Value = EmpMast.Emp_PAdd3;
            cmd.Parameters.Add("@Emp_PCity", SqlDbType.VarChar).Value = EmpMast.Emp_PCity;
            cmd.Parameters.Add("@Emp_PState", SqlDbType.TinyInt).Value = EmpMast.Emp_PState;
            cmd.Parameters.Add("@Emp_Ppin", SqlDbType.VarChar).Value = EmpMast.Emp_Ppin;
            cmd.Parameters.Add("@Emp_Accomadation", SqlDbType.Char).Value = EmpMast.Emp_Accomadation;
            cmd.Parameters.Add("@Emp_House_Rpaid", SqlDbType.Decimal).Value = EmpMast.Emp_House_Rpaid;
            cmd.Parameters.Add("@Emp_City_Class", SqlDbType.SmallInt).Value = EmpMast.Emp_City_Class;
            cmd.Parameters.Add("@Emp_LTC_Availed", SqlDbType.TinyInt).Value = EmpMast.Emp_LTC_Availed;
            cmd.Parameters.Add("@Emp_VPF_Contri", SqlDbType.Decimal).Value = EmpMast.Emp_VPF_Contri;
            cmd.Parameters.Add("@Emp_Vehicle", SqlDbType.TinyInt).Value = EmpMast.Emp_Vehicle;
            cmd.Parameters.Add("@Emp_Deput_Indi", SqlDbType.Char).Value = EmpMast.Emp_Deput_Indi;
            cmd.Parameters.Add("@Emp_Stop_Pay", SqlDbType.Char).Value = EmpMast.Emp_Stop_Pay;
            
            cmd.Parameters.Add("@Emp_Branch", SqlDbType.SmallInt).Value = EmpMast.Emp_Branch;
            cmd.Parameters.Add("@Emp_StateCode", SqlDbType.SmallInt).Value = EmpMast.Emp_StateCode;
            cmd.Parameters.Add("@Emp_EMailID", SqlDbType.VarChar).Value = EmpMast.Emp_EMailID;
            cmd.Parameters.Add("@Emp_Metro", SqlDbType.Char).Value = EmpMast.Emp_Metro;
            cmd.Parameters.Add("@Emp_CrDt", SqlDbType.Date).Value = EmpMast.Emp_CrDt;
            cmd.Parameters.Add("@Emp_PAN", SqlDbType.VarChar).Value = EmpMast.Emp_PAN;
            cmd.Parameters.Add("@Emp_qualification", SqlDbType.VarChar).Value = EmpMast.Emp_qualification;
            cmd.Parameters.Add("@Emp_Father_name", SqlDbType.VarChar).Value = EmpMast.Emp_Father_name;
            cmd.Parameters.Add("@Emp_Spouse_name", SqlDbType.VarChar).Value = EmpMast.Emp_Spouse_name;
            
            cmd.Parameters.Add("@Emp_UAN", SqlDbType.VarChar).Value = EmpMast.Emp_UAN;
            cmd.Parameters.Add("@Emp_AadharNo", SqlDbType.VarChar).Value = EmpMast.Emp_AadharNo;
            cmd.Parameters.Add("@Action", SqlDbType.Char).Value = EmpMast.Action;
            cmd.Parameters.Add("@Result", SqlDbType.VarChar,80).Direction = ParameterDirection.Output;
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

        public string DelEmpDetails(EmpDetailsBO EmpMast)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("DelEmpDetails", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Emp_No", SqlDbType.Int).Value = EmpMast.Emp_No;
            cmd.Parameters.Add("@Result", SqlDbType.VarChar,50).Direction = ParameterDirection.Output;
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

       
    }
}