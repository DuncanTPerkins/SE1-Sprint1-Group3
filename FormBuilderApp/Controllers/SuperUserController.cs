using FormBuilderApp.DataContexts;
using FormBuilderApp.Models;
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

        //Change user roles
        [Authorize(Roles = "Super User")]
        public ActionResult ViewFormsSuperUser()
        {
            var statusesToShow = Form.FormStatus.Template | Form.FormStatus.Draft | Form.FormStatus.Completed | Form.FormStatus.Accepted | Form.FormStatus.Denied;
            return View(_db.Forms.Where(x => (x.Status & statusesToShow) == Form.FormStatus.Completed).ToList());
        }
    }
}