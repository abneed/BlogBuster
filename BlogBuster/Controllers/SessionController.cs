using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogBuster.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Session/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.User user = Models.User.FindBy(collection["Email"].ToLower(), collection["Password"]);
                Session["User_Id"] = user.Id;
                return RedirectToAction("Details", "User", user);
            }
            catch
            {
                ViewBag.Message = "Invalid email/password combination";
                return View();
            }
        }

        // GET: Session/Delete/5
        public ActionResult Delete()
        {
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
