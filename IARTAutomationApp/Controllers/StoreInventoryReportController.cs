using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Controllers
{

    public class StoreInventoryReportController : Controller
    {
        // GET: StoreInventoryReport
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        // GET: StoreInventoryReport
        public ActionResult Index()
        {

            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            var storeCode = from a in db.StoreMasters
                            where a.CustomerId == user.CustomerId
                            join b in db.UserMasters on a.EmployeeID equals b.EmployeeCode
                            join c in db.StatusMasters on a.StoreStatus equals c.RecordId
                            let cc = (
                from s in db.ItemMasters
                where a.RecordId == s.StoreId
                select s
                ).Count()
                            select new StoreInventoryDetails()
                            {
                                storeName = a.StoreName,
                                storeDesc = a.StoreDesc,
                                storeItemCount = cc,
                                storeNumber = a.StoreNumber,
                                empId = a.EmployeeID,
                                storeStatus = c.StatusName,
                                empName = b.UserName,
                                createdDate = a.CreatedDate
                            };
            return View(storeCode);
        }
    }
}