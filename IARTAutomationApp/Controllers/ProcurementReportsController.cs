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
            var model = db.TendererInformations.ToList();
            return View(model);
        }

       

        //[HttpPost]
        //public ActionResult TenderinformationReport(int? id)
        //{
        //    int SrNo = 1;
        //    IARTDBNEWEntities entities = new IARTDBNEWEntities();
        //    DataTable dt = new DataTable("Grid");
        //    dt.Columns.AddRange(new DataColumn[6] {new DataColumn("SR.No"), new DataColumn("Name of Representative"),
        //                                                    new DataColumn("Name of Company"),

        //                                    new DataColumn("Date of Submission of Bid"),
        //                                                                                                new DataColumn("Project Title"),
                                                                                                       
        //                                                                               new DataColumn("Phone No")
        //    });

        //    var ti = from a in db.TendererInformations
                            
        //                    select new { t = a };


        //    foreach (var tinfo in ti)
        //    {
        //        dt.Rows.Add(SrNo++,tinfo.t.RepresentativeName,tinfo.t.CompanyName,tinfo.t.SubmissionDate,tinfo.t.ProjectTitle,tinfo.t.PhoneNo);
        //    }

        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.ShowRowColHeaders = true;
        //        wb.Worksheets.Add(dt);

        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TenderInformationreports.xlsx");
        //        }
        //    }


        //}

        public ActionResult TenderOpeningReport()
        {
            var model = db.TenderOpenings.ToList();
            return View(model);
        }

        //S/No	NAMEOF COMPANY	NAME OF REPRESENTATIVE	LOT NO	PROJECT TITLE	AMOUNT QUOTED	COMPLETION PERIOD	YEAR OF PROJECT														

        //[HttpPost]
        //public ActionResult TenderOpeningReport(int? id)
        //{
        //    int SrNo = 1;
        //    IARTDBNEWEntities entities = new IARTDBNEWEntities();
        //    DataTable dt = new DataTable("Grid");
        //    dt.Columns.AddRange(new DataColumn[] {new DataColumn("SR.No"),  new DataColumn("Name of Company"),new DataColumn("Name of Representative"),
        //                                                   new DataColumn("LOT No."),
        //                                                        new DataColumn("Project Title"),
        //                                                             new DataColumn("Amount Quoted"),

        //                                    new DataColumn("Completion Period"),
                                                                                                   

        //                                                                               new DataColumn("Year Of Project")
        //    });

        //    var to = from a in db.TenderOpenings

        //             select new { t = a };


        //    foreach (var tinfo in to)
        //    {
        //        TimeSpan CompletionPeriod = Convert.ToDateTime(tinfo.t.CompletionPeriodTo) - Convert.ToDateTime(tinfo.t.CompletionPeriodFrom);
        //        dt.Rows.Add(SrNo++,  tinfo.t.CompanyName,tinfo.t.RepresentativeName, tinfo.t.LotNo, tinfo.t.ProjectTitle, tinfo.t.AmountQuoted, CompletionPeriod,tinfo.t.YearofProject);
        //    }

        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.ShowRowColHeaders = true;
        //        wb.Worksheets.Add(dt);

        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TenderOpeningReport.xlsx");
        //        }
        //    }


        //}

        public ActionResult ContractorAssessmentReport()
        {
            var model = db.PrequalificationScorings.ToList();
            return View(model);
        }


        //S/No	NAME OF CONTRACTOR	NAME OF COMPANY	PROJECT TITLE	LOT NO	FINAL ASSESSMENT SCORE																

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
                 dt.Rows.Add(SrNo++, tinfo.t.ContractorName,tinfo.t.CompanyName,tinfo.t.ProjectTitle, tinfo.t.LotNo, tinfo.t.FinalScore);
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
