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
    public class BooksDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();

        public IQueryable<Book> GetData()
        {
            var results = from b in db.Books
                          orderby b.Title ascending
                          select b;
            return results;
        }

        public IQueryable<BookVM> GetBooksWithAuthors()
        {

            var results = from b in db.Books.Include("Authors")
                          orderby b.Title ascending
                          select new BookVM
                          {
                              BookID = b.BookID,
                              AuthorID = b.AuthorID,
                              Title = b.Title,
                              PublicationDate = b.PublicationDate,
                              ISBN = b.ISBN,
                              CoverImage = b.CoverImage,
                              Price = b.Price,
                              Description = b.Description,
                              Publisher = b.Publisher,
                              FirstName = b.Author.FirstName,
                              LastName = b.Author.LastName
                          };
            return results;
        }

        public BookVM GetDetailWithAuthors(int id)
        {
            var result = (from b in db.Books.Include("Authors")
                          where b.BookID == id
                          orderby b.Title ascending
                          select new BookVM
                          {
                              BookID = b.BookID,
                              AuthorID = b.AuthorID,
                              Title = b.Title,
                              PublicationDate = b.PublicationDate,
                              ISBN = b.ISBN,
                              CoverImage = b.CoverImage,
                              Price = b.Price,
                              Description = b.Description,
                              Publisher = b.Publisher,
                              FirstName = b.Author.FirstName,
                              LastName = b.Author.LastName
                          }).SingleOrDefault();
            if(result!=null)
            {
                return result;
            }
            else
            {
                throw new Exception("Data tidak ditemukan");
            }
           
        }

        public IQueryable<BookVM> SearchByKriteria(string selectKriteria, string txtSearch)
        {
            IQueryable<BookVM> results;
            if(selectKriteria=="Title")
            {
                results = from b in db.Books.Include("Authors")
                              where b.Title.ToLower().Contains(txtSearch.ToLower())
                              orderby b.Title ascending
                              select new BookVM
                              {
                                  BookID = b.BookID,
                                  AuthorID = b.AuthorID,
                                  Title = b.Title,
                                  PublicationDate = b.PublicationDate,
                                  ISBN = b.ISBN,
                                  CoverImage = b.CoverImage,
                                  Price = b.Price,
                                  Description = b.Description,
                                  Publisher = b.Publisher,
                                  FirstName = b.Author.FirstName,
                                  LastName = b.Author.LastName
                              };
            }
            else
            {
                results = from b in db.Books.Include("Authors")
                          where b.Author.FirstName.ToLower().Contains(txtSearch.ToLower()) ||
                          b.Author.LastName.ToLower().Contains(txtSearch.ToLower())
                          orderby b.Title ascending
                          select new BookVM
                          {
                              BookID = b.BookID,
                              AuthorID = b.AuthorID,
                              Title = b.Title,
                              PublicationDate = b.PublicationDate,
                              ISBN = b.ISBN,
                              CoverImage = b.CoverImage,
                              Price = b.Price,
                              Description = b.Description,
                              Publisher = b.Publisher,
                              FirstName = b.Author.FirstName,
                              LastName = b.Author.LastName
                          };
            }
            return results;
        }

        public Book GetDataByID(int BookID)
        {
            var result = (from b in db.Books
                         where b.BookID == BookID
                         select b).SingleOrDefault();
            return result;
        }
    
        public void Add(Book obj)
        {
            try 
	        {	        
		        db.Books.Add(obj);
                db.SaveChanges();
	        }
	        catch (Exception ex)
	        {
		        throw new Exception(ex.Message);
	        }
        }

        public void Edit(int BookID, Book obj)
        {
            var model = GetDataByID(obj.BookID);
            if (model != null)
            {
                model.AuthorID = obj.AuthorID;
                model.CategoryID = obj.CategoryID;
                model.Title = obj.Title;
                model.PublicationDate = obj.PublicationDate;
                model.ISBN = obj.ISBN;
                model.CoverImage = obj.CoverImage;
                model.Price = obj.Price;
                model.Description = obj.Description;
                model.Publisher = obj.Publisher;
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

        public void Delete(int BookID)
        {
            var model = GetDataByID(BookID);
            if(model != null)
            {
                try
                {
                    db.Books.Remove(model);
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
        public void Dispose()
        {
            db.Dispose();
        }
    }
}