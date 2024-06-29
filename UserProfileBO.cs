using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class UserProfileBO
    {
        public UserProfileBO()
        {
            UserId = string.Empty;
            Pwd = string.Empty;
            UserName = string.Empty;
            Designation = string.Empty;
            Createddate = string.Empty;
            PWDexpdate = string.Empty;
            LoginFlag = char.MinValue;
            lastlogindate = string.Empty;
            Empid = string.Empty;
            Action = string.Empty;
        }
        public Int32 Dpcd { get; set; }
        public string UserId { get; set; }
        public string Pwd { get; set; }
        public string UserName { get; set; }
        public string Designation { get; set; }
        public Int16 UserStatus { get; set; }
        public Int16 UserMode { get; set; }
        public string Createddate { get; set; }
        public string PWDexpdate { get; set; }
        public char LoginFlag { get; set; }
        public string lastlogindate { get; set; }
        public string Empid { get; set; }
        public string Action { get; set; }
    }

    public class EmpDetailsBO
    {
        public EmpDetailsBO()
        {
            Emp_Name = string.Empty;
            Emp_Sex = char.MinValue;
            
            Emp_Marital_Status = char.MinValue;
            Emp_Child_Allow = char.MinValue;
            Emp_PF_Nom = string.Empty;
            Emp_PF_No = string.Empty;
            Emp_EPS_No = string.Empty;
            
            Emp_OT_Ind = char.MinValue;
            Emp_AccNo = string.Empty;
            Emp_Res_Add1 = string.Empty;
            Emp_Res_Add2 = string.Empty;
            Emp_Res_Add3 = string.Empty;
            Emp_Res_City = string.Empty;
            Emp_Res_pin = string.Empty;
            Emp_PAdd1 = string.Empty;
            Emp_PAdd2 = string.Empty;
            Emp_PAdd3 = string.Empty;
            Emp_PCity = string.Empty;
            Emp_Ppin = string.Empty;
            Emp_Accomadation = char.MinValue;
            Emp_Deput_Indi = char.MinValue;
            Emp_Stop_Pay = char.MinValue;
            
            Emp_EMailID = string.Empty;
            Emp_Metro = char.MinValue;
            
            Emp_PAN = string.Empty;
            Emp_qualification = string.Empty;
            Emp_Father_name = string.Empty;
            Emp_Spouse_name = string.Empty;
            Emp_UAN = string.Empty;
            Emp_AadharNo = string.Empty;
            Emp_ResigReas = char.MinValue;
            Action = string.Empty;

           
        }
        public Int32 Emp_No { get; set; }
        public string Emp_Name { get; set; }
        public char Emp_Sex { get; set; }
        public Int16 Emp_Desig { get; set; }
        public DateTime Emp_DOB { get; set; }
        public Int16 Emp_Dept { get; set; }
        public DateTime Emp_JoinDt { get; set; }
        public DateTime Emp_ConfDt { get; set; }
        public DateTime Emp_IncrementDt { get; set; }
        public DateTime Emp_PromotionDt { get; set; }
        public char Emp_Marital_Status { get; set; }
        public Int16 Emp_Children { get; set; }
        public char Emp_Child_Allow { get; set; }
        public Int16 Emp_Grade { get; set; }
        
        public string Emp_PF_Nom { get; set; }
        public string Emp_PF_No { get; set; }
        public string Emp_EPS_No { get; set; }
        //public string Emp_ESI_No { get; set; }
        public char Emp_OT_Ind { get; set; }
        public Int16 Emp_SalBank { get; set; }
        public Int16 Emp_SalBranch { get; set; }
        public string Emp_AccNo { get; set; }
        public string Emp_Res_Add1 { get; set; }
        public string Emp_Res_Add2 { get; set; }
        public string Emp_Res_Add3 { get; set; }
        public string Emp_Res_City { get; set; }
        public Int16 Emp_Res_State { get; set; }
        public string Emp_Res_pin { get; set; }
        public string Emp_PAdd1 { get; set; }
        public string Emp_PAdd2 { get; set; }
        public string Emp_PAdd3 { get; set; }
        public string Emp_PCity { get; set; }
        public Int16 Emp_PState { get; set; }
        public string Emp_Ppin { get; set; }
        public char Emp_Accomadation { get; set; }
        public float Emp_House_Rpaid { get; set; }
        public Int16 Emp_City_Class { get; set; }
        public Int16 Emp_LTC_Availed { get; set; }
        public float Emp_VPF_Contri { get; set; }
        public Int16 Emp_Vehicle { get; set; }
        public char Emp_Deput_Indi { get; set; }
        public char Emp_Stop_Pay { get; set; }
        public DateTime Emp_ResigDt { get; set; }
        public Int16 Emp_Branch { get; set; }
        public Int16 Emp_StateCode { get; set; }
        public string Emp_EMailID { get; set; }
        public char Emp_Metro { get; set; }
        //public char Emp_ESI_Flag { get; set; }
        //public Int16 Emp_Eby { get; set; }
        public DateTime Emp_CrDt { get; set; }
        public string Emp_PAN { get; set; }
        public string Emp_AadharNo { get; set; }
        public string Emp_qualification { get; set; }
        public string Emp_Father_name { get; set; }
        public string Emp_Spouse_name { get; set; }
        public char Emp_ResigReas { get; set; }
        public string Emp_UAN { get; set; }

        public Int16 MCode { get; set; }
        public Int16 SV { get; set; }

        public string Action { get; set; }
    }
}

