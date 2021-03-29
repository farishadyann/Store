using Store.DataAccess;
using Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Bussines
{
    public class BL_Checkout
    {
        DAL_Checkout DALCheckout = new DAL_Checkout();

        public bool PostCheckout(TR_Checkout_Request req)
        {
            bool RetVal = false;

            try
            {
                RetVal = DALCheckout.PostCheckout(req);
            }
            catch(Exception ex)
            {

            }

            return RetVal;
        }
    }
}
