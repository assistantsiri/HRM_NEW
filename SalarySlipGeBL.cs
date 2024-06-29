using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA
{
   public class SalarySlipGeBL
    {
        SalarySlipGeDA salarySlipGeDA = new SalarySlipGeDA();
        public List<SalarySlipGe> GetSalarySlip(string action, int fromEmpNo, int toEmpNo)
        {
            try
            {
                return salarySlipGeDA.GetSalarySlip(action, fromEmpNo, toEmpNo);
            }

            catch
            {
                throw;
            }
        }

    }
}
