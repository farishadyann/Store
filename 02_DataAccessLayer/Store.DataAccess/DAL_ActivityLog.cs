using Database.Helper;
using Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Store.DataAccess
{
    public class DAL_ActivityLog
    {
        private DbTransaction DBtran = new DbTransaction(ConfigurationManager.AppSettings["ConnStr"].ToString());

        public bool ActivityLog(MS_ActivityLog param)
        {
            bool RetVal;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                List<SqlParameter> Parameter = new List<SqlParameter>();

                //param
                //Parameter.Add(new SqlParameter() { ParameterName = "@pPostType", SqlDbType = SqlDbType.VarChar, Value = req.PostType.ToString() ?? "" });
                Parameter.Add(new SqlParameter() { ParameterName = "@pActionName", SqlDbType = SqlDbType.VarChar, Value = param.ActionName ?? "" });
                Parameter.Add(new SqlParameter() { ParameterName = "@pUserID", SqlDbType = SqlDbType.Int, Value = param.UserID_FK.HasValue ? param.UserID_FK : 0 });
                Parameter.Add(new SqlParameter() { ParameterName = "@pController", SqlDbType = SqlDbType.VarChar, Value = param.ControllerName ?? "" });
                Parameter.Add(new SqlParameter() { ParameterName = "@pDescription", SqlDbType = SqlDbType.VarChar, Value = param.Description ?? "" });
                Parameter.Add(new SqlParameter() { ParameterName = "@pActivityDate", SqlDbType = SqlDbType.DateTime, Value = param.ActivityDate.HasValue ? param.ActivityDate : DateTime.Today });

                DBtran.DbExecute("USP_POST_ACTIVITY_LOG", Parameter, true, out SqlParameterCollection outParameter);

                transactionScope.Complete();

                RetVal = true;
            }
            return RetVal;
        }
    }
}
