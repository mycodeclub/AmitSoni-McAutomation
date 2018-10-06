using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;
using ClosedXML.Excel;
using System.Data;
using System.IO;

namespace IARTAutomationApp.Controllers
{

    public class ProcurementReportsController : Controller
    {
        IARTDBNEWEntities db = new IARTDBNEWEntities();

        public ActionResult TenderinformationReport()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            var model = db.TendererInformations.Where(e => e.CustomerId == user.CustomerId).ToList();
            return View(model);
        }

        public ActionResult TenderOpeningReport()
        {
            var model = db.TenderOpenings.ToList();
            return View(model);
        }

        public ActionResult ContractorAssessmentReport()
        {
            var model = db.PrequalificationScorings.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult ContractorAssessmentReport(int? id)
        {
            int SrNo = 1;
            IARTDBNEWEntities entities = new IARTDBNEWEntities();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6] {new DataColumn("SR.No"),new DataColumn("Name of Contractor"),  new DataColumn("Name of Company"),

                                                                new DataColumn("Project Title"),
                                                                  new DataColumn("LOT No."),
                                                                     new DataColumn("Final Assessment Score")


            });

            var to = from a in db.PrequalificationScorings

                     select new { t = a };


            foreach (var tinfo in to)
            {
                dt.Rows.Add(SrNo++, tinfo.t.ContractorName, tinfo.t.CompanyName, tinfo.t.ProjectTitle, tinfo.t.LotNo, tinfo.t.FinalScore);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.ShowRowColHeaders = true;
                wb.Worksheets.Add(dt);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ContractorAssessmentScoreReport.xlsx");
                }
            }

        }


        // GET: ProcurementReports
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProcurementReports/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProcurementReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProcurementReports/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProcurementReports/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProcurementReports/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProcurementReports/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProcurementReports/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
