using Store.Bussines;
using Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store_Presentation.Controllers
{
    public class CheckoutController : Controller
    {
        BL_Checkout BLCheckout = new BL_Checkout();
        BL_Cart BLCart = new BL_Cart();
        BL_Product BLProduct = new BL_Product();
        
        public ActionResult Checkout()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                List<TR_Cart_Item> Items = Session["TesData"] as List<TR_Cart_Item>;
                List<TR_Cart_Response> CartItem = GetCartItems(Items);
                ViewBag.VCart = CartItem;
            }
            catch (Exception ex)
            {

            }

           
            return View();
        }
        [HttpPost]
       public ActionResult Checkout(TR_Checkout_Request req)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string Valid = ValidationRequest(req);
                    if(Valid == "")
                    {
                        string Address = req.ShippingAddress+" " + req.City +" "+ req.Districs;
                        List<TR_Cart_Item> Items = Session["TesData"] as List<TR_Cart_Item>;
                        List<TR_Cart_Response> CartItem = GetCartItems(Items);
                        req.Cart = CartItem;
                        req.CreatedBy = Session["UserName"].ToString();
                        bool PostCheckout = BLCheckout.PostCheckout(req);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return RedirectToAction("Index","Home");
        }

        

        public string ValidationRequest(TR_Checkout_Request req)
        {
            string RetVal = "";
            if (req.UserName != Session["UserName"].ToString())
            {
                RetVal = "UserName Not Match";
            }
            return RetVal;
        }

        #region Cart Method
        [HttpPost]
        public string GetDataCart(List<TR_Cart_Item> data)
        {
            string RetVal = "";
            Session["TesData"] = data;
            RetVal = Url.Action("Checkout");

            return RetVal;
        }

        public List<TR_Cart_Response> GetCartItems(List<TR_Cart_Item> req)
        {
            List<TR_Cart_Response> RetVal = new List<TR_Cart_Response>();
            try
            {
                foreach(var item in req)
                {
                    TR_Cart_Request param = new TR_Cart_Request
                    {
                        CartID_PK = item.CartID_PK
                    };
                    TR_Cart_Response cartItem = GetCart(param).FirstOrDefault();
                    RetVal.Add(cartItem);
                }
            }
            catch(Exception ex)
            {

            }
            return RetVal;
        }

        public List<TR_Cart_Response> GetCart(TR_Cart_Request req)
        {
            List<TR_Cart_Response> RetVal = new List<TR_Cart_Response>();
            try
            {
                RetVal = BLCart.GetCart(req);
            }
            catch(Exception ex)
            {

            }
            return RetVal;
        }
        #endregion
    }
}