using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;

namespace BA
{
    public class SalarySlipEmailBL
    {
        public DataTable FetchDetails(SalarySlipEmailBO emp)
        {
            SalarySlipEmailDA pDA = new SalarySlipEmailDA();
            try
            {
                return pDA.FetchDetails(emp);
            }
            catch
            {
                throw;
            }
        }
    }
}
