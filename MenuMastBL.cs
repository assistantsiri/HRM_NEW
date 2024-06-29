using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
using DA;
using System.Threading.Tasks;
using System.Data;


namespace BL
{
    public class MenuMastBL
    {
        public DataTable FetchMenuMast(MenuMastBO MenuMast)
        {
            MenuMastDA pDa = new MenuMastDA();
            try
            {
                return pDa.FetchMenuMast(MenuMast);
            }
            catch
            {
                throw;
            }
        

        }

    }
}
