using FormBuilderApp.DataContexts;
using FormBuilderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormBuilderApp.Controllers
{
    public class HomeController : Controller
    {
        private FormBuilderDb _db = new FormBuilderDb();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Forms()
        {
            return View(_db.Forms.ToList());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        [Authorize(Roles="User")]
        public ActionResult DummyForm()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult DummyForm(Form form1)
        {
            _db.Forms.Add(form1);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Super Admin")]
        public ActionResult CreateForm()
        {
            return View();
        }
    }
}