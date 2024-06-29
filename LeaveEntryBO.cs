using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LeaveEntryBO
    {
        public LeaveEntryBO()
        {
            Action = string.Empty;
            Lea_Ty = string.Empty;
            Reason = string.Empty;
            Sanction = string.Empty;

        }
        public string Action { get; set; }
        public Int32 Emp_No { get; set; }
        public Int32 Year { get; set; }
        public Int32 Sl_No { get; set; }
        public string Reason { get; set; }
        public string Sanction { get; set; }
        public string Lea_Ty { get; set; }
        public Int32 Lea_Sanc { get; set; }
        public Int32 Code { get; set; }
        public Int32 Days { get; set; }

        public Int16 SV { get; set; }
        public DateTime AppDt { get; set; }
        public DateTime ToDt { get; set; }
        public DateTime FrmDt { get; set; }
        public string BR_NAME { get; set; }
        public string BR_ADD1 { get; set; }
        public string BR_ADD2 { get; set; }
        public string BR_ADD3 { get; set; }
        public string BR_CITY { get; set; }
        public int BR_PIN { get; set; }
        public string LPARAM_REF { get; set; }
        public int REFNO { get; set; }
        public int STRYR { get; set; }
        public int PLBALANCE { get; set; }
        //----------------------------------

        public int LvApp_SlNo { get; set; }
        public int LvApp_EmpNo { get; set; }
        public int LvApp_Code { get; set; }
        public DateTime LvApp_AppDate { get; set; }
        public DateTime LvApp_FromDt { get; set; }
        public DateTime LvApp_ToDt { get; set; }
        public int LvApp_Days { get; set; }
        public string LvApp_Sanctioned { get; set; }
        public int  LvApp_LeaveSanc { get; set; }
        public string LvApp_Reason { get; set; }
        public string LvApp_PLSL { get; set; }
        public int  LvApp_Eby { get; set; }
        public DateTime LvApp_Crdate { get; set; }


    }
}
