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

    public class SalaryPaymentsController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: SalaryPayments
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            return View(db.SalaryPayments.Where(e => e.CustomerId == user.CustomerId).ToList());
        }

        // GET: SalaryPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryPayment salaryPayment = db.SalaryPayments.Find(id);
            if (salaryPayment == null)
            {
                return HttpNotFound();
            }
            return View(salaryPayment);
        }

        // GET: SalaryPayments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalaryPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,SalaryPaymId,EmployeeCode,SalaryAmount,Month,Year,DeductionAmount,AllowanceAmount,OtherDeduction,OtherAllowance,PaymentDate,IsPaid,IsSalarySlipPrint,IsDeleted,CreatedDate")] SalaryPayment salaryPayment)
        {
            if (ModelState.IsValid)
            {
                db.SalaryPayments.Add(salaryPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salaryPayment);
        }

        // GET: SalaryPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryPayment salaryPayment = db.SalaryPayments.Find(id);
            if (salaryPayment == null)
            {
                return HttpNotFound();
            }
            return View(salaryPayment);
        }

        // POST: SalaryPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,SalaryPaymId,EmployeeCode,SalaryAmount,Month,Year,DeductionAmount,AllowanceAmount,OtherDeduction,OtherAllowance,PaymentDate,IsPaid,IsSalarySlipPrint,IsDeleted,CreatedDate")] SalaryPayment salaryPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salaryPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salaryPayment);
        }

        // GET: SalaryPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryPayment salaryPayment = db.SalaryPayments.Find(id);
            if (salaryPayment == null)
            {
                return HttpNotFound();
            }
            return View(salaryPayment);
        }

        // POST: SalaryPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalaryPayment salaryPayment = db.SalaryPayments.Find(id);
            db.SalaryPayments.Remove(salaryPayment);
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
