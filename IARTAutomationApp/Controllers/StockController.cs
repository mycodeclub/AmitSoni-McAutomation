using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Controllers
{

    public class StockController : Controller
    {
        // GET: Stock
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        public ActionResult Create()
        {
            ViewBag.Store = new SelectList(GetStore(), "Value", "Text");
            ViewBag.Class = new SelectList(GetClass(), "Value", "Text");
            ViewBag.Item = new SelectList(GetItem(), "Value", "Text");
            ViewBag.Uom = new SelectList(GetUom(), "Value", "Text");
            ViewBag.Vendor = new SelectList(GetVendor(), "Value", "Text");
            ViewBag.Invoice = new SelectList(GetInvoice(), "Value", "Text");
            return View();
        }

        private List<SelectListItem> GetStore()
        {
            var user = (UserMaster)Session["User"];
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

        private List<SelectListItem> GetClass()
        {
            var user = (UserMaster)Session["User"];
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.ClassMasters.Where(e => e.CustomerId == user.CustomerId).AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.ClassName,
                                                    Value = p.RecordId.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Class", Value = "" });
            return storeStatus;
        }

        private List<SelectListItem> GetItem()
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            var user = (UserMaster)Session["User"];
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
            var user = (UserMaster)Session["User"];
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.UomMasters.Where(u => u.CustomerId == user.CustomerId).AsEnumerable()
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
            var user = (UserMaster)Session["User"];
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

        private List<SelectListItem> GetInvoice()
        {
            var user = (UserMaster)Session["User"];
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.PurchaseOrders.Where(e => e.CustomerId == user.CustomerId).AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.OrderNo,
                                                    Value = p.OrderNo.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Purchase Invoice", Value = "" });
            return storeStatus;
        }

        public ActionResult TotalPrice(int itemId)
        {
            var totalPrice = (from i in db.ItemMasters
                              where i.RecordId == itemId
                              select new { i.ItemRate }).Single();
            string pri = totalPrice.ItemRate;
            return Json(new { pri });
        }




        [HttpPost]
        public ActionResult Create([Bind(Include = "CustomerId,StoreId, ClassId, ItemId,BatchNo,Quantity,UomId,VendorId,PInvoiceNo,ReciveDate,TotalPrice")] StockMaster item)
        {

            StockMaster i = new StockMaster();
            ModelState.Remove("EmployeeID");
            if (ModelState.IsValid)
            {
                i.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
                i.StoreId = item.StoreId;
                i.ClassId = item.ClassId;
                i.ItemId = item.ItemId;
                i.BatchNo = item.BatchNo;
                i.UomId = item.UomId;
                i.VendorId = item.VendorId;
                i.BatchNo = item.BatchNo;
                i.Quantity = item.Quantity;
                i.UomId = item.UomId;
                i.PInvoiceNo = item.PInvoiceNo;
                i.TotalPrice = item.TotalPrice;
                i.ReciveDate = item.ReciveDate;
                i.CreatedDate = DateTime.Now;
                db.StockMasters.Add(i);
                db.SaveChanges();

            }
            return RedirectToAction("Create");

        }

        public ActionResult ViewAll()
        {
            ViewBag.TotalStockCount = (from a in db.StockMasters select a).ToList().Count();
            var vendorList = from a in db.StockMasters
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

        public ActionResult Edit(int? id)
        {
            ViewBag.Store = new SelectList(GetStore(), "Value", "Text");
            ViewBag.Class = new SelectList(GetClass(), "Value", "Text");
            ViewBag.Item = new SelectList(GetItem(), "Value", "Text");
            ViewBag.Uom = new SelectList(GetUom(), "Value", "Text");
            ViewBag.Vendor = new SelectList(GetVendor(), "Value", "Text");
            ViewBag.Invoice = new SelectList(GetInvoice(), "Value", "Text");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockMaster ven = db.StockMasters.Find(id);
            if (ven == null)
            {
                return HttpNotFound();
            }
            return View(ven);

        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CustomerId,RecordId,StoreId, ClassId, ItemId,BatchNo,Quantity,UomId,VendorId,PInvoiceNo,ReciveDate,TotalPrice")] StockMaster item)
        {

            StockMaster i = (from c in db.StockMasters
                             where c.RecordId == item.RecordId
                             select c).FirstOrDefault();
            ModelState.Remove("EmployeeID");
            if (ModelState.IsValid)
            {
                i.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
                i.StoreId = item.StoreId;
                i.ClassId = item.ClassId;
                i.ItemId = item.ItemId;
                i.BatchNo = item.BatchNo;
                i.UomId = item.UomId;
                i.VendorId = item.VendorId;
                i.BatchNo = item.BatchNo;
                i.Quantity = item.Quantity;
                i.UomId = item.UomId;
                i.PInvoiceNo = item.PInvoiceNo;
                i.TotalPrice = item.TotalPrice;
                i.ReciveDate = item.ReciveDate;
                i.CreatedDate = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("ViewAll");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockDetails vendorList = (from a in db.StockMasters
                                       join b in db.StoreMasters on a.StoreId equals b.RecordId
                                       join c in db.ClassMasters on a.ClassId equals c.RecordId
                                       join d in db.ItemMasters on a.ItemId equals d.RecordId
                                       join e in db.UomMasters on a.UomId equals e.RecordId
                                       join f in db.VendorMasters on a.VendorId equals f.RecordId
                                       join g in db.PurchaseOrders on a.PInvoiceNo equals g.OrderNo
                                       join u in db.UserMasters on a.EmployeeID equals u.EmployeeCode
                                       where a.RecordId == id
                                       select new StockDetails() { stock = a, store = b, classMas = c, item = d, uom = e, vendor = f, po = g, empName = u.UserName }).Single();

            if (vendorList == null)
            {
                return HttpNotFound();
            }
            return View(vendorList);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockMaster ven = db.StockMasters.Find(id);
            db.StockMasters.Remove(ven);
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }
    }
}