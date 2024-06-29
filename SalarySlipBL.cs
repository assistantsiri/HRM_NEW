using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA
{
   public  class SalarySlipBL
    {
        public DataTable FetchEmployeeData(EmployeeBO employeeBO)
        {
            SalarySlipDA sdA = new SalarySlipDA();
            try
            {
                return sdA.FetchEmployeesData(employeeBO);
            }
            catch
            {
                throw;
            }
        }
       

    }
    public class EmpBL
    {
        public string FetchEmployeesDataAndWriteToFile(EmployeeBO employeeBO)
        {
            SalarySlipDA sdA = new SalarySlipDA();
            try
            {
                return sdA.FetchEmployeesDataAndWriteToFile(employeeBO);
            }
            catch
            {
                throw;
            }
        }
    }
}
