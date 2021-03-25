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
    public class DAL_Cart
    {
        private DbTransaction DBtran = new DbTransaction(ConfigurationManager.AppSettings["ConnStr"].ToString());

        public List<TR_Cart_Response> GetCart(TR_Cart_Request req)
        {
            List<TR_Cart_Response> RetVal = new List<TR_Cart_Response>();

            try
            {
                DataTable dt = DBtran.DbToDataTable("[dbo].[USP_GET_CART]", new
                {
                    pUserName = req.UserName == null ? "" : req.UserName,
                    pUserID = req.UserID_FK == null ? 0 : req.UserID_FK,
                    pProductID = req.ProductID_FK == null ? 0 : req.ProductID_FK,
                    pQuantity = req.Quantity == null ? 0 : req.Quantity
                }, true);

                foreach (DataRow row in dt.Rows)
                {
                    RetVal.Add(new TR_Cart_Response
                    {
                        CartID_PK = string.IsNullOrEmpty(row["CartID_PK"].ToString()) ? 0 : Convert.ToInt32(row["CartID_PK"]),
                        UserID_FK = string.IsNullOrEmpty(row["UserID_FK"].ToString()) ? 0 : Convert.ToInt32(row["UserID_FK"]),
                        UserName = string.IsNullOrEmpty(row["UserName"].ToString()) ? "" : row["UserName"].ToString(),
                        ProductID_FK = string.IsNullOrEmpty(row["ProductID_FK"].ToString()) ? 0 : Convert.ToInt32(row["ProductID_FK"]),
                        SellerID_FK = string.IsNullOrEmpty(row["SellerID_FK"].ToString()) ? 0 : Convert.ToInt32(row["SellerID_FK"]),
                        SellerName = string.IsNullOrEmpty(row["SellerName"].ToString()) ? "" : row["SellerName"].ToString(),
                        ProductName = string.IsNullOrEmpty(row["ProductName"].ToString()) ? "" : row["ProductName"].ToString(),
                        ProductImage = string.IsNullOrEmpty(row["ProductImage"].ToString()) ? "" : row["ProductImage"].ToString(),
                        Price = string.IsNullOrEmpty(row["Price"].ToString()) ? 0 : Convert.ToInt32(row["Price"]),
                        Stock = string.IsNullOrEmpty(row["Stock"].ToString()) ? 0 : Convert.ToInt32(row["Stock"]),
                        Quantity = string.IsNullOrEmpty(row["Quantity"].ToString()) ? 0 : Convert.ToInt32(row["Quantity"]),
                        FlagActive = string.IsNullOrEmpty(row["FlagActive"].ToString()) ? true : Convert.ToBoolean(row["FlagActive"]),
                        FlagDelete = string.IsNullOrEmpty(row["FlagDelete"].ToString()) ? true : Convert.ToBoolean(row["FlagDelete"]),
                        CreatedBy = string.IsNullOrEmpty(row["CreatedBy"].ToString()) ? "" : row["CreatedBy"].ToString(),
                        CreatedDate = string.IsNullOrEmpty(row["CreatedDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["CreatedDate"]),
                        ModifiedBy = string.IsNullOrEmpty(row["ModifiedBy"].ToString()) ? "" : row["ModifiedBy"].ToString(),
                        ModifiedDate = string.IsNullOrEmpty(row["ModifiedDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["ModifiedDate"]),
                        RecordTotal = string.IsNullOrEmpty(row["RecordTotal"].ToString()) ? 0 : Convert.ToInt32(row["RecordTotal"])
                    });
                }
            }
            catch (Exception ex)
            {
                RetVal = null;
                throw ex;
            }
            return RetVal;
        }

        public bool PostCart(TR_Cart_Request param)
        {
            bool RetVal;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                List<SqlParameter> Parameter = new List<SqlParameter>();

                //param
                //Parameter.Add(new SqlParameter() { ParameterName = "@pPostType", SqlDbType = SqlDbType.VarChar, Value = req.PostType.ToString() ?? "" });
                Parameter.Add(new SqlParameter() { ParameterName = "@pPostType", SqlDbType = SqlDbType.VarChar, Value = param.PostType ?? "" });
                Parameter.Add(new SqlParameter() { ParameterName = "@pCartID", SqlDbType = SqlDbType.VarChar, Value = param.CartID_PK.HasValue ? param.CartID_PK : 0 });
                Parameter.Add(new SqlParameter() { ParameterName = "@pUserID", SqlDbType = SqlDbType.Int, Value = param.UserID_FK.HasValue ? param.UserID_FK : 0 });
                Parameter.Add(new SqlParameter() { ParameterName = "@pProductID", SqlDbType = SqlDbType.Int, Value = param.ProductID_FK.HasValue ? param.ProductID_FK : 0 });
                Parameter.Add(new SqlParameter() { ParameterName = "@pQuantity", SqlDbType = SqlDbType.Int, Value = param.Quantity.HasValue ? param.Quantity : -1 });
                Parameter.Add(new SqlParameter() { ParameterName = "@pUserName", SqlDbType = SqlDbType.VarChar, Value = param.UserName ?? "" });
                Parameter.Add(new SqlParameter() { ParameterName = "@pDate", SqlDbType = SqlDbType.DateTime, Value = param.Date.HasValue ? param.Date : DateTime.Today });

                DBtran.DbExecute("USP_POST_CART", Parameter, true, out SqlParameterCollection outParameter);

                transactionScope.Complete();

                RetVal = true;
            }
            return RetVal;
        }
    }
}
