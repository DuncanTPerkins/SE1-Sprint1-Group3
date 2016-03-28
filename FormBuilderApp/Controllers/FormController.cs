using FormBuilderApp.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
namespace FormBuilderApp.Controllers
{
    public class FormController : Controller
    {
        private FormBuilderDb _db = new FormBuilderDb();

        // GET: Form
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(String[] jsonData)
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
    }
}