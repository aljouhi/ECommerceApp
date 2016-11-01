using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Models;

namespace ECommerceApp.DAL
{
    public class CategoriesDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();

        public IQueryable<Category> GetData()
        {
            var results = from c in db.Categories
                         orderby c.CategoryName ascending
                         select c;
            return results;
        }

        public void Add(Category obj)
        {
            try 
            {
                db.Categories.Add(obj);
                db.SaveChanges();
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public Category GetDataByID(int CategoryID)
        {
            var result = (from c in db.Categories
                          where c.CategoryID == CategoryID
                          select c).SingleOrDefault();
            return result;
        }

        public void Edit(int CategoryID, Category obj)
        {
            var result = GetDataByID(CategoryID);
            if (result != null)
            {
                result.CategoryName = obj.CategoryName;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
       
        public void Delete(int CategoryID)
        {
            var result = GetDataByID(CategoryID);
            if (result != null)
            {
                db.Categories.Remove(result);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public IQueryable<Category> SearchByKriteria(string txtSearch)
        {
            var result = from c in db.Categories
                         where c.CategoryName.ToLower().Contains(txtSearch.ToLower())
                         select c;
            return result;
        }

    }
}