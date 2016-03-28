using FormBuilderApp.DataContexts;
using FormBuilderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
namespace FormBuilderApp.Controllers
{
    public class FormController : Controller
    {
        private FormBuilderDb _db = new FormBuilderDb();

        // GET: Form
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

       
       
        [Authorize(Roles = "Admin")]
        public ActionResult Completed()
        {
            List<Form> completed = new List<Form>();
            foreach (Form form in _db.Forms)
            {
                if ((int)form.Status == 1)
                {
                    completed.Add(form);
                }
            }
            return View(completed);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Review(int id)
        {
            Form form = _db.Forms.Find(id);
            ViewBag.FormHtml = form.FormData;
            ViewBag.Name = form.Name;
            if (form == null)
            {
                return HttpNotFound();
            }
            return View();


        }
        //***** NOT SURE WHAT STATUS INTS REPRESENT WHAT *******
        /*
        [Authorize(Roles = "Admin")]
        public ActionResult Accept(int id)
        {
            Form form = _db.Forms.Find(id);
            
            Form.FormStatus = *INSERT ACCEPTED INT*
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Deny(int id)
        {
            Form.FormStatus = *INSERT DENIED INT*
            return View();
        }
        */


        [Authorize(Roles="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(String[] jsonData)
        {
            _db.Forms.Add(new Models.Form
            {
                Name = jsonData[0],
                Status = Models.Form.FormStatus.Template,
                FormData = jsonData[1]

            });
            _db.SaveChanges();
            return View();
        }

        [HttpGet]
        [Authorize(Roles ="User")]
        public ActionResult FillOut(int id = 0)
        {
            Form form = _db.Forms.Find(id);
            ViewBag.FormHtml = form.FormData;
            ViewBag.Name = form.Name;
            if (form == null)
            {
                return HttpNotFound();
            }
            return View();
        }
    }
}