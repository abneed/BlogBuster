using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogBuster.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            int id = int.Parse(Session["User_Id"].ToString());
            return View(Models.User.Find(id).Books);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            return View(Models.Book.Find(id));
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (new Models.Book(
                    collection["Title"],
                    collection["Description"],
                    int.Parse(Session["User_Id"].ToString()))
                    .Save())
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                ViewBag.Message = "Error: Couldn't add book to the list of favorites.";
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Models.Book.Find(id));
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (new Models.Book(
                    int.Parse(collection["Id"].ToString()),
                    collection["Title"],
                    collection["Description"],
                    int.Parse(Session["User_Id"].ToString()))
                    .Save())
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                ViewBag.Message = "Error: Couldn't update the book.";
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            Models.Book.Find(id).Delete();
            return RedirectToAction("Index");
        }
    }
}
