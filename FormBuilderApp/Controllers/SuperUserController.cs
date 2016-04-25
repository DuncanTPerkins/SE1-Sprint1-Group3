using FormBuilderApp.DataContexts;
using FormBuilderApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

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

        //Allows "Super User" to view the completed form list
        [Authorize(Roles = "Super User")]
        public ActionResult ViewFormsSuperUser(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;


            var statusesToShow = Form.FormStatus.Template | Form.FormStatus.Draft | Form.FormStatus.Completed | Form.FormStatus.Accepted | Form.FormStatus.Denied;
            var forms = _db.form.Where(x => (x.Status & statusesToShow) == Form.FormStatus.Completed);

            if (!String.IsNullOrEmpty(searchString))
            {
                forms = forms.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    forms = forms.OrderByDescending(s => s.Name);
                    break;
                default:
                    forms = forms.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(forms.ToPagedList(pageNumber, pageSize));
        }


        //Allows "Super User" to view the contents of a certain form
        [Authorize(Roles = "Super User")]
        public ActionResult ViewContentSuperUser(int id)
        {
            List<String> FormOutput = new List<String>();
            Form form = _db.form.Find(id);
            ViewBag.Name = form.Name;
            var FormJSON = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(form.FormObjectRepresentation);
            for (int i = 0; i < FormJSON.Count; i++)
            {
                FormOutput.Add(FormJSON[i]["name"] + ": " + FormJSON[i]["value"]);
            }
            ViewBag.Id = form.Id;
            ViewBag.Output = FormOutput;
            if (form == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [Authorize(Roles = "Super User")]
        public ActionResult AcceptForm(int id)
        {
            Form form = _db.form.Find(id);
            form.Status = Form.FormStatus.Accepted;
            _db.SaveChanges();
            return RedirectToAction("ViewFormsSuperUser", "SuperUser");
        }

        [Authorize(Roles = "Super User")]
        public ActionResult DenyForm(int id, string reason)
        {
            Form form = _db.form.Find(id);
            form.Status = Form.FormStatus.Draft;
            form.DenyReason = reason;
            _db.SaveChanges();
            return RedirectToAction("ViewFormsSuperUser", "SuperUser");
        }

    }
}