using FormBuilderApp.DataContexts;
using FormBuilderApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

<<<<<<< HEAD
=======
        public ActionResult Forms()
        {
            return View(_db.Forms.Where(f => f.Status == Models.Form.FormStatus.Template).ToList());
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            Form form = _db.Forms.Find(id);
            if (form == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Form form)
        {
            _db.Entry(form).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Forms");
        }

>>>>>>> 0f4982b57b7253696dc3e1732ffccb28e127927a
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
    }
}