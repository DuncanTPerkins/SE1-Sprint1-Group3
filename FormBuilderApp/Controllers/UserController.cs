using FormBuilderApp.DataContexts;
using FormBuilderApp.Models;
using FormBuilderApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormBuilderApp.Controllers
{
    public class UserController : Controller
    {
        private IdentityDb _identityDb = new IdentityDb();

        protected override void Dispose(bool disposing)
        {
            _identityDb.Dispose();
            base.Dispose(disposing);
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.RoleNames = _identityDb.Roles.Select(r => r.Name);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserViewModel model)
        {
            var userStore = new UserStore<ApplicationUser>(_identityDb);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (ModelState.IsValid)
            {
                if (!_identityDb.Users.Any(u => u.UserName == model.Email))
                {
                    var user = new ApplicationUser
                    {
                        Email = model.Email,
                        UserName = model.Email,

                    };

                    userManager.Create(user, model.Password);
                    userManager.AddToRole(user.Id, model.StartingRole);
                    return RedirectToAction("Users", "User");
                }
                else
                {
                    ModelState.AddModelError("Email", "This username already exists!");
                }
            }
            ViewBag.RoleNames = _identityDb.Roles.Select(r => r.Name);
            return View(model);
        }

        public ActionResult Roles()
        {
            return View(_identityDb.Roles.ToList());
        }

        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Users()
        {
            return View(_identityDb.Users.ToList());
        }

        [Authorize(Roles = "Super Admin")]
        public ActionResult AssignRole()
        {
            ViewBag.UserNames = _identityDb.Users.Select(u => u.UserName);
            ViewBag.RoleNames = _identityDb.Roles.Select(r => r.Name);
            return View();
        }

        [Authorize(Roles = "Super Admin")]
        [HttpPost]
        public ActionResult AssignRole(string username, string rolename)
        {
            var userStore = new UserStore<ApplicationUser>(_identityDb);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var user = userManager.Users.FirstOrDefault(u => u.UserName == username);
            var roles = userManager.GetRoles(user.Id);
            var role = roles.FirstOrDefault(r => r == rolename);
            if(role == null)
                userManager.AddToRole(user.Id, rolename);

            return RedirectToAction("Users");
        }


    }
}