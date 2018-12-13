using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogBuster.Controllers
{
    public class MicroPostController : Controller
    {
        // GET: MicroPost
        public ActionResult Index()
        {
            int id = int.Parse(Session["User_Id"].ToString());
            return View(Models.User.Find(id).MicroPosts);
        }

        // GET: MicroPost/Details/5
        public ActionResult Details(int id)
        {
            return View(Models.MicroPost.Find(id));
        }

        // GET: MicroPost/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MicroPost/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (new Models.MicroPost(
                    collection["Content"],
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
                ViewBag.Message = "Error: Couldn't create micropost.";
                return View();
            }
        }

        // GET: MicroPost/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Models.MicroPost.Find(id));
        }

        // POST: MicroPost/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (new Models.MicroPost(
                    int.Parse(collection["Id"]),
                    collection["Content"],
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
                ViewBag.Message = "Error: Couldn't update the micropost.";
                return View();
            }
        }

        // GET: MicroPost/Delete/5
        public ActionResult Delete(int id)
        {
            Models.MicroPost.Find(id).Delete();
            return RedirectToAction("Index");
        }

    }
}
