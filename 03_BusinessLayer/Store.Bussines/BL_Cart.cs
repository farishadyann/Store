using Store.DataAccess;
using Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Bussines
{
    public class BL_Cart
    {
        DAL_Cart DAL = new DAL_Cart();
        public List<TR_Cart_Response> GetCart(TR_Cart_Request req)
        {
            List<TR_Cart_Response> RetVal = new List<TR_Cart_Response>();
            try
            {
                RetVal = DAL.GetCart(req);
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

        public bool PostCart(TR_Cart_Request req)
        {
            bool Retval = DAL.PostCart(req);
            return Retval;
        }
    }
}
