using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA;
using System.Data;
using BO;

namespace BL
{
    public class SalaryCalculationBL
    {
        public string SalCalcu()
        {
            SalaryCalculationDA dA = new SalaryCalculationDA();
            try
            {
                return dA.SalCalcu();
            }
            catch
            {
                throw;
            }
        }
       
    }
    
}
