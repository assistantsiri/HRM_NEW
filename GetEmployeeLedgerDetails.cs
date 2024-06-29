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
  public class GetEmployeeLedgerDetails
    {
        public DataTable FetchEmployeeLedgerDetails( SalarySlip salarySlip)
        {
            GetEmployeeDA getEmployeeDA = new GetEmployeeDA();
            try
            {
                return getEmployeeDA.FetchGetLedgerEmployeesData(salarySlip);

            }
            catch
            {
                throw;
            }
        }


    }
}
