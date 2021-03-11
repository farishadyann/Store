using Store.DataAccess;
using System;
using System.Collections;
using System.Data;
using Store.ViewModel;
using System.Collections.Generic;

namespace Store.Bussines
{
    public class BL_Login
    {
        DAL_Login DAL = new DAL_Login();

        public MS_UserAccount_Response LoginAuthentication(MS_UserAccount_Request req)
        {
            MS_UserAccount_Response retVal = new MS_UserAccount_Response();
            try
            {
                retVal = DAL.LoginAuthentication(req);

            }
            catch (Exception ex)
            {
                retVal = null;
                throw ex;
            }
            finally
            {
            }
            return retVal;
        }

        public List<MS_UserInfo_Response> RegistrasiCek(MS_UserInfo_Request req)
        {
            List<MS_UserInfo_Response> retVal = new List<MS_UserInfo_Response>();
            try
            {
                retVal = DAL.RegistrasiCek(req);

            }
            catch (Exception ex)
            {
                retVal = null;
                throw ex;
            }
            finally
            {
            }
            return retVal;
        }

        public bool RegisterNewAccount(MS_UserInfo_Request req)
        {
            bool Retval = DAL.RegisterNewAccount(req);
            return Retval;
        }
    }
}
