using FormBuilderApp.DataContexts;
using FormBuilderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace FormBuilderApp.Controllers
{
    public class SuperUserController : Controller
    {
        private FormBuilderDb _db = new FormBuilderDb();

        // GET: SuperUser
        public ActionResult Index()
        {
            return View();
        }

        //Change user roles
        [Authorize(Roles = "Super User")]
        public ActionResult ViewFormsSuperUser()
        {
            var statusesToShow = Form.FormStatus.Template | Form.FormStatus.Draft | Form.FormStatus.Completed | Form.FormStatus.Accepted | Form.FormStatus.Denied;
            return View(_db.Forms.Where(x => (x.Status & statusesToShow) == Form.FormStatus.Completed).ToList());
        }

        [Authorize(Roles = "Super User")]
        public ActionResult Review(int id)
        {
            Form form = _db.Forms.Find(id);
            var formRegex = new Regex("\\\"(.*?)\\\"");
            currentFormID = id;

            List<string> formData = new List<string>();
            string formObject = form.FormObjectRepresentation;
            string formObject2 = "";
            foreach (Match m in formRegex.Matches(formObject))
            {
                string temp = m.ToString();
                formData.Add(temp);
            }
            int k = 3;
            for (int i = 1; i < formData.Count - 1; i = i + 4)
            {
                formData[i].TrimStart('"');
                formData[k].TrimEnd('"');
                formObject2 = formObject2 + " <h2>" + formData[i] + "</h2> ";
                formObject2 = formObject2 + "<p>" + formData[k] + "</p>";
                k = k + 4;
            }

            ViewBag.FormHtml = formObject2;
            ViewBag.Name = form.Name;


            return View();

        }

        
        [Authorize(Roles = "Super User")]
        public ActionResult Accept(int id)
        {
            Form form = _db.Forms.Find(id);
            form.Status = Form.FormStatus.Accepted;
            return View();
        }

        [Authorize(Roles = "Super User")]
        public ActionResult Deny(int id)
        {
            Form form = _db.Forms.Find(id);
            form.Status = Form.FormStatus.Denied;
            return View();
        }
    }
}