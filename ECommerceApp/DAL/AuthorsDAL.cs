using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Models;


namespace ECommerceApp.DAL
{
    public class AuthorsDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();

        public IQueryable<Author> GetData()
        {
            var results = from c in db.Authors
                          orderby c.FirstName ascending
                          select c;
            return results;
        }
        public Author GetDataByID(int AuthorID)
        {
            var result = (from c in db.Authors
                          where c.AuthorID == AuthorID
                          select c).SingleOrDefault();
            return result;
        }

        public void Add(Author obj)
        {
            try
            {
                db.Authors.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
      
        public void Edit(int AuthorID, Author obj)
        {
            var model = GetDataByID(AuthorID);
            if (model!=null)
            {
                model.FirstName = obj.FirstName;
                model.LastName = obj.LastName;
                model.Email = obj.Email;
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

        public void Delete(int AuthorID)
        {
            var model = GetDataByID(AuthorID);
            if (model != null)
            {    
                try
                {
                    db.Authors.Remove(model);
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            else
            {
                throw new Exception("Data tidak ditemukan !");
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}