using FormBuilderApp.DataContexts;
using FormBuilderApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult ViewFormsSuperUser()
        {
            var statusesToShow = Form.FormStatus.Template | Form.FormStatus.Draft | Form.FormStatus.Completed | Form.FormStatus.Accepted | Form.FormStatus.Denied;
            return View(_db.Forms.Where(x => (x.Status & statusesToShow) == Form.FormStatus.Completed).ToList());
        }

        //Allows "Super User" to view the contents of a certain form
        [Authorize(Roles = "Super User")]
        public ActionResult ViewContentSuperUser(int id)
        {
            List<String> FormOutput = new List<String>();
            Form form = _db.Forms.Find(id);
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
    }
}