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
        // GET: Checkout
        public ActionResult Checkout(MS_Product_Item req)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}