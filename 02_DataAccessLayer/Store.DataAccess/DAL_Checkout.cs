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
    public class DAL_Checkout
    {
        private DbTransaction DBtran = new DbTransaction(ConfigurationManager.AppSettings["ConnStr"].ToString());

        public bool PostCheckout(TR_Checkout_Request param)
        {
            bool RetVal = false;

            try
            {
                DataTable CartID = new DataTable();

                using (CartID)
                {
                    CartID.Columns.Add("CartID", typeof(int));
                    foreach (var i in param.Cart)
                    {
                        int item = Convert.ToInt32(i.CartID_PK);
                        CartID.Rows.Add(item);
                    }
                }
                using(TransactionScope transactionScope = new TransactionScope())
                {
                    List<SqlParameter> Parameter = new List<SqlParameter>();

                    Parameter.Add(new SqlParameter() { ParameterName = "@pListCartID", SqlDbType = SqlDbType.Structured });
                    Parameter[0].TypeName = "[dbo].[CARTLIST]";
                    Parameter[0].Value = CartID;
                    Parameter.Add(new SqlParameter() { ParameterName = "@pShippingAddress", SqlDbType = SqlDbType.VarChar, Value = param.ShippingAddress ?? "" });
                    Parameter.Add(new SqlParameter() { ParameterName = "@pPaymentTypeID", SqlDbType = SqlDbType.Int, Value = param.PaymentTypeID.HasValue ? param.PaymentTypeID :0 });
                    Parameter.Add(new SqlParameter() { ParameterName = "@pCreatedBy", SqlDbType = SqlDbType.VarChar, Value = param.CreatedBy ?? "" });
                    Parameter.Add(new SqlParameter() { ParameterName = "@pCreatedDate", SqlDbType = SqlDbType.DateTime, Value = param.CreatedDate.HasValue ? param.CreatedDate : DateTime.Now });

                    DBtran.DbExecute("USP_POST_CHECKOUT", Parameter, true, out SqlParameterCollection outParameter);
                    transactionScope.Complete();

                    RetVal = true;
                }
            }
            catch(Exception ex)
            {

            }

            return RetVal;
        }
    }
}
