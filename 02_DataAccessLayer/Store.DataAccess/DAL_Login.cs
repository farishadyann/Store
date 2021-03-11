using Database.Helper;
using System;
using System.Collections;
using System.Data;
using Store.ViewModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;

namespace Store.DataAccess
{

    public class DAL_Login
    {
        private DbTransaction DBtran = new DbTransaction(ConfigurationManager.AppSettings["ConnStr"].ToString());
        
        public MS_UserAccount_Response LoginAuthentication(MS_UserAccount_Request req)
        {
            MS_UserAccount_Response RetVal = new MS_UserAccount_Response();
            try
            {
                
                DataTable dt = DBtran.DbToDataTable("[dbo].[USP_LOGIN_AUTHENTICATION]", new
                {
                    UserName = req.UserName,
                    Password = req.Password
                },true);

                foreach(DataRow row in dt.Rows)
                {
                    RetVal.UserID_PK = string.IsNullOrEmpty(row["UserID_PK"].ToString()) ? 0 : Convert.ToInt32(row["UserID_PK"]);
                    RetVal.UserName = string.IsNullOrEmpty(row["UserName"].ToString()) ? "" : row["UserName"].ToString();
                    RetVal.Password = string.IsNullOrEmpty(row["Password"].ToString()) ? "" : row["Password"].ToString();
                    RetVal.IsActive = string.IsNullOrEmpty(row["IsActive"].ToString()) ? true : Convert.ToBoolean(row["IsActive"]);
                    RetVal.IsDelete = string.IsNullOrEmpty(row["IsDelete"].ToString()) ? true : Convert.ToBoolean(row["IsDelete"]);
                    RetVal.CreatedBy = string.IsNullOrEmpty(row["CreatedBy"].ToString()) ? "" : row["CreatedBy"].ToString();
                    RetVal.CreatedDate = string.IsNullOrEmpty(row["CreatedDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["CreatedDate"]);
                    RetVal.ModifiedBy = string.IsNullOrEmpty(row["ModifiedBy"].ToString()) ? "" : row["ModifiedBy"].ToString();
                    RetVal.ModifiedDate = string.IsNullOrEmpty(row["ModifiedDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["ModifiedDate"]);

                    RetVal.Authentication = string.IsNullOrEmpty(row["Authentication"].ToString()) ? true : Convert.ToBoolean(row["Authentication"]);
                    RetVal.ResponseMessage = string.IsNullOrEmpty(row["ResponseMessage"].ToString()) ? "" : (row["ResponseMessage"].ToString());
                }

               


            }
            catch (Exception ex)
            {
                RetVal = null;
                throw ex;
            }
            finally
            {
            }
            return RetVal;
        }

        public List<MS_UserInfo_Response> RegistrasiCek(MS_UserInfo_Request req)
        {
            List<MS_UserInfo_Response> RetVal = new List<MS_UserInfo_Response>();
            try
            {
                DataTable dt = DBtran.DbToDataTable("[dbo].[USP_GET_REGISTRASI_CEK]", new
                {
                    pUserName = req.UserName,
                    pEmail = req.Email
                }, true);

                foreach (DataRow row in dt.Rows)
                {
                    RetVal.Add(new MS_UserInfo_Response
                    {
                        UserInfoID_PK = string.IsNullOrEmpty(row["UserInfoID_PK"].ToString())? 0 :Convert.ToInt32(row["UserInfoID_PK"]),
                        UserID_FK = string.IsNullOrEmpty(row["UserID_FK"].ToString()) ? 0 : Convert.ToInt32(row["UserID_FK"]),
                        UserName = string.IsNullOrEmpty(row["UserName"].ToString()) ? "" : row["UserName"].ToString(),
                        Email = string.IsNullOrEmpty(row["Email"].ToString()) ? "" : row["Email"].ToString(),
                        BornDate = string.IsNullOrEmpty(row["BornDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["BornDate"]),
                        IsActive = string.IsNullOrEmpty(row["IsActive"].ToString()) ? false : Convert.ToBoolean(row["IsActive"]),
                        IsDelete = string.IsNullOrEmpty(row["IsDelete"].ToString()) ? false : Convert.ToBoolean(row["IsDelete"]),
                        FullName = string.IsNullOrEmpty(row["FullName"].ToString()) ? "" : row["FullName"].ToString(),
                        CreatedBy = string.IsNullOrEmpty(row["CreatedBy"].ToString()) ? "" : row["CreatedBy"].ToString(),
                        CreatedDate = string.IsNullOrEmpty(row["CreatedDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["CreatedDate"]),
                        ModifiedBy = string.IsNullOrEmpty(row["ModifiedBy"].ToString()) ? "" : row["ModifiedBy"].ToString(),
                        ModifiedDate = string.IsNullOrEmpty(row["ModifiedDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["ModifiedDate"]),
                        ResponseAction = string.IsNullOrEmpty(row["IsExists"].ToString()) ? false : Convert.ToBoolean(row["IsExists"]),
                        ResponseMessage = string.IsNullOrEmpty(row["ResponseMessage"].ToString()) ? "" : row["ResponseMessage"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                RetVal = null;
                throw ex;
            }
            finally
            {
            }
            return RetVal;
        }

        public bool RegisterNewAccount(MS_UserInfo_Request req)
        {
            bool Retval;

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    List<SqlParameter> Parameter = new List<SqlParameter>();

                    //param
                    //Parameter.Add(new SqlParameter() { ParameterName = "@pPostType", SqlDbType = SqlDbType.VarChar, Value = req.PostType.ToString() ?? "" });
                    Parameter.Add(new SqlParameter() { ParameterName = "@pEmail", SqlDbType = SqlDbType.VarChar, Value = req.Email ?? "" });
                    Parameter.Add(new SqlParameter() { ParameterName = "@pUserName", SqlDbType = SqlDbType.VarChar, Value = req.UserName ?? "" });
                    Parameter.Add(new SqlParameter() { ParameterName = "@pPassword", SqlDbType = SqlDbType.VarChar, Value = req.Password ?? "" });
                    Parameter.Add(new SqlParameter() { ParameterName = "@pCreatedDate", SqlDbType = SqlDbType.DateTime, Value = req.CreatedDate.HasValue ? req.CreatedDate : DateTime.Today});

                    DBtran.DbExecute("USP_REGISTER_USER", Parameter, true, out SqlParameterCollection outParameter);

                    transactionScope.Complete();

                    Retval = true;
                }
                catch (Exception ex)
                {
                    transactionScope.Dispose();

                    throw new Exception("DAL error : " + ex.Message);
                }
            }

            return Retval;
        }
    }
}
