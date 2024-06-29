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
    public class LeaveEntryBL
    {
        public DataTable FetchDetails(LeaveEntryBO leave)
        {
            LeaveEntryDA dA = new LeaveEntryDA();
            try
            {
                return dA.FetchDetails(leave);
            }
            catch
            {
                throw;
            }
        }
        public string AUDDetails(LeaveEntryBO leave)
        {
            LeaveEntryDA dA = new LeaveEntryDA();
            try
            {
                return dA.AUDDetails(leave);

            }
            catch
            {
                throw;
            }
        }
    }
}
