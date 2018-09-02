using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Controllers
{
    
    public class StoreReciptController : Controller
    {
        // GET: StoreRecipt
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
                                                    Value = p.StoreName.ToString()
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
                                                    Value = p.ItemName.ToString()
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
                                                    Value = p.UOMName.ToString()
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
                                                    Value = p.VendorName.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Vendor", Value = "" });
            return storeStatus;
        }


        public JsonResult InsertStoreRecipt(List<StoreReciptVoucher> criv)
        {
            using (db)
            {
                if (criv == null)
                {
                    criv = new List<StoreReciptVoucher>();
                }

                //Loop and insert records.
                foreach (StoreReciptVoucher c in criv)
                {
                    StoreReciptVoucher cr = new StoreReciptVoucher();
                    cr.VoucherId = c.VoucherId;
                    cr.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
                    cr.DepartmentId = c.DepartmentId;
                    cr.Store = c.Store;
                    cr.DateIns = c.DateIns;
                    cr.Item = c.Item;
                    cr.Vendor = c.Vendor;
                    cr.Qunatity = c.Qunatity;
                    cr.Uom = c.Uom;
                    cr.Rate = c.Rate;
                    cr.TotalValue = c.TotalValue;
                    cr.BatchDetail = c.BatchDetail;
                    cr.StoreOfficer = c.StoreOfficer;
                    cr.RecivingPerson = c.RecivingPerson;
                    cr.Remark = c.Remark;
                    cr.CreatedDate = DateTime.Now;
                    cr.Date1 = c.Date1;
                    cr.Sign1 = c.Sign1;
                    cr.Date2 = c.Date2;
                    cr.Sign2 = c.Sign2;
                    cr.Date3 = c.Date3;
                    cr.Sign3 = c.Sign3;
                    cr.ReceviedFrom = c.ReceviedFrom;
                    
                    db.StoreReciptVouchers.Add(cr);
                }
                int insertedRecords = db.SaveChanges();
                insertedRecords = 0;
                return Json(insertedRecords);
            }
        }


        public ActionResult ViewAll()
        {
            ViewBag.CrivCount = (from a in db.StoreReciptVouchers  group a by a.VoucherId into a select a).ToList().Count();

            var crivList = (from a in db.StoreReciptVouchers
                            
                            join d in db.UserMasters on a.EmployeeID equals d.EmployeeCode
                            select new StoreReciptDetails() { criv = a, Emp = d.UserName }).ToList();

            var finalList = crivList.GroupBy(a => a.criv.VoucherId).Select(b => b.First()).ToList();
            return View(finalList);
        }

        public ActionResult Details(string id)
        {
            var crivList = (from a in db.StoreReciptVouchers where a.VoucherId == id select a);

            return View(crivList);
        }
    }
}