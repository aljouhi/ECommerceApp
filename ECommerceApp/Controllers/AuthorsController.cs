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
    public class AuthorsController : Controller
    {
        private CommerceModel db = new CommerceModel();
        // GET: Authors
        public ActionResult Index()
        {
            {
                using (AuthorsDAL service = new AuthorsDAL())
                {
                    var authors = service.GetData().ToList();
                    if(TempData["Pesan"] != null)
                    {
                        ViewBag.Pesan = TempData["Pesan"].ToString();
                    }
                    return View(authors);
                }
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Author author)
        {
            using (AuthorsDAL service = new AuthorsDAL())
            {
                try
                    {
                        service.Add(author);
                        TempData["Pesan"] = Helper.KotakPesan.GetPesan("success",
                        "Sukses ", "Data berhasil ditambahkan");
                    }
                catch(Exception ex)
                    {
                        TempData["Pesan"] = Helper.KotakPesan.GetPesan("danger",
                        "Error! ", ex.Message);
                    }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if(id != null)
            {
                using(AuthorsDAL service = new AuthorsDAL())
                {
                    try
                    {
                        service.Delete(id.Value);
                        TempData["Pesan"] = Helper.KotakPesan.GetPesan("success",
                        "Sukses ", "Data berhasil dihapus");
                    }
                    catch(Exception ex)
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
            using (AuthorsDAL service = new AuthorsDAL())
            {
                var author = service.GetDataByID(id);
                return View(author);
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, Author author)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (AuthorsDAL service = new AuthorsDAL())
            {
                try
                {
                    service.Edit(id.Value, author);
                    TempData["Pesan"] = Helper.KotakPesan.GetPesan("success",
                    "Sukses ", "Data author berhasil diubah");
                }
                catch (Exception ex)
                {
                    TempData["Pesan"] = Helper.KotakPesan.GetPesan("danger",
                    "Error! ", ex.Message);
                }

            }
            return RedirectToAction("Index");
        }
    }
}