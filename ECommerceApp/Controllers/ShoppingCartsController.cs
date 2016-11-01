using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using ECommerceApp.Models;
using ECommerceApp.DAL;

namespace ECommerceApp.Controllers
{
    public class ShoppingCartsController : Controller
    {
        // GET: ShoppingCarts
        public ActionResult Index()
        {
            using (ShoppingCartsDAL scService = new ShoppingCartsDAL())
            {
                if (TempData["Pesan"] != null)
                {
                    ViewBag.Pesan = TempData["Pesan"].ToString();
                }
                string username =
                    Session["username"] != null ? Session["username"].ToString() : string.Empty;
                return View(scService.GetAllData(username).ToList());
            }
        }

        public ActionResult AddToCart(int id)
        {
            //cek apakah user sudah login
            if (Session["username"] == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Session["username"] = User.Identity.Name;
                }
                else
                {
                    return Redirect("~/Account/Login");
                }
            }

            using (ShoppingCartsDAL scService = new ShoppingCartsDAL())
            {
                var newShoppingCart = new ShoppingCart
                {
                    CartID = Session["username"].ToString(),
                    Quantity = 1,
                    BookID = id,
                    DateCreated = DateTime.Now
                };
                scService.AddToCart(newShoppingCart);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                using (ShoppingCartsDAL service = new ShoppingCartsDAL())
                {
                    try
                    {
                        service.Delete(id.Value);
                        TempData["Pesan"] = Helper.KotakPesan.GetPesan("success",
                        "Sukses ", "Data berhasil dihapus");
                    }
                    catch (Exception ex)
                    {
                        TempData["Pesan"] = Helper.KotakPesan.GetPesan("danger",
                        "Error! ", ex.Message);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            using (ShoppingCartsDAL service = new ShoppingCartsDAL())
            {
                var shopcart = service.GetDataByID(id);
                return View(shopcart);
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, ShoppingCart shopcart)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ShoppingCartsDAL service = new ShoppingCartsDAL())
            {
                try
                {
                    service.Edit(id.Value, shopcart);
                    TempData["Pesan"] = Helper.KotakPesan.GetPesan("success",
                    "Sukses ", "Quantity berhasil diubah");
                }
                catch (Exception ex)
                {
                    TempData["Pesan"] = Helper.KotakPesan.GetPesan("danger",
                    "Error! ", ex.Message);
                }

            }
            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            if(User.Identity.IsAuthenticated)
            {
                using (ShoppingCartsDAL scService = new ShoppingCartsDAL())
            {
                string username =
                    Session["username"] != null ? Session["username"].ToString() : string.Empty;
                return View(scService.GetAllData(username).ToList());
            }
            }
            else
            {
                return RedirectToAction("Login","Account");
            }
        }
    }
}