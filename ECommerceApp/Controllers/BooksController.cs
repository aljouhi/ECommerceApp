using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.Models;
using ECommerceApp.DAL;
using ECommerceApp.ViewModels;
using System.IO;

namespace ECommerceApp.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            using (BooksDAL svBooks = new BooksDAL())
            {
                if (TempData["Pesan"] != null)
                {
                    ViewBag.Pesan = TempData["Pesan"].ToString();
                }
                var results = svBooks.GetData().ToList();
                return View(results);
            }
        }
        public ActionResult Create()
        {
            //data author
            var lstAuthor = new List<SelectListItem>();
            using (AuthorsDAL svAuthors = new AuthorsDAL())
            {
                foreach(var author in svAuthors.GetData())
                {
                    lstAuthor.Add(new SelectListItem
                        {
                            Value = author.AuthorID.ToString(),
                            Text = author.FirstName + " " + author.LastName
                        });
                }
                ViewBag.Authors = lstAuthor;
            }
            //
            var lstCat = new List<SelectListItem>();
            using (CategoriesDAL svCat = new CategoriesDAL())
            {
                foreach (var cat in svCat.GetData())
                {
                    lstCat.Add(new SelectListItem
                    {
                        Value = cat.CategoryID.ToString(),
                        Text = cat.CategoryName
                    });
                }
                ViewBag.Categories = lstCat;
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Book book, HttpPostedFileBase uploadimage)
        {
            string filePath = "";
            if(uploadimage.ContentLength>0)
            {
                string fileName = Guid.NewGuid().ToString() + " " + uploadimage.FileName;
                filePath = Path.Combine(HttpContext.Server.MapPath("~/Content/Image"),fileName);
                uploadimage.SaveAs(filePath);
                book.CoverImage = fileName;
            }
            using(BooksDAL svBooks = new BooksDAL())
            {
                try
                {
                    svBooks.Add(book);
                    TempData["Pesan"] = Helper.KotakPesan.GetPesan("success",
                        "Sukses ", "Data berhasil ditambahkan");
                }
                catch (Exception ex)
                {
                    TempData["Pesan"] = Helper.KotakPesan.GetPesan("danger",
                         "Error! ", ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult ViewAllBooks()
        {
            using (BooksDAL svBooks = new BooksDAL())
            {
                var results = svBooks.GetBooksWithAuthors().ToList();
                return View(results);
            }
        }

        public ActionResult Details(int id)
        {
            using (BooksDAL svBooks = new BooksDAL())
            {
                var result = svBooks.GetDetailWithAuthors(id);
                return View(result);
            }
        }


        public ActionResult Search(string selectKriteria, string txtSearch)
        {
            using(BooksDAL svBooks = new BooksDAL())
            {
                var results = svBooks.SearchByKriteria(selectKriteria, txtSearch).ToList();
                return View("ViewAllBooks", results);
            }
        }
    }
}