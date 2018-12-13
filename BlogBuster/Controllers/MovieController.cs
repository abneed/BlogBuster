using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogBuster.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            int id = int.Parse(Session["User_Id"].ToString());
            return View(Models.User.Find(id).Movies);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View(Models.Movie.Find(id));
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (new Models.Movie(
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
                ViewBag.Message = "Error: Couldn't add movie to the list of favorites.";
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Models.Movie.Find(id));
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (new Models.Movie(
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
                ViewBag.Message = "Error: Couldn't update the movie.";
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            Models.Movie.Find(id).Delete();
            return RedirectToAction("Index");
        }

    }
}
