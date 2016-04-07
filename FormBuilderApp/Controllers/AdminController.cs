using FormBuilderApp.DataContexts;
using FormBuilderApp.Models;
using FormBuilderApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FormBuilderApp.Controllers
{
    public class AdminController : Controller
    {

        private IdentityDb _identityDb = new IdentityDb();
        private FormBuilderDb _db = new FormBuilderDb();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        /******************************
                SuperAdmin Stuff
        *******************************/

        [Authorize(Roles = "Super Admin")]
        public async Task<ActionResult> AssignRole(string id = "default")
        {
            if (id != "default")
            {
                var User = _identityDb.Users.Find(id);
                ViewBag.User = User;
                var userStore = new UserStore<ApplicationUser>(_identityDb);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var roleStore = new RoleStore<IdentityRole>(_identityDb);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                ViewBag.Role = roleManager.FindById(User.Roles.FirstOrDefault().RoleId).Name;
                // await ViewBag.Roles.Add(roleManager.FindById(User.Roles.FirstOrDefault().RoleId));
                ViewBag.UserNames = _identityDb.Users.Select(u => u.UserName);
                ViewBag.RoleNames = _identityDb.Roles.Select(r => r.Name);
                return View();
            }
            Response.StatusCode = 404;
            return View();

        }

        [Authorize(Roles = "Super Admin")]
        [HttpPost]
        public async Task<ActionResult> AssignRole(string username, string rolename)
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(_identityDb);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.Users.FirstOrDefault(u => u.UserName == username);
            await Task.Run(() => userManager.RemoveFromRoles(user.Id, userManager.GetRoles(user.Id).ToArray()));
            //var role = roles.FirstOrDefault(r => r == rolename);
            //if(role == null)
            userManager.AddToRole(user.Id, rolename);
            return RedirectToAction("Users");
        }

        /******************************
            Admin/SuperAdmin Stuff
        *******************************/

        [Authorize(Roles = "Admin, Super Admin")]
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

        [Authorize(Roles = "Admin, Super Admin")]
        [HttpGet]
        public ActionResult CreateForm()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Super Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateForm(String[] jsonData)
        {
            _db.Forms.Add(new Models.Form
            {
                Name = jsonData[0],
                Status = Models.Form.FormStatus.Template,
                FormData = jsonData[1]
                

            });
            _db.SaveChanges();
            return View();
        }

        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult CreateUser()
        {
            ViewBag.RoleNames = _identityDb.Roles.Select(r => r.Name);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult CreateUser(CreateUserViewModel model)
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

        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Roles()
        {
            ViewBag.Roles = _identityDb.Roles;
            return View(_identityDb.Roles.ToList());
        }

        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Users()
        {
            return View(_identityDb.Users.ToList());
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public ActionResult Details(string id)
        {
            var userStore = new UserStore<ApplicationUser>(_identityDb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            ViewBag.Roles = userManager.GetRoles(id);
            ApplicationUser user = _identityDb.Users.Find(id);
            return View(user);
        }

        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult ViewFormsAdmin()
        {
            var statusesToShow = Form.FormStatus.Template | Form.FormStatus.Draft | Form.FormStatus.Completed | Form.FormStatus.Accepted;
            return View(_db.Forms.Where(x => (x.Status & statusesToShow) == Form.FormStatus.Completed).ToList());
        }
    }
}