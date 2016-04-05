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
            ViewBag.FormHtml = form.FormData;
            ViewBag.Name = form.Name;
            if (form == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult Forms()
        {
            var statusesToShow = Form.FormStatus.Template | Form.FormStatus.Draft | Form.FormStatus.Completed | Form.FormStatus.Accepted;
            return View(_db.Forms.Where(x => (x.Status & statusesToShow) == Form.FormStatus.Template).ToList());
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }        
    }
}
