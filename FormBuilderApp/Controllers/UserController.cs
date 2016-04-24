using FormBuilderApp.DataContexts;
using FormBuilderApp.Models;
using FormBuilderApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FormBuilderApp.Controllers
{
    public class UserController : Controller
    {
        private IdentityDb _identityDb = new IdentityDb();
        private FormBuilderDb _db = new FormBuilderDb();

        protected override void Dispose(bool disposing)
        {
            _identityDb.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult FillOut(int id = 0)
        {
            Form form = _db.form.Find(id);
            if (form.FormData != null)
            {
                ViewBag.FormHtml = form.FormData;
            }
            if (form.FormObjectRepresentation != null)
            {
                ViewBag.FormObjectRepresentation = form.FormObjectRepresentation;
            }

            ViewBag.Name = form.Name;
            ViewBag.Id = form.Id;

            if (form == null)
            {
                return HttpNotFound();
            }
            return View();
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult FillOut(String[] jsonData)
        {
            var userStore = new UserStore<ApplicationUser>(_identityDb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            Form ParentForm = _db.form.Find((Int32.Parse(jsonData[0])));
            Form ChildForm = new Form();
            ChildForm.ParentId = ParentForm.Id;
            ChildForm.FormObjectRepresentation = jsonData[1];
            if (jsonData[2] == "0")
                ChildForm.Status = Form.FormStatus.Completed;
            else
            {
                ChildForm.Status = Form.FormStatus.Draft;
            }
            ChildForm.FormData = jsonData[3];
            ChildForm.Name = ParentForm.Name;
            ChildForm.UserId = User.Identity.GetUserId();
            _db.form.Add(ChildForm);
            _db.SaveChanges();
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult Forms()
        {
            var statusesToShow = Form.FormStatus.Template | Form.FormStatus.Draft | Form.FormStatus.Completed | Form.FormStatus.Accepted;
            ViewBag.UserId = User.Identity.GetUserId();
            //return View(_db.Forms.Where(x => (x.Status & statusesToShow) == Form.FormStatus.Template).ToList());
            return View(_db.form.ToList());
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult Submitted()
        {
            String UserId = User.Identity.GetUserId();
            return View(_db.form.Where(x => x.UserId == UserId).Where(x => x.Status == Form.FormStatus.Completed));
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult ViewSubmitted(int id = 0)
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

    }
}
