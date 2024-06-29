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
    public class DAP_BL
    {
        public DataTable FetchDAPoints(DApointBO dApointBO)
        {
            DAP_DA dAP_DA = new DAP_DA();
            try
            {
                return dAP_DA.FetchDAPoint(dApointBO);

            }
            catch
            {
                throw;
            }
        }
    }
}
