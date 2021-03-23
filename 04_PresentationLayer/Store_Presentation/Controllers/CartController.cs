using Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store_Presentation.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public JsonResult AddToCart(TR_Cart_Request req)
        {
            JsonResult RetVal = new JsonResult();
            try
            {

            }
            catch(Exception ex)
            {

            }
            finally
            {
                RetVal.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                RetVal.Data = req.Quantity;
            }

            return RetVal;
        }
    }
}