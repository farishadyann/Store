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

namespace Store.DataAccess
{
    public class DAL_Product
    {
        private DbTransaction DBtran = new DbTransaction(ConfigurationManager.AppSettings["ConnStr"].ToString());

        public List<MS_Product_Response> GetProduct(MS_Product_Request req)
        {
            List<MS_Product_Response> RetVal = new List<MS_Product_Response>();
            
            try
            {
                DataTable dt = DBtran.DbToDataTable("[dbo].[USP_GET_PRODUCT]", new
                {
                    pProductID= req.ProductID_PK == null ? 0 : req.ProductID_PK,
                    pSellerID = req.SellerID_FK == null ? 0 : req.SellerID_FK,
                    pCategoryID = req.CategoryID_FK == null ? 0 : req.CategoryID_FK,
                    pIsActive = req.IsActive == null ? true : req.IsActive
                }, true);

                foreach (DataRow row in dt.Rows)
                {
                    RetVal.Add(new MS_Product_Response
                    {
                        ProductID_PK = string.IsNullOrEmpty(row["ProductID_PK"].ToString()) ? 0 : Convert.ToInt32(row["ProductID_PK"]),
                        SellerID_FK = string.IsNullOrEmpty(row["SellerID_FK"].ToString()) ? 0 : Convert.ToInt32(row["SellerID_FK"]),
                        CategoryID_FK = string.IsNullOrEmpty(row["CategoryID_FK"].ToString()) ? 0 : Convert.ToInt32(row["CategoryID_FK"]),
                        ProductName = string.IsNullOrEmpty(row["ProductName"].ToString()) ? "" : row["ProductName"].ToString(),
                        Description = string.IsNullOrEmpty(row["Description"].ToString()) ? "" : row["Description"].ToString(),
                        Stock = string.IsNullOrEmpty(row["Stock"].ToString()) ? 0 : Convert.ToInt32(row["Stock"]),
                        Price = string.IsNullOrEmpty(row["Price"].ToString()) ? 0 : Convert.ToInt32(row["Price"]),
                        ProductSeen = string.IsNullOrEmpty(row["ProductSeen"].ToString()) ? 0 : Convert.ToInt32(row["ProductSeen"]),
                        ProductImage = string.IsNullOrEmpty(row["ProductImage"].ToString()) ? "" : row["ProductImage"].ToString(),
                        IsActive = string.IsNullOrEmpty(row["IsActive"].ToString()) ? true : Convert.ToBoolean(row["IsActive"]),
                        IsDelete = string.IsNullOrEmpty(row["IsDelete"].ToString()) ? true : Convert.ToBoolean(row["IsDelete"]),
                        CreatedBy = string.IsNullOrEmpty(row["CreatedBy"].ToString()) ? "" : row["CreatedBy"].ToString(),
                        CreatedDate = string.IsNullOrEmpty(row["CreatedDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["CreatedDate"]),
                        ModifiedBy = string.IsNullOrEmpty(row["ModifiedBy"].ToString()) ? "" : row["ModifiedBy"].ToString(),
                        ModifiedDate = string.IsNullOrEmpty(row["ModifiedDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(row["ModifiedDate"]),
                        RecordTotal = string.IsNullOrEmpty(row["RecordTotal"].ToString()) ? 0 : Convert.ToInt32(row["RecordTotal"])
                    });
                }
            }
            catch(Exception ex)
            {
                RetVal = null;
                throw ex;
            }
            return RetVal;
        }
    }
}
