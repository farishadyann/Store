using Store.Bussines;
using Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store_Presentation.Controllers
{
    public class HomeController : Controller
    {
        BL_Product BL = new BL_Product();
        BL_ActivityLog Act = new BL_ActivityLog();

        public ActionResult Index()
        {
            
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    MS_Product_Request param = new MS_Product_Request();
                    List<MS_Product_Response> Product = GetProduct(param);
                    ViewBag.VProducts = Product;
                }
            }
            catch(Exception ex)
            {
                int UserID = Convert.ToInt32(HttpContext.Session["UserID"].ToString());

                MS_ActivityLog param = new MS_ActivityLog
                {
                    ActionName = ControllerContext.RouteData.Values["action"].ToString(),
                    UserID_FK = UserID,
                    ControllerName = ControllerContext.RouteData.Values["controller"].ToString(),
                    Description = ex.Message.ToString(),
                    ActivityDate = DateTime.Now
                };
                bool RetVal = Act.ActivityLog(param);
            }

            return View();
        }

        public List<MS_Product_Response> GetProduct(MS_Product_Request req)
        {
            List<MS_Product_Response> RetVal = new List<MS_Product_Response>();
            try
            {
                RetVal = BL.GetProduct(req);
            }
            catch(Exception ex)
            {

            }
            return RetVal;
        }

        public ActionResult btnOpenDetail(MS_Product_Request req)
        {
            return RedirectToAction("Product", "ProductDetail",req);
        }
    }
}