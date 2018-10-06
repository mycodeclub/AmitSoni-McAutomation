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

    public class StoreController : Controller
    {
        // GET: Store
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        public ActionResult Create()
        {
            ViewBag.Status = new SelectList(db.StatusMasters, "RecordId", "StatusName");
            ViewBag.TitleHead = "Add Store";
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "CustomerId,StoreNumber,StoreName,StoreStatus,StoreDesc,StoreImgName")] StoreMaster storeMaster, HttpPostedFileBase StoreImgName)
        {
            StoreMaster s = new StoreMaster();
            ViewBag.Status = new SelectList(db.StatusMasters, "RecordId", "StatusName");
            if (StoreImgName != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                System.Drawing.Image img = System.Drawing.Image.FromStream(StoreImgName.InputStream);
                int height = img.Height;
                int width = img.Width;
                decimal size = Math.Round(((decimal)StoreImgName.ContentLength / (decimal)1024), 2);

                if (width == 300 || height == 250)
                {
                    s.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
                    s.CreatedDate = DateTime.Now;
                    s.StoreName = storeMaster.StoreName;
                    s.StoreNumber = storeMaster.StoreNumber;
                    s.StoreStatus = storeMaster.StoreStatus;
                    s.StoreImgName = (storeMaster.StoreName + "_" + Path.GetFileName(StoreImgName.FileName));
                    s.StoreDesc = storeMaster.StoreDesc;
                    db.StoreMasters.Add(s);
                    db.SaveChanges();
                    StoreImgName.SaveAs(path + (storeMaster.StoreName + "_" + Path.GetFileName(StoreImgName.FileName)));
                    return RedirectToAction("Create");
                }
                else
                {
                    ViewBag.ImgMessage = string.Format("Please Select Image With resolution of 300px X 250px .\\nCurrent resolution is Width: {0}px and Heigth: {1}px", width.ToString(), height.ToString());
                    return View();
                }
            }
            else
            {
                ViewBag.ImgMessage = string.Format("Please select image");
                return View();

            }
        }

        public ActionResult ViewAll()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];

            var storeCode = from a in db.StoreMasters
                            where a.CustomerId == user.CustomerId
                            let cc = (
                from s in db.ItemMasters
                where a.RecordId == s.StoreId
                select s
                ).Count()
                            select new StoreViewAllDetails()
                            {
                                store = a.StoreName,
                                storeDesc = a.StoreDesc,
                                count = cc,
                                storeImg = a.StoreImgName
                            };
            return View(storeCode.ToList());
        }

        public ActionResult StoreList()
        {
            var u = (IARTAutomationApp.Models.UserMaster)Session["User"];
            var StoreActiveCount = (from a in db.StoreMasters where a.StoreStatus == 1 && a.CustomerId == u.CustomerId select a).ToList().Count();
            var StoreClosedCount = (from a in db.StoreMasters where a.StoreStatus == 2 && a.CustomerId == u.CustomerId select a).ToList().Count();
            var TotalStoreCount = (from a in db.StoreMasters where a.CustomerId == u.CustomerId select a).ToList().Count();
            var TotalStoreItemCount = (from a in db.ItemMasters where a.CustomerId == u.CustomerId select a).ToList().Count();
            ViewBag.StoreActiveCount = StoreActiveCount;
            ViewBag.StoreClosedCount = StoreClosedCount;
            ViewBag.TotalStoreCount = TotalStoreCount;
            ViewBag.TotalStoreItemCount = TotalStoreItemCount;
            var storeList = from a in db.StoreMasters
                            where a.CustomerId == u.CustomerId
                            join user in db.UserMasters on a.EmployeeID equals user.EmployeeCode
                            join status in db.StatusMasters on a.StoreStatus equals status.RecordId

                            select new StoreListDetails()
                            {
                                recordId = a.RecordId,
                                storeImage = a.StoreImgName,
                                storeName = a.StoreName,
                                storeNumber = a.StoreNumber,
                                storeDesc = a.StoreDesc,
                                storeStatus = status.StatusName,
                                empId = a.EmployeeID,
                                empName = user.UserName,
                                createdDate = a.CreatedDate
                            };
            return View(storeList.ToList());
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Status = new SelectList(db.StatusMasters, "CustomerId,RecordId", "StatusName");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreMaster stores = db.StoreMasters.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CustomerId,RecordId,StoreNumber,StoreName,StoreStatus,StoreDesc")] StoreMaster store)
        {
            StoreMaster s = (from c in db.StoreMasters
                             where c.RecordId == store.RecordId
                             select c).FirstOrDefault();
            ViewBag.Status = new SelectList(db.StatusMasters, "RecordId", "StatusName");
            var file = Request.Files["ImageUpload"];

            if (file != null && file.ContentLength > 0)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                int height = img.Height;
                int width = img.Width;
                decimal size = Math.Round(((decimal)file.ContentLength / (decimal)1024), 2);
                if (width == 300 || height == 250)
                {
                    file.SaveAs(path + (store.StoreName + "_" + Path.GetFileName(file.FileName)));
                    s.StoreImgName = store.StoreName + "_" + Path.GetFileName(file.FileName);
                }
                else
                {
                    ViewBag.ImgMessage = string.Format("Please Select Image With resolution of 300px X 250px .\\nCurrent resolution is Width: {0}px and Heigth: {1}px", width.ToString(), height.ToString());
                    return RedirectToAction("Edit/1"); ;
                }
            }
            s.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
            s.StoreName = store.StoreName;
            s.StoreNumber = store.StoreNumber;
            s.StoreStatus = store.StoreStatus;
            s.StoreDesc = store.StoreDesc;
            s.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("StoreList");

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreMaster stores = db.StoreMasters.Find(id);
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
            StoreMaster stores = db.StoreMasters.Find(id);
            db.StoreMasters.Remove(stores);
            db.SaveChanges();
            return RedirectToAction("StoreList");
        }
    }
}