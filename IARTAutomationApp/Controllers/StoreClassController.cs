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

    public class StoreClassController : Controller
    {
        // GET: StoreClass
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        public ActionResult Create()
        {
            ViewBag.Status = new SelectList(db.StatusMasters, "RecordId", "StatusName");
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "CustomerId,ClassNumber, ClassName, ClassStatus")] ClassMaster classMaster)
        {
            ClassMaster cls = new ClassMaster();
            if (ModelState.IsValid)
            {
                cls.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
                cls.ClassName = classMaster.ClassName;
                cls.ClassNumber = classMaster.ClassNumber;
                cls.ClassStatus = classMaster.ClassStatus;
                cls.CreatedDate = DateTime.Now;
                db.ClassMasters.Add(cls);
                db.SaveChanges();


            }
            return RedirectToAction("Create");
        }
        public ActionResult ViewAll()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            ViewBag.TotalClassCount = (from a in db.ClassMasters where a.CustomerId == user.CustomerId select a).ToList().Count();
            ViewBag.ClassActiveCount = (from a in db.ClassMasters where a.CustomerId == user.CustomerId && a.ClassStatus == 1 select a).ToList().Count();
            ViewBag.ClassClosedCount = (from a in db.ClassMasters where a.CustomerId == user.CustomerId && a.ClassStatus == 2 select a).ToList().Count();
            var classList = from c in db.ClassMasters
                            where c.CustomerId == user.CustomerId
                            join u in db.UserMasters on c.EmployeeID equals u.EmployeeCode
                            join s in db.StatusMasters on c.ClassStatus equals s.RecordId
                            select new ClassDetails()
                            {
                                className = c.ClassName,
                                classNumber = c.ClassNumber,
                                createdDate = c.CreatedDate,
                                empid = c.EmployeeID,
                                empName = u.UserName,
                                RecordId = c.RecordId,
                                status = s.StatusName
                            };
            return View(classList.ToList());
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Status = new SelectList(db.StatusMasters, "RecordId", "StatusName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassMaster cls = db.ClassMasters.Find(id);
            if (cls == null)
            {
                return HttpNotFound();
            }
            return View(cls);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CustomerId,RecordId, ClassNumber, ClassName, ClassStatus")] ClassMaster classMaster)
        {
            ClassMaster cls = (from c in db.ClassMasters
                               where c.RecordId == classMaster.RecordId
                               select c).FirstOrDefault();
            cls.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
            cls.ClassName = classMaster.ClassName;
            cls.ClassNumber = classMaster.ClassNumber;
            cls.ClassStatus = classMaster.ClassStatus;
            cls.CreatedDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassMaster cls = db.ClassMasters.Find(id);
            if (cls == null)
            {
                return HttpNotFound();
            }
            return View(cls);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassMaster cls = db.ClassMasters.Find(id);
            db.ClassMasters.Remove(cls);
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }
    }
}