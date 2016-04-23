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
using System.Text.RegularExpressions;
using PagedList;

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

            // Assign Role
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

        // POST: assign roles
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

        // review forms
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Review(int id)
        {
            Form form = _db.form.Find(id);
            var formRegex = new Regex("\\\"(.*?)\\\"");
           

            List<string> formData = new List<string>();
            string formObject = form.FormObjectRepresentation;
            string formObject2 = "";
            foreach (Match m in formRegex.Matches(formObject))
            {
                string temp = m.ToString();
                formData.Add(temp);
            }
            int k = 3;
            for (int i = 1; i < formData.Count - 1; i= i+4)
            {
                formData[i].TrimStart('"');
                formData[k].TrimEnd('"');
                formObject2 = formObject2 + " <h2>" + formData[i] + "</h2> ";
                formObject2 = formObject2 + "<p>" + formData[k] + "</p>";
                k = k +4;
            }

            ViewBag.FormHtml = formObject2;
            ViewBag.Name = form.Name;
            

            return View();

        }

        [Authorize(Roles = "Admin, Super Admin")]
        [HttpGet]
        public ActionResult CreateForm()
        {
            ViewBag.Positions = _db.Position.Where(p => p.Position != null).Select(p => p);
            return View();
        }

        [Authorize(Roles = "Admin, Super Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateForm(String[] jsonData)
        {
            _db.form.Add(new Models.Form
            {
                Name = jsonData[0],
                Status = Models.Form.FormStatus.Template,
                FormData = jsonData[2],
                WorkflowId = Convert.ToInt32(jsonData[4])


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
                    return RedirectToAction("Users", "Admin");
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
        public ActionResult Users(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "email" : "";
            ViewBag.UsernameSortParm = sortOrder == "Username" ? "username" : "Username";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var users = from s in _identityDb.Users
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Email.Contains(searchString)
                                       || s.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "email":
                    users = users.OrderByDescending(s => s.Email);
                    break;
                case "Username":
                    users = users.OrderBy(s => s.UserName);
                    break;
                case "username":
                    users = users.OrderByDescending(s => s.UserName);
                    break;
                default:
                    users = users.OrderBy(s => s.Email);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(users.ToPagedList(pageNumber, pageSize));
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
        public ActionResult ViewFormsAdmin(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status" : "Status";
            ViewBag.UserSortParm = sortOrder == "UserId" ? "userId" : "UserId";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var statusesToShow = Form.FormStatus.Template | Form.FormStatus.Draft | Form.FormStatus.Completed | Form.FormStatus.Accepted;
            var forms = _db.form.Where(x => (x.Status & statusesToShow) == Form.FormStatus.Completed);

            if (!String.IsNullOrEmpty(searchString))
            {
                forms = forms.Where(s => s.Name.Contains(searchString)
                                       || s.UserId.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    forms = forms.OrderByDescending(s => s.Name);
                    break;
                case "Status":
                    forms = forms.OrderBy(s => s.Status);
                    break;
                case "status":
                    forms = forms.OrderByDescending(s => s.Status);
                    break;
                case "UserId":
                    forms = forms.OrderBy(s => s.UserId);
                    break;
                case "userId":
                    forms = forms.OrderByDescending(s => s.UserId);
                    break;
                default:
                    forms = forms.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(forms.ToPagedList(pageNumber, pageSize));
        } 
}
}