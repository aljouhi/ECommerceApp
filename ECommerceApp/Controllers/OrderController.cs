using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using ECommerceApp.Models;
using ECommerceApp.DAL;

namespace ECommerceApp.DAL
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(int id)
        {
            using (OrderDAL service = new OrderDAL())
            {
                ViewBag.id = id;
                return View(service.GetAllOrder(id).ToList());
            } 
        }
        public ActionResult OrderSkrg()
        {
            var baru = new Order
            {
                CustomerName = Session["username"].ToString(),
                OrderDate = DateTime.Now,
                ShipDate = DateTime.Now.AddDays(2)
            };
            using(OrderDAL service = new OrderDAL())
            {
                service.AddOrder(baru);

                foreach (var item in service.GetAllData(Session["username"].ToString()).ToList())
                {
                    var DetailBaru = new OrderDetail
                    {
                        OrderID = baru.OrderID,
                        BookID = item.BookID,
                        Quantity = item.Quantity,
                        Price = item.Book.Price
                    };
                    service.AddDetailOrd(DetailBaru);
                    service.RemoveCart(item);
                }
            }

            return RedirectToAction("Index", new { id = baru.OrderID });
        }
    }
}