using BO;
using DA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EmpInvBL
    {
        public DataTable FetchEmp(EmpInvBO emp)
        {
            EmpInvDA pDA = new EmpInvDA();
            try
            {
                return pDA.FetchEmp(emp);
            }
            catch
            {
                throw;
            }
        }
    }
}
