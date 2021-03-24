using Store.Bussines;
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
        BL_Cart BL = new BL_Cart();
        // GET: Cart
        public ActionResult Cart()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    TR_Cart_Request param = new TR_Cart_Request
                    {
                        UserName = Session["UserName"].ToString(),
                        UserID_FK = Convert.ToInt32(Session["UserID"].ToString())
                    };
                    
                    List<TR_Cart_Response> Cart = GetCart(param);
                    ViewBag.VCart = Cart;
                }
            }
            catch(Exception ex)
            {

            }
            return View();
        }

        public JsonResult SearchCart(TR_Cart_Request req)
        {
            JsonResult RetVal = new JsonResult();
            TR_Cart_Response UserCart = new TR_Cart_Response();
            try
            {
                req.UserID_FK = Convert.ToInt32(HttpContext.Session["UserID"]);
                req.UserName = HttpContext.Session["UserName"].ToString();
                UserCart = GetCartFilter(req);
            }
            catch(Exception ex)
            {
                
            }
            finally
            {
                RetVal.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                RetVal.Data = (UserCart==null) ? 0 : Convert.ToInt32(UserCart.RecordTotal);
            }
            return RetVal;
        }

        public JsonResult AddToCart(TR_Cart_Request req)
        {
            JsonResult RetVal = new JsonResult();            
            TR_Cart_Response CekCart = new TR_Cart_Response();
            bool Action = false;
            try
            {
                CekCart = GetCartFilter(req);
                if (CekCart == null)
                {
                    Action = InsertCart(req);
                }
                else
                {
                    req.CartID_PK = CekCart.CartID_PK;
                    req.Quantity = CekCart.Quantity + req.Quantity;
                    Action = UpdateCart(req);
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                CekCart = GetCartFilter(req);
                RetVal.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                RetVal.Data = CekCart.RecordTotal;
            }

            return RetVal;
        }

        #region Method Cart
        public List<TR_Cart_Response> GetCart(TR_Cart_Request req)
        {
            List<TR_Cart_Response> RetVal = new List<TR_Cart_Response>();
            try
            {
                RetVal = BL.GetCart(req);
            }
            catch (Exception ex)
            {

            }
            return RetVal;
        }

        public TR_Cart_Response GetCartFilter(TR_Cart_Request req)
        {
            TR_Cart_Response RetVal = new TR_Cart_Response();
            try
            {
                RetVal = BL.GetCart(req).FirstOrDefault();
            }
            catch(Exception ex)
            {

            }
            return RetVal;
        }

        public bool InsertCart(TR_Cart_Request req)
        {
            bool RetVal = false;
            try
            {
                req.PostType = "INSERT";
                RetVal = BL.PostCart(req);
            }
            catch (Exception ex)
            {

            }
            return RetVal;
        }

        public bool UpdateCart(TR_Cart_Request req)
        {
            bool RetVal = false;
            try
            {
                req.PostType = "UPDATE";
                RetVal = BL.PostCart(req);
            }
            catch (Exception ex)
            {

            }
            return RetVal;
        }

        public bool DeleteCart(TR_Cart_Request req)
        {
            bool RetVal = false;
            try
            {
                req.PostType = "DELETE";
                RetVal = BL.PostCart(req);
            }
            catch (Exception ex)
            {

            }
            return RetVal;
        }
        #endregion
    }
}