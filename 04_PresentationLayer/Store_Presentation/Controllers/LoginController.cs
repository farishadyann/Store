using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.ViewModel;
using Store.Bussines;
using System.Data;

namespace Store_Presentation.Controllers
{
    public class LoginController : Controller
    {
        BL_Login BL = new BL_Login();
        BL_ActivityLog Act = new BL_ActivityLog();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(MS_UserAccount_Request req)
        {
            MS_UserAccount_Response res = new MS_UserAccount_Response();
            try
            {
                if (ModelState.IsValid)
                {
                    res = BL.LoginAuthentication(req);

                    bool Authententication = Convert.ToBoolean(res.Authentication);
                    string ResponseMessage = res.ResponseMessage.ToString();
                    
                    if (Authententication)
                    {
                        Session["UserName"] = res.UserName.ToString();
                        Session["UserID"] = res.UserID_PK.ToString();
                        ViewBag.ReturnUrl = Url.Action("Index", "Home");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorLog = ResponseMessage;
                        return View("Index");
                    }
                }
               
            }
            catch(Exception ex)
            {
                MS_ActivityLog param = new MS_ActivityLog
                {
                    ActionName = ControllerContext.RouteData.Values["action"].ToString(),
                    UserID_FK = res.UserID_PK.HasValue ? res.UserID_PK : 0,
                    ControllerName = ControllerContext.RouteData.Values["controller"].ToString(),
                    Description = ex.Message.ToString(),
                    ActivityDate = DateTime.Now
                };
                bool RetVal = Act.ActivityLog(param);
            }

            return View();
        }

        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(MS_UserInfo_Request req)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<MS_UserInfo_Response> Cek = BL.RegistrasiCek(req);
                    bool IsExists = Convert.ToBoolean(Cek.FirstOrDefault().ResponseAction);
                    if (!IsExists)
                    {
                        bool Req = BL.RegisterNewAccount(req);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = Cek.FirstOrDefault().ResponseMessage.ToString();
                        ViewBag.Err = "Y";
                        return View("Index");
                    }

                   
                }

            }
            catch (Exception ex)
            {
                MS_ActivityLog param = new MS_ActivityLog
                {
                    ActionName = ControllerContext.RouteData.Values["action"].ToString(),
                    UserID_FK = 0,
                    ControllerName = ControllerContext.RouteData.Values["controller"].ToString(),
                    Description = ex.Message.ToString(),
                    ActivityDate = DateTime.Now
                };
                bool RetVal = Act.ActivityLog(param);
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index");
        }

    }
}