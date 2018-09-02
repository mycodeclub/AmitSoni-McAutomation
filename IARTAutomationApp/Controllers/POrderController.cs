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
    
    public class POrderController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        // GET: POrder
        public ActionResult Create()
        {
            ViewBag.Vendor = new SelectList(GetVendor(), "Value", "Text");
            ViewBag.Item = new SelectList(GetItem(), "Value", "Text");
            return View();
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

        [HttpPost]
        public ActionResult Create([Bind(Include = "OrderNo, VendorId, ItemId,ItemQunt,ItemDesc,ItemTax,DeliLoc,Terms")] PurchaseOrder porder)
        {
            PurchaseOrder po = new PurchaseOrder();
            ModelState.Remove("EmployeeID");
            if (ModelState.IsValid)
            {
                po.OrderNo = porder.OrderNo;
                po.VendorId = porder.VendorId;
                po.ItemId = porder.ItemId;
                po.ItemQunt = porder.ItemQunt;
                po.ItemDesc = porder.ItemDesc;
                po.ItemTax = porder.ItemTax;
                po.DeliLoc = porder.DeliLoc;
                po.Terms = porder.Terms;
                po.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
                po.CreatedDate = DateTime.Now;
                db.PurchaseOrders.Add(po);
                db.SaveChanges();

            }
            return RedirectToAction("Create");
        }

        public ActionResult ViewAll()
        {
            ViewBag.TotalPOCount = (from a in db.PurchaseOrders select a).ToList().Count();
            var Porder = from a in db.PurchaseOrders
                         join b in db.VendorMasters on a.VendorId equals b.RecordId
                         join c in db.ItemMasters on a.ItemId equals c.RecordId
                         join u in db.UserMasters on a.EmployeeID equals u.EmployeeCode
                         select new PurchaseOrderDetails() { porder = a, vendor = b, item = c, empName = u.UserName };
            return View(Porder);

        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Vendor = new SelectList(GetVendor(), "Value", "Text");
            ViewBag.Item = new SelectList(GetItem(), "Value", "Text");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder uom = db.PurchaseOrders.Find(id);
            if (uom == null)
            {
                return HttpNotFound();
            }
            return View(uom);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "RecordId,OrderNo, VendorId, ItemId,ItemQunt,ItemDesc,ItemTax,DeliLoc,Terms")] PurchaseOrder porder)
        {
            PurchaseOrder po = (from c in db.PurchaseOrders
                                where c.RecordId == porder.RecordId
                                select c).FirstOrDefault();
            po.OrderNo = porder.OrderNo;
            po.VendorId = porder.VendorId;
            po.ItemId = porder.ItemId;
            po.ItemQunt = porder.ItemQunt;
            po.ItemDesc = porder.ItemDesc;
            po.ItemTax = porder.ItemTax;
            po.DeliLoc = porder.DeliLoc;
            po.Terms = porder.Terms;
            po.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
            po.CreatedDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder uom = db.PurchaseOrders.Find(id);
            if (uom == null)
            {
                return HttpNotFound();
            }
            return View(uom);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrder uom = db.PurchaseOrders.Find(id);
            db.PurchaseOrders.Remove(uom);
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }
    }
}