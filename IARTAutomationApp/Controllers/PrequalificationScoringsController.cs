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

    public class PrequalificationScoringsController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: PrequalificationScorings
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            return View(db.PrequalificationScorings.Where(e => e.CustomerId == user.CustomerId).ToList());
        }

        // GET: PrequalificationScorings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrequalificationScoring prequalificationScoring = db.PrequalificationScorings.Find(id);
            if (prequalificationScoring == null)
            {
                return HttpNotFound();
            }
            return View(prequalificationScoring);
        }

        // GET: PrequalificationScorings/Create
        public ActionResult Create()
        {
            List<SelectListItem> numlist = new List<SelectListItem>();
            numlist.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1; i <= 10; i++)
            {
                numlist.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            //10%
            ViewBag.AudittedAccount = new SelectList(numlist, "Value", "Text");
            ViewBag.ClearanceCert_Itf = new SelectList(numlist, "Value", "Text");
            ViewBag.ClearanceCert_Pencom = new SelectList(numlist, "Value", "Text");

            ViewBag.CurrentFinStatus = new SelectList(numlist, "Value", "Text");
            ViewBag.EquipmentList = new SelectList(numlist, "Value", "Text");

            List<SelectListItem> numlist5 = new List<SelectListItem>();
            numlist5.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1; i <= 5; i++)
            {
                numlist5.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            //5%
            ViewBag.ClearanceCert_Nsitf = new SelectList(numlist5, "Value", "Text");

            List<SelectListItem> numlist15 = new List<SelectListItem>();
            numlist15.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1; i <= 15; i++)
            {
                numlist15.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            //15%
            ViewBag.StaffStrength = new SelectList(numlist15, "Value", "Text");

            ViewBag.EvidPreSimJob = new SelectList(numlist15, "Value", "Text");
            ViewBag.ExpCompt = new SelectList(numlist15, "Value", "Text");


            return View();
        }

        // POST: PrequalificationScorings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,Id,CompanyName,ProjectTitle,LotNo,ContractorName,EvidofReg_Cac,TaxClearanceCertificate,EvidofReg_Bureau,AudittedAccount,ClearanceCert_Itf,ClearanceCert_Pencom,ClearanceCert_Nsitf,StaffStrength,CurrentFinStatus,EquipmentList,EvidPreSimJob,ExpCompt,FinalScore,CreatedDate,IsDeleted")] PrequalificationScoring prequalificationScoring)
        {
            if (ModelState.IsValid)
            {
                db.PrequalificationScorings.Add(prequalificationScoring);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prequalificationScoring);
        }

        // GET: PrequalificationScorings/Edit/5
        public ActionResult Edit(int? id)
        {
            List<SelectListItem> numlist = new List<SelectListItem>();
            numlist.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1; i <= 10; i++)
            {
                numlist.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            //10%
            ViewBag.AudittedAccount = new SelectList(numlist, "Value", "Text");
            ViewBag.ClearanceCert_Itf = new SelectList(numlist, "Value", "Text");
            ViewBag.ClearanceCert_Pencom = new SelectList(numlist, "Value", "Text");

            ViewBag.CurrentFinStatus = new SelectList(numlist, "Value", "Text");
            ViewBag.EquipmentList = new SelectList(numlist, "Value", "Text");

            List<SelectListItem> numlist5 = new List<SelectListItem>();
            numlist5.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1; i <= 5; i++)
            {
                numlist5.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            //5%
            ViewBag.ClearanceCert_Nsitf = new SelectList(numlist5, "Value", "Text");

            List<SelectListItem> numlist15 = new List<SelectListItem>();
            numlist15.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1; i <= 15; i++)
            {
                numlist15.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            //15%
            ViewBag.StaffStrength = new SelectList(numlist15, "Value", "Text");

            ViewBag.EvidPreSimJob = new SelectList(numlist15, "Value", "Text");
            ViewBag.ExpCompt = new SelectList(numlist15, "Value", "Text");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrequalificationScoring prequalificationScoring = db.PrequalificationScorings.Find(id);
            if (prequalificationScoring == null)
            {
                return HttpNotFound();
            }
            return View(prequalificationScoring);
        }

        // POST: PrequalificationScorings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Id,CompanyName,ProjectTitle,LotNo,ContractorName,EvidofReg_Cac,TaxClearanceCertificate,EvidofReg_Bureau,AudittedAccount,ClearanceCert_Itf,ClearanceCert_Pencom,ClearanceCert_Nsitf,StaffStrength,CurrentFinStatus,EquipmentList,EvidPreSimJob,ExpCompt,FinalScore,CreatedDate,IsDeleted")] PrequalificationScoring prequalificationScoring)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prequalificationScoring).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prequalificationScoring);
        }

        // GET: PrequalificationScorings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrequalificationScoring prequalificationScoring = db.PrequalificationScorings.Find(id);
            if (prequalificationScoring == null)
            {
                return HttpNotFound();
            }
            return View(prequalificationScoring);
        }

        // POST: PrequalificationScorings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrequalificationScoring prequalificationScoring = db.PrequalificationScorings.Find(id);
            db.PrequalificationScorings.Remove(prequalificationScoring);
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
