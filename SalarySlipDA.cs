using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace DAL
{
    public class SalarySlipDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();
        public SalarySlipDA() { }

        public DataTable FetchEmployeesData(EmployeeBO employeeBO)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter dAd = new SqlDataAdapter("GetEmployeesData", conn);
                dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
                dAd.SelectCommand.Parameters.Add("@BranchCode", SqlDbType.Int).Value = employeeBO.BranchCode;
                dAd.SelectCommand.Parameters.Add("@FromEmp", SqlDbType.Int).Value = employeeBO.FromEmp;
                dAd.SelectCommand.Parameters.Add("@ToEmp", SqlDbType.Int).Value = employeeBO.ToEmp;
                DataSet dSet = new DataSet();
                try
                {
                    dAd.Fill(dSet, "HRMS_EMPLOYEES_DATA");
                    return dSet.Tables["HRMS_EMPLOYEES_DATA"];
                }
                catch
                {
                    throw;
                }
            }
        }

        public string FetchEmployeesDataAndWriteToFile(EmployeeBO employeeBO)
        {
                SqlConnection sqlConnection = new SqlConnection(connStr);
            
                SqlCommand command = new SqlCommand("GetEmployeesData", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@EmpNo", SqlDbType.Int).Value = employeeBO.EmpNo;
                command.Parameters.Add("@Cu_Tr_Code", SqlDbType.Int).Value = employeeBO.Cu_Tr_Code;
                command.Parameters.Add("@Cu_Tr_EmpNo", SqlDbType.Int).Value = employeeBO.Cu_Tr_EmpNo;
                command.Parameters.Add("@Cu_Tr_Year", SqlDbType.Int).Value = employeeBO.Cu_Tr_Year;
                command.Parameters.Add("@Cu_Tr_Month", SqlDbType.Int).Value = employeeBO.Cu_Tr_Month;
                command.Parameters.Add("@Cu_Tr_Payable", SqlDbType.VarChar).Value = employeeBO.Cu_Tr_Payable;
                command.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = employeeBO.EmpName;
                command.Parameters.Add("@Emp_Branch", SqlDbType.Int).Value = employeeBO.Emp_Branch;
                command.Parameters.Add("@Emp_Desig", SqlDbType.VarChar).Value = employeeBO.Emp_Desig;
                command.Parameters.Add("@ED_Desc", SqlDbType.VarChar).Value = employeeBO.ED_Desc;
                command.Parameters.Add("@fromNo", SqlDbType.Int).Value = employeeBO.fromNo;
                command.Parameters.Add("@ToValue", SqlDbType.Int).Value = employeeBO.ToNo;

                try
                {
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    return Convert.ToString(command.Parameters["@EmpName"].Value);
                }
                catch
                {
                    throw;
                }
            
        }
    }

}
