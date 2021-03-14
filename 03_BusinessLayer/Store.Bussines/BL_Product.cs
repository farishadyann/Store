using Store.DataAccess;
using Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Bussines
{
    public class BL_Product
    {
        DAL_Product DAL = new DAL_Product();
        public List<MS_Product_Response> GetProduct(MS_Product_Request req)
        {
            List<MS_Product_Response> RetVal = new List<MS_Product_Response>();
            try
            {
                RetVal = DAL.GetProduct(req);
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
    }
}
