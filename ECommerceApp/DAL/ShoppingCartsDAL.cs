using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Models;


namespace ECommerceApp.DAL
{
    public class ShoppingCartsDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();

        public IQueryable<ShoppingCart> GetAllData(string username)
        {
            var result = from s in db.ShoppingCarts.Include("Book")
                         where s.CartID == username
                         orderby s.RecordID ascending
                         select s;
            return result;
        }
        // cek apakah barang dengan user yang sama ada di shopping cart
        public ShoppingCart GetItemByUser(string username, int bookId)
        {
            var result = (from s in db.ShoppingCarts
                          where s.CartID == username && s.BookID == bookId
                          select s).SingleOrDefault();
            return result;
        }

        public void UpdateCartID(string tempUsername, string username)
        {
            var result = from s in db.ShoppingCarts
                         where s.CartID == tempUsername
                         select s;
            foreach(var sc in result)
            {
                sc.CartID = username;
            }
            db.SaveChanges();
        }

        public void AddToCart(ShoppingCart shoppingCart)
        {
            //cek apakah cart dengan pengguna dan buku sama sudah ada
            var result = GetItemByUser(shoppingCart.CartID, shoppingCart.BookID);
            if (result != null)
            {
                //update
                result.Quantity += 1;
            }
            else
            {
                //tambah baru
                db.ShoppingCarts.Add(shoppingCart);
            }
            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public ShoppingCart GetDataByID(int RecordID)
        {
            var result = (from s in db.ShoppingCarts
                          where s.RecordID == RecordID
                          select s).SingleOrDefault();
            return result;
        }
        public void Delete(int RecordID)
        {
            var model = GetDataByID(RecordID);
            if (model != null)
            {
                try
                {
                    db.ShoppingCarts.Remove(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            else
            {
                throw new Exception("Data tidak ditemukan !");
            }
        }
        public void Edit(int RecordID, ShoppingCart obj)
        {
            var model = GetDataByID(RecordID);
            if (model != null)
            {
                model.Quantity = obj.Quantity;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Data tidak ditemukan!");
            }
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}