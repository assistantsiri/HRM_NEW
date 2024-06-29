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
    public class SalarySlipGeDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        public List<SalarySlipGe> GetSalarySlip(string action, int fromEmpNo, int toEmpNo)
        {
            List<SalarySlipGe> entries = new List<SalarySlipGe>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("HRMS_SalarySlipGeneration", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", action);
                command.Parameters.AddWithValue("@FromEmpNo", fromEmpNo);
                command.Parameters.AddWithValue("@ToEmpNo", toEmpNo);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SalarySlipGe entry = new SalarySlipGe();
                        entry.EmpNo = Convert.ToInt32(reader["Emp_No"]);
                        entry.EmpName = reader["Emp_Name"].ToString();
                        entry.Designation = reader["Emp_Desig"].ToString();
                        entry.ED_Description = GetDescriptionFromCode(Convert.ToInt32(reader["Cu_Tr_Code"]));
                        entry.Amount = Convert.ToDecimal(reader["Cu_Tr_Amt"]);
                        entry.Code = Convert.ToInt32(reader["Cu_Tr_Code"]);
                        entries.Add(entry);
                    }
                }


            }
            return entries;
        }
        private string GetDescriptionFromCode(int code)
        {
            switch (code)
            {
                case 1001:
                    return "BasicPay";
                case 1002:
                    return "DA";
                case 1003:
                    return "HRA";
                case 5001:
                    return "PF";
                case 5002:
                    return "P.TAX";
                case 5006:
                    return "INCOME TAX";
                case 6030:
                    return "FEST ADV RECOVERY";
                
                default:
                    return "Unknown";
            }
        }


    }

}
