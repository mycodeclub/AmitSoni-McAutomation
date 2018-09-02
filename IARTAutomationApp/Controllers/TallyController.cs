using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Controllers
{
    
    public class TallyController : Controller
    {
        // GET: Tally
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        public ActionResult Create()
        {
            ViewBag.Store = new SelectList(GetStore(), "Value", "Text");
            ViewBag.Item = new SelectList(GetItem(), "Value", "Text");
            ViewBag.Uom = new SelectList(GetUom(), "Value", "Text");
            ViewBag.Vendor = new SelectList(GetVendor(), "Value", "Text");
            return View();
        }

        private static List<SelectListItem> GetStore()
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.StoreMasters.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.StoreName,
                                                    Value = p.RecordId.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Store", Value = "" });
            return storeStatus;
        }

        private static List<SelectListItem> GetItem()
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.ItemMasters.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.ItemName,
                                                    Value = p.RecordId.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Item", Value = "" });
            return storeStatus;
        }

        private static List<SelectListItem> GetUom()
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.UomMasters.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.UOMName,
                                                    Value = p.RecordId.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select UOM", Value = "" });
            return storeStatus;
        }

        private static List<SelectListItem> GetVendor()
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.VendorMasters.AsEnumerable()
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
            ViewBag.CrivCount = (from a in db.StoreTallyVouchers where a.RecordId == 0 group a by a.TVoucherId into a select a).ToList().Count();



            var crivList = (from a in db.StoreTallyVouchers

                            join d in db.UserMasters on a.EmployeeID equals d.EmployeeCode
                            select new StoreTallyDetails() { tally = a, Emp = d.UserName }).ToList();

            var finalList = crivList.GroupBy(a => a.tally.TVoucherId).Select(b => b.First()).ToList();
            return View(finalList);
        }

        public ActionResult Details(string id)
        {
            var crivList = (from a in db.StoreTallyVouchers where a.TVoucherId == id select a);

            return View(crivList);
        }
    }
}