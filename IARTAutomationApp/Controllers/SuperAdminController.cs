using IARTAutomationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        // GET: SuperAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.NoOfTenants = db.CustomerMasters.Count();
            var beforeDate = DateTime.Now.AddMonths(-1);
            ViewBag.RecentCount = db.CustomerMasters.Where(c => c.UserMaster.CreatedDate > beforeDate).Count();
            return View();
        }
    }
}