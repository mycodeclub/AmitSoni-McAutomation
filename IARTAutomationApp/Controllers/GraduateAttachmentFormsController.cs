using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IARTAutomationApp.Models;

namespace IARTAutomationApp.Controllers
{
    public class GraduateAttachmentFormsController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: GraduateAttachmentForms
        public ActionResult Index()
        {
            var graduateAttachmentForms = db.GraduateAttachmentForms.Include(g => g.EmployeeGI).Include(g => g.EmployeeGI1);
            return View(graduateAttachmentForms.ToList());
        }

        // GET: GraduateAttachmentForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GraduateAttachmentForm graduateAttachmentForm = db.GraduateAttachmentForms.Find(id);
            if (graduateAttachmentForm == null)
            {
                return HttpNotFound();
            }
            return View(graduateAttachmentForm);
        }

        // GET: GraduateAttachmentForms/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank");
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank");
            return View();
        }

        // POST: GraduateAttachmentForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeCode,OurRef,YourRef,Date,Name,LetterDated,FromDate,ToDate,OfficerInCharge,PrincipalAccountant,ReinstatePayment,PaymentToDate,PaymentFromDate,CreatedDate,IsDeleted")] GraduateAttachmentForm graduateAttachmentForm)
        {
            if (ModelState.IsValid)
            {
                db.GraduateAttachmentForms.Add(graduateAttachmentForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", graduateAttachmentForm.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", graduateAttachmentForm.EmployeeCode);
            return View(graduateAttachmentForm);
        }

        // GET: GraduateAttachmentForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GraduateAttachmentForm graduateAttachmentForm = db.GraduateAttachmentForms.Find(id);
            if (graduateAttachmentForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", graduateAttachmentForm.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", graduateAttachmentForm.EmployeeCode);
            return View(graduateAttachmentForm);
        }

        // POST: GraduateAttachmentForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeCode,OurRef,YourRef,Date,Name,LetterDated,FromDate,ToDate,OfficerInCharge,PrincipalAccountant,ReinstatePayment,PaymentToDate,PaymentFromDate,CreatedDate,IsDeleted")] GraduateAttachmentForm graduateAttachmentForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(graduateAttachmentForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", graduateAttachmentForm.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", graduateAttachmentForm.EmployeeCode);
            return View(graduateAttachmentForm);
        }

        // GET: GraduateAttachmentForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GraduateAttachmentForm graduateAttachmentForm = db.GraduateAttachmentForms.Find(id);
            if (graduateAttachmentForm == null)
            {
                return HttpNotFound();
            }
            return View(graduateAttachmentForm);
        }

        // POST: GraduateAttachmentForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GraduateAttachmentForm graduateAttachmentForm = db.GraduateAttachmentForms.Find(id);
            db.GraduateAttachmentForms.Remove(graduateAttachmentForm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
