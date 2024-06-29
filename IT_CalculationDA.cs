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
    public class IT_CalculationDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        IT_WORKSHEETBO iT_WORKSHEETBO = new IT_WORKSHEETBO();
        public DataTable GetEmployeeTaxDetails()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EmpIT_TaxDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
        public bool IsCurrentMonthSalaryCalculated()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Sp_allDatafromHrm_Currentledger", conn))
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        public class IncomeTaxCalculator
        {
            public bool ValidateDate(string inputDate, DateTime calDt)
            {
                if (!DateTime.TryParseExact(inputDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _))
                {
                    return false;
                }
                else
                {
                    DateTime parsedDate = Convert.ToDateTime(inputDate);
                    return parsedDate.Month == calDt.Month && parsedDate.Year == calDt.Year;
                }
            }
        }
        public void ExecuteITCalculation(int empNo, DateTime salDate)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand("Sp_CalculateEmployeeIncomeTax", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmpNo", empNo);
                    command.Parameters.AddWithValue("@CalcDate", salDate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DataFromCureentLedger()
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_Hrm_CurrntLedger", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {

                            throw new Exception("EOF");
                        }
                    }
                }
            }
        }
        public List<int> CheckAndCalculate()
        {
            List<int> employeeNumbers = new List<int>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("CheckAndCalculate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeNumbers.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            return employeeNumbers;
        }

        public DataTable ITWorkSheetData(IT_WORKSHEETBO iT_WORKSHEETBO)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("ITWORKSHEET", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", iT_WORKSHEETBO.Action);
                    cmd.Parameters.AddWithValue("@Calc_IT_EmpNo", iT_WORKSHEETBO.EmpNo);

                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (log or rethrow as needed)
                        throw new Exception($"Error executing action {iT_WORKSHEETBO.Action}: " + ex.Message);
                    }
                }
            }

            return dt;
        }
        public DataTable GetEmployeeDetailsDataTable()
        {
            DataTable dataTable = new DataTable();

            // Define the columns for the DataTable
           

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand("GetEmpDet_ITWorkSheet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpNo", iT_WORKSHEETBO.EmpNo);
                    command.Parameters.AddWithValue("@EmpName", iT_WORKSHEETBO.EmpName);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create a new DataRow and add it to the DataTable
                            DataRow row = dataTable.NewRow();
                            row["EmpNo"] = reader.GetInt32(reader.GetOrdinal("Emp_No"));
                            row["EmpName"] = reader.GetString(reader.GetOrdinal("Emp_Name"));
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }

            return dataTable;
        }

    }
   
}

