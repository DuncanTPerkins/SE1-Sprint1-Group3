using FormBuilderApp.DataContexts;
using FormBuilderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FormBuilderApp.Controllers
{
    public class FormController : Controller
    {
        private FormBuilderDb _db = new FormBuilderDb();
        private IdentityDb _IdentityDb = new IdentityDb();
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
                FormData = jsonData[2]
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
            ViewBag.Id = form.Id;
            if (form == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        public ActionResult FillOut(String[] jsonData)
        {
            var userStore = new UserStore<ApplicationUser>(_IdentityDb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            Form ParentForm = _db.Forms.Find((Int32.Parse(jsonData[0])));
            Form ChildForm = new Form();
            ChildForm.ParentId = ParentForm.Id;
            ChildForm.FormObjectRepresentation = jsonData[1];
            ChildForm.Status = Form.FormStatus.Completed;
            ChildForm.Name = ParentForm.Name;
            ChildForm.UserId = User.Identity.GetUserId();
            ParentForm.Users.Add(ChildForm.UserId);
            _db.Forms.Add(ChildForm);
            _db.SaveChanges();
            return View();
        }
    }
}