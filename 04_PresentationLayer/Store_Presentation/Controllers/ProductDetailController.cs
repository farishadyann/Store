using Store.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.ViewModel;

namespace Store_Presentation.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        BL_Product BL = new BL_Product();
        BL_ActivityLog Act = new BL_ActivityLog();

        public ActionResult Product(MS_Product_Request req)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                MS_Product_Response ProductBy_ID = GetProduct(req);
                ViewBag.VProduct = ProductBy_ID;
            }
            catch(Exception ex)
            {

            }
            
            return View();
        }

        public MS_Product_Response GetProduct(MS_Product_Request req)
        {
            MS_Product_Response RetVal = new MS_Product_Response();
            try
            {
                List<MS_Product_Response> AllProduct = BL.GetProduct(req);
                RetVal = AllProduct.FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return RetVal;
        }
    }
}