using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace BlogBuster.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View(Models.User.All());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View(Models.User.Find(id));
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (new Models.User(
                    collection["Name"],
                    collection["Email"].ToLower(),
                    collection["Password"],
                    collection["Gender"])
                    .Save())
                {
                    Session["User_Id"] = Models.User.FindBy(collection["Email"].ToLower(), collection["Password"]).Id;
                    return RedirectToAction("Details", new { Id = int.Parse(Session["User_Id"].ToString()) });
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                ViewBag.Message = "Error: Couldn't create the user.";
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Models.User.Find(id));
        }


        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (new Models.User(
                    int.Parse(collection["Id"]),
                    collection["Name"],
                    collection["Email"].ToLower(),
                    collection["Password"],
                    collection["Gender"])
                    .Save())
                {
                    return RedirectToAction("Details", Models.User.Find(id));
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                ViewBag.Message = "Error: Couldn't update the user.";
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            Models.User.Find(id).Delete();

            ViewBag.Message = "User deleted";

            return RedirectToAction("Index", "Home");
        }

    }
}
