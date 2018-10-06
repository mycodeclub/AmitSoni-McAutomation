using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Controllers
{
    public class DeliveryNoteController : Controller
    {
        public UserMaster user;

        // GET: DeliveryNote
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        public ActionResult Create()
        {
            ViewBag.Store = new SelectList(GetStore(), "Value", "Text");
            ViewBag.Item = new SelectList(GetItem(), "Value", "Text");
            ViewBag.Uom = new SelectList(GetUom(), "Value", "Text");
            ViewBag.Vendor = new SelectList(GetVendor(), "Value", "Text");
            return View();
        }

        private List<SelectListItem> GetStore()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.StoreMasters.Where(e => e.CustomerId == user.CustomerId).AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.StoreName,
                                                    Value = p.RecordId.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Store", Value = "" });
            return storeStatus;
        }

        private List<SelectListItem> GetItem()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.ItemMasters.Where(e => e.CustomerId == user.CustomerId).AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.ItemName,
                                                    Value = p.RecordId.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Item", Value = "" });
            return storeStatus;
        }

        private List<SelectListItem> GetUom()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.UomMasters.Where(e => e.CustomerId == user.CustomerId).AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.UOMName,
                                                    Value = p.RecordId.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select UOM", Value = "" });
            return storeStatus;
        }

        private List<SelectListItem> GetVendor()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.VendorMasters.Where(e => e.CustomerId == user.CustomerId).AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.VendorName,
                                                    Value = p.RecordId.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Vendor", Value = "" });
            return storeStatus;
        }

        public ActionResult ViewAll()
        {
            ViewBag.CrivCount = (from a in db.StoreGateVouchers where a.RecordId == 0 group a by a.VoucherId into a select a).ToList().Count();



            var crivList = (from a in db.StoreGateVouchers

                            join d in db.UserMasters on a.EmployeeID equals d.EmployeeCode
                            select new StoreGatePassDetails() { credit = a, Emp = d.UserName }).ToList();

            var finalList = crivList.GroupBy(a => a.credit.VoucherId).Select(b => b.First()).ToList();
            return View(finalList);
        }

        public ActionResult Details(string id)
        {
            var crivList = (from a in db.StoreGateVouchers where a.VoucherId == id select a);

            return View(crivList);
        }
    }
}