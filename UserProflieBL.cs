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
    public class UserProflieBL
    {
        
        public DataTable FetchUserMast(UserProfileBO UserMast)
        {
            UserProfileDA pDA = new UserProfileDA();
            try
            {
                return pDA.FetchUserMast(UserMast);
            }
            catch
            {
                throw;
            }
        }
    }
    public class EmpDetailsBL
    {
        public DataTable FetchEmpDetails(EmpDetailsBO EmpMast)
        {
            EmpDetailsDA pDa = new EmpDetailsDA();
            try
            {
                return pDa.FetchEmpDetails(EmpMast);

            }
            catch
            {
                throw;
            }
        }

        public string AUDEmpDetails(EmpDetailsBO EmpMast)
        {
            EmpDetailsDA dA = new EmpDetailsDA();
            try
            {
                return dA.AUDEmpDetails(EmpMast);
            }
            catch
            {
                throw;
            }
        }

        public string DelEmpDetails(EmpDetailsBO EmpMast)
        {
            EmpDetailsDA dA = new EmpDetailsDA();
            try
            {
                return dA.DelEmpDetails(EmpMast);
            }
            catch
            {
                throw;
            }
        }
    }
}
