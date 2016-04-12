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
            Form form = _db.Forms.Find(id);
            if (form.FormData != null)
            {
                ViewBag.FormHtml = form.FormData;
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
            Form ParentForm = _db.Forms.Find((Int32.Parse(jsonData[0])));
            Form ChildForm = new Form();
            ChildForm.ParentId = ParentForm.Id;
            ChildForm.FormObjectRepresentation = jsonData[1];
            if (jsonData[2] == "0")
                ChildForm.Status = Form.FormStatus.Completed;
            else
            {
                ChildForm.FormData = jsonData[3];
                ChildForm.Status = Form.FormStatus.Draft;
            }
            ChildForm.Name = ParentForm.Name;
            ChildForm.UserId = User.Identity.GetUserId();
            _db.Forms.Add(ChildForm);
            _db.SaveChanges();
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult Forms()
        {
            var statusesToShow = Form.FormStatus.Template | Form.FormStatus.Draft | Form.FormStatus.Completed | Form.FormStatus.Accepted;
            ViewBag.UserId = User.Identity.GetUserId();
            //return View(_db.Forms.Where(x => (x.Status & statusesToShow) == Form.FormStatus.Template).ToList());
            return View(_db.Forms.ToList());
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }        
    }
}
