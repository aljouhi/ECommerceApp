using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Models;
using ECommerceApp.ViewModels;

namespace ECommerceApp.DAL
{
    public class OrderDAL: IDisposable
    {
        private CommerceModel db = new CommerceModel();
        
        public IQueryable<ShoppingCart>GetAllData(string id)
        {
            var result = (from s in db.ShoppingCarts
                          where
                              s.CartID == id
                          select s);
            return result;
        }

        public IQueryable<OrderVM>GetAllOrder(int id)
        {
            var result = from o in db.OrderDetails.Include("Orders")
                         where o.OrderID == id
                         select new OrderVM
                         {
                             OrderID = o.Order.OrderID,
                             CustomerName = o.Order.CustomerName,
                             OrderDate = o.Order.OrderDate,
                             ShipDate = o.Order.ShipDate,
                             BookID = o.BookID,
                             Quantity = o.Quantity,
                             Title = o.Book.Title,
                             Price = o.Book.Price
                         };
            return result;
        }
        public void AddOrder(Order obj) 
        {
            try
            {
                db.Orders.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw new Exception (ex.Message);
            }
        }

        public void AddDetailOrd(OrderDetail obj)
        {
            try
            {
                db.OrderDetails.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void RemoveCart(ShoppingCart sc)
        {
            try
            {
                db.ShoppingCarts.Remove(sc);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}