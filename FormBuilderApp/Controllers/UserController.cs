using FormBuilderApp.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormBuilderApp.Controllers
{
    public class UserController : Controller
    {
        IdentityDb _identityDb = new IdentityDb();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Roles()
        {
            return View(_identityDb.Roles.ToList());
        }
    }
}