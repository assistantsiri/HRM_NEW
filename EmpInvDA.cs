using BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class EmpInvDA
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSConnectionString"].ToString();



        public DataTable FetchEmp(EmpInvBO emp)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlDataAdapter dAd = new SqlDataAdapter("HRMS_Emp_Inv", conn);
            dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
            dAd.SelectCommand.Parameters.Add("@Emp_Name", SqlDbType.VarChar).Value = emp.Emp_Name;
            dAd.SelectCommand.Parameters.Add("@Action", SqlDbType.Char).Value = emp.Action;

            DataSet dSet = new DataSet();
            try
            {
                dAd.Fill(dSet, "HRMS_EmpInv_Mast");
                return dSet.Tables["HRMS_EmpInv_Mast"];
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
        public DataTable GetSectionCodes()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand("SECTIONCODE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return dataTable;
        }
        public DataTable GetEmpDet()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand("EMPLOYEEDETAILS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return dataTable;
        }

        public DataTable GetSubSections()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand("SUBSECTIONCODE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // command.Parameters.Add(new SqlParameter("@Sub_MCode", subMCode));

                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return dataTable;
        }

        public void InsertEmployee(EmployeeInvestment employeeInvestment)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EMPLOYEEINVESTMENT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", "CREATE");
                    cmd.Parameters.AddWithValue("@EMPINV_EMPNO", employeeInvestment.EmpNo);
                    cmd.Parameters.AddWithValue("@EMPINV_MCODE", employeeInvestment.MCode);
                    cmd.Parameters.AddWithValue("@EMPINV_SCODE", employeeInvestment.SCode);
                    cmd.Parameters.AddWithValue("@EMPINV_SUBNO", employeeInvestment.SubNo);
                    cmd.Parameters.AddWithValue("@EMPINV_AMOUNT", employeeInvestment.Amount);
                    cmd.Parameters.AddWithValue("@EMPINV_DATE", employeeInvestment.Date);
                    cmd.Parameters.AddWithValue("@EMPINV_ITCODE", employeeInvestment.ITCode);
                    cmd.Parameters.AddWithValue("@EMPINV_REMARKS", employeeInvestment.Remarks);
                    //cmd.Parameters.AddWithValue("", );
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public int InsertEmployee1(EmployeeInvestment investment)
        {
            int empInvSlno = 0;


            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EMPLOYEEINVESTMENT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", investment.Action);
                    cmd.Parameters.AddWithValue("@EMPINV_EMPNO", investment.EmpNo);
                    cmd.Parameters.AddWithValue("@EMPINV_MCODE", investment.MCode);
                    cmd.Parameters.AddWithValue("@EMPINV_SCODE", investment.SCode);
                    cmd.Parameters.AddWithValue("@EMPINV_SUBNO", investment.SubNo);
                    cmd.Parameters.AddWithValue("@EMPINV_AMOUNT", investment.Amount);
                    cmd.Parameters.AddWithValue("@EMPINV_DATE", investment.Date);
                    cmd.Parameters.AddWithValue("@EMPINV_REMARKS", investment.Remarks);
                    cmd.Parameters.AddWithValue("@EMPINV_ITCODE", investment.ITCode);

                    //SqlParameter outputSlno = new SqlParameter("@EMPINV_SLNO", SqlDbType.Int)
                    //{
                    //    Direction = ParameterDirection.Output
                    //};
                    //cmd.Parameters.Add(outputSlno);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    //empInvSlno = Convert.ToInt32(outputSlno.Value);
                }
            }

            return empInvSlno;
        }




        public DataTable GetEmployeeInvestments(int employeeNumber)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_GetEmployeeInvestments", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EMPINV_EMPNO", employeeNumber);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public void EditEmployeeInvestment(EmployeeInvestment investment)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_EMPLOYEEINVESTMENT", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                //command.Parameters.AddWithValue("@EMPINV_EMPNO", employeeInvestment.EmpNo);
                //command.Parameters.AddWithValue("@EMPINV_SCODE", employeeInvestment.SCode);
                //command.Parameters.AddWithValue("@EMPINV_AMOUNT", employeeInvestment.Amount);
                //command.Parameters.AddWithValue("@EMPINV_DATE", employeeInvestment.Date);
                //command.Parameters.AddWithValue("@EMPINV_REMARKS", employeeInvestment.Remarks);
                //command.Parameters.AddWithValue("@ACTION", "EDIT");
                cmd.Parameters.AddWithValue("@ACTION", investment.Action);
                cmd.Parameters.AddWithValue("@EMPINV_EMPNO", investment.EmpNo);
                cmd.Parameters.AddWithValue("@EMPINV_MCODE", investment.MCode);
                cmd.Parameters.AddWithValue("@EMPINV_SCODE", investment.SCode);
                cmd.Parameters.AddWithValue("@EMPINV_SUBNO", investment.SubNo);
                cmd.Parameters.AddWithValue("@EMPINV_AMOUNT", investment.Amount);
                cmd.Parameters.AddWithValue("@EMPINV_DATE", investment.Date);
                cmd.Parameters.AddWithValue("@EMPINV_REMARKS", investment.Remarks);
                cmd.Parameters.AddWithValue("@EMPINV_ITCODE", investment.ITCode);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

      

    }
}
