using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Controllers
{

    public class ItemController : Controller
    {
        // GET: Item
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        public ActionResult Create()
        {
            ViewBag.Store = new SelectList(GetStore(), "Value", "Text");
            ViewBag.Class = new SelectList(GetClass(), "Value", "Text");
            ViewBag.Uom = new SelectList(GetUom(), "Value", "Text");
            ViewBag.Vendor = new SelectList(GetVendor(), "Value", "Text");
            ViewBag.Status = new SelectList(GetStatus(), "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "CustomerId,StoreId, ClassId, ItemName,ItemCat,UomId,VendorId,ItemRate,ItemTax,StatusId,ItemDesc")] ItemMaster item, HttpPostedFileBase ItemImage)
        {

            ItemMaster i = new ItemMaster();
            ModelState.Remove("ItemImage");
            if (ModelState.IsValid)
            {
                if (ItemImage != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ItemImage.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    decimal size = Math.Round(((decimal)ItemImage.ContentLength / (decimal)1024), 2);

                    if (width == 300 || height == 250)
                    {
                        i.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
                        i.StoreId = item.StoreId;
                        i.ClassId = item.ClassId;
                        i.ItemName = item.ItemName;
                        i.ItemCat = item.ItemCat;
                        i.UomId = item.UomId;
                        i.VendorId = item.VendorId;
                        i.ItemRate = item.ItemRate;
                        i.ItemTax = item.ItemTax;
                        i.StatusId = item.StatusId;
                        i.ItemDesc = item.ItemDesc;
                        i.ItemImage = (item.ItemName + "_" + Path.GetFileName(ItemImage.FileName));
                        i.CreatedDate = DateTime.Now;
                        db.ItemMasters.Add(i);
                        db.SaveChanges();
                        ItemImage.SaveAs(path + (item.ItemName + "_" + Path.GetFileName(ItemImage.FileName)));
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        ViewBag.Store = new SelectList(GetStore(), "Value", "Text");
                        ViewBag.Class = new SelectList(GetClass(), "Value", "Text");
                        ViewBag.Uom = new SelectList(GetUom(), "Value", "Text");
                        ViewBag.Vendor = new SelectList(GetVendor(), "Value", "Text");
                        ViewBag.Status = new SelectList(GetStatus(), "Value", "Text");
                        ViewBag.ImgMessage = string.Format("Please Select Image With resolution of 300px X 250px .\\nCurrent resolution is Width: {0}px and Heigth: {1}px", width.ToString(), height.ToString());
                        return View();
                    }
                }
                else
                {
                    ViewBag.Store = new SelectList(GetStore(), "Value", "Text");
                    ViewBag.Class = new SelectList(GetClass(), "Value", "Text");
                    ViewBag.Uom = new SelectList(GetUom(), "Value", "Text");
                    ViewBag.Vendor = new SelectList(GetVendor(), "Value", "Text");
                    ViewBag.Status = new SelectList(GetStatus(), "Value", "Text");
                    ViewBag.ImgMessage = string.Format("Please select image");
                    return View();

                }

            }
            return RedirectToAction("Create");

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

        private List<SelectListItem> GetStatus()
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.StatusMasters.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.StatusName,
                                                    Value = p.RecordId.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Status", Value = "" });
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

        private List<SelectListItem> GetUom()
        {
            var user = (UserMaster)Session["User"];
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

        public ActionResult ViewAll()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            ViewBag.ItemActiveCount = (from a in db.ItemMasters where a.StatusId == 1 && user.CustomerId == a.CustomerId select a).ToList().Count();
            ViewBag.ItemClosedCount = (from a in db.ItemMasters where a.StatusId == 2 && user.CustomerId == a.CustomerId select a).ToList().Count();
            ViewBag.TotalItemCount = (from a in db.ItemMasters where user.CustomerId == a.CustomerId select a).ToList().Count();
            var item = from a in db.ItemMasters
                       where user.CustomerId == a.CustomerId
                       join b in db.StoreMasters on a.StoreId equals b.RecordId
                       join c in db.StatusMasters on a.StatusId equals c.RecordId
                       join d in db.ClassMasters on a.ClassId equals d.RecordId
                       join e in db.UomMasters on a.UomId equals e.RecordId
                       join f in db.VendorMasters on a.VendorId equals f.RecordId
                       join g in db.UserMasters on a.EmployeeID equals g.EmployeeCode
                       select new ItemDetails() { item = a, store = b, status = c, classMas = d, uom = e, vendor = f, empName = g.UserName };
            return View(item);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Store = new SelectList(GetStore(), "Value", "Text");
            ViewBag.Class = new SelectList(GetClass(), "Value", "Text");
            ViewBag.Uom = new SelectList(GetUom(), "Value", "Text");
            ViewBag.Vendor = new SelectList(GetVendor(), "Value", "Text");
            ViewBag.Status = new SelectList(GetStatus(), "Value", "Text");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster item = db.ItemMasters.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CustomerId,RecordId,StoreId, ClassId, ItemName,ItemCat,UomId,VendorId,ItemRate,ItemTax,StatusId,ItemDesc")] ItemMaster item)
        {
            ItemMaster i = (from c in db.ItemMasters
                            where c.RecordId == item.RecordId
                            select c).FirstOrDefault();

            var file = Request.Files["ImageUpload"];

            if (file != null && file.ContentLength > 0)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                file.SaveAs(path + (item.ItemName + "_" + Path.GetFileName(file.FileName)));
                i.ItemImage = item.ItemName + "_" + Path.GetFileName(file.FileName);

            }
            i.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
            i.StoreId = item.StoreId;
            i.ClassId = item.ClassId;
            i.ItemName = item.ItemName;
            i.ItemCat = item.ItemCat;
            i.UomId = item.UomId;
            i.VendorId = item.VendorId;
            i.ItemRate = item.ItemRate;
            i.ItemTax = item.ItemTax;
            i.StatusId = item.StatusId;
            i.ItemDesc = item.ItemDesc;
            i.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("ViewAll");

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster stores = db.ItemMasters.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemMaster stores = db.ItemMasters.Find(id);
            db.ItemMasters.Remove(stores);
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }


    }
}