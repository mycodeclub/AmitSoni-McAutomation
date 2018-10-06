using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Controllers
{

    public class StockReportController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        // GET: StockReport
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];

            var vendorList = from a in db.StockMasters
                             where a.CustomerId == user.CustomerId
                             join b in db.StoreMasters on a.StoreId equals b.RecordId
                             join c in db.ClassMasters on a.ClassId equals c.RecordId
                             join d in db.ItemMasters on a.ItemId equals d.RecordId
                             join e in db.UomMasters on a.UomId equals e.RecordId
                             join f in db.VendorMasters on a.VendorId equals f.RecordId
                             join g in db.PurchaseOrders on a.PInvoiceNo equals g.OrderNo
                             join u in db.UserMasters on a.EmployeeID equals u.EmployeeCode
                             select new StockDetails() { stock = a, store = b, classMas = c, item = d, uom = e, vendor = f, po = g, empName = u.UserName };
            return View(vendorList);
        }
    }
}