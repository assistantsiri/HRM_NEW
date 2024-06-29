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
    public class GetEmployeeDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        public GetEmployeeDA()
        { }
        public DataTable FetchGetLedgerEmployeesData(SalarySlip salarySlip)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlDataAdapter dAd = new SqlDataAdapter("GetEmployeeLedgerDetails", conn);
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
            dAd.SelectCommand.Parameters.Add("@Cu_Tr_EmpNo", SqlDbType.Int).Value = salarySlip.CUTRNO;
            DataSet dSet = new DataSet();


            try
            {
                dAd.Fill(dSet, "EmployeeLedgerDetails");
                return dSet.Tables["EmployeeLedgerDetails"];
            }

            catch
            {

                throw;
            }
              
        }

        public DataTable FetchGetEmployeeDeductionDetails(SalarySlip salarySlip)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlDataAdapter dAd = new SqlDataAdapter("GetEmployeeDeductionDetails", conn);
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
            dAd.SelectCommand.Parameters.Add("@Cu_Tr_EmpNo", SqlDbType.Int).Value = salarySlip.CUTRNO;
            DataSet dSet = new DataSet();


            try
            {
                dAd.Fill(dSet, "GetEmployeeDeductionDetails");
                return dSet.Tables["GetEmployeeDeductionDetails"];
            }

            catch
            {

                throw;
            }

        }



        public void GetEmployeeDeductionDetails(int employeeNumber)
       {
        string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("GetEmployeeDeductionDetails", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Cu_Tr_EmpNo", employeeNumber);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                            int cu_Tr_Code = reader.GetInt32(reader.GetOrdinal("Cu_Tr_Code"));
                            int cu_Tr_Month = reader.GetInt32(reader.GetOrdinal("Cu_Tr_Month"));
                            int cu_Tr_Year = reader.GetInt32(reader.GetOrdinal("Cu_Tr_Year"));
                            string cu_Tr_Payable = reader.GetString(reader.GetOrdinal("Cu_Tr_Payable"));
                            decimal cuAndArr = reader.GetDecimal(reader.GetOrdinal("CuAndArr")); // Assuming CuAndArr is of decimal type
                            string ed_Desc = reader.GetString(reader.GetOrdinal("ED_Desc"));
                        }
                }
            }
        }
    }


    public void GetEmployeeLedgerDetails(int employeeNumber)
       {
        string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("GetEmployeeLedgerDetails", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Cu_Tr_EmpNo", employeeNumber);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                            int cu_Tr_Code = reader.GetInt32(reader.GetOrdinal("Cu_Tr_Code"));
                            int cu_Tr_Month = reader.GetInt32(reader.GetOrdinal("Cu_Tr_Month"));
                            int cu_Tr_Year = reader.GetInt32(reader.GetOrdinal("Cu_Tr_Year"));
                            string cu_Tr_Payable = reader.GetString(reader.GetOrdinal("Cu_Tr_Payable"));
                            decimal cuAndArr = reader.GetDecimal(reader.GetOrdinal("CuAndArr")); // Assuming CuAndArr is of decimal type
                            string ed_Desc = reader.GetString(reader.GetOrdinal("ED_Desc"));

                        }
                    }
            }
        }
       }

    }
}
