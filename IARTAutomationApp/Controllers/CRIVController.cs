using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Controllers
{
    public class CRIVController : Controller
    {
        // GET: CRIV
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        public ActionResult Create()
        {
            ViewBag.Store = new SelectList(GetStore(), "Value", "Text");
            ViewBag.Class = new SelectList(GetClass(), "Value", "Text");
            ViewBag.Item = new SelectList(GetItem(), "Value", "Text");
            ViewBag.Uom = new SelectList(GetUom(), "Value", "Text");
            return View();
        }

        public JsonResult InsertCriv(List<CrivVoucher> criv)
        {
            using (db)
            {
                if (criv == null)
                {
                    criv = new List<CrivVoucher>();
                }

                //Loop and insert records.
                foreach (CrivVoucher c in criv)
                {
                    CrivVoucher cr = new CrivVoucher();
                    cr.CRIVVoucherId = c.CRIVVoucherId;
                    cr.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
                    cr.DepartmentId = c.DepartmentId;
                    cr.Store = c.Store;
                    cr.DateIns = c.DateIns;
                    cr.Item = c.Item;
                    cr.Class = c.Class;
                    cr.Qunatity = c.Qunatity;
                    cr.Uom = c.Uom;
                    cr.Rate = c.Rate;
                    cr.TotalValue = c.TotalValue;
                    cr.BatchDetail = c.BatchDetail;
                    cr.RequisitionOfficer = c.RequisitionOfficer;
                    cr.RecivingPerson = c.RecivingPerson;
                    cr.Remarks = c.Remarks;
                    cr.CreatedDate = DateTime.Now;
                    cr.TotalCost = c.TotalCost;
                    cr.AmtWord = c.AmtWord;
                    cr.Name1 = c.Name1;
                    cr.Fac1 = c.Fac1;
                    cr.Sign1 = c.Sign1;
                    cr.Date1 = c.Date1;
                    cr.Sign2 = c.Sign2;
                    cr.Date2 = c.Date2;
                    cr.Name3 = c.Name3;
                    cr.Fac3 = c.Fac3;
                    cr.Date3 = c.Date3;
                    db.CrivVouchers.Add(cr);
                }
                int insertedRecords = db.SaveChanges();
                insertedRecords = 0;
                return Json(insertedRecords);
            }
        }

        private static List<SelectListItem> GetStore()
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.StoreMasters.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.StoreName,
                                                    Value = p.StoreName.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Store", Value = "" });
            return storeStatus;
        }

        private static List<SelectListItem> GetClass()
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.ClassMasters.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.ClassName,
                                                    Value = p.ClassName.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Class", Value = "" });
            return storeStatus;
        }

        private static List<SelectListItem> GetItem()
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.ItemMasters.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.ItemName,
                                                    Value = p.ItemName.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Item", Value = "" });
            return storeStatus;
        }

        private static List<SelectListItem> GetUom()
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.UomMasters.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.UOMName,
                                                    Value = p.UOMName.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select UOM", Value = "" });
            return storeStatus;
        }


        public ActionResult ViewAll()
        {
            ViewBag.CrivCount = (from a in db.CrivVouchers group a by a.CRIVVoucherId into a select a).ToList().Count();

            var crivList = (from a in db.CrivVouchers

                            join d in db.UserMasters on a.EmployeeID equals d.EmployeeCode
                            select new CRIVDetails() { criv = a, Emp = d.UserName }).ToList();

            var finalList = crivList.GroupBy(a => a.criv.CRIVVoucherId).Select(b => b.First()).ToList();

            
            return View(finalList);
        }

        public ActionResult Details(int id)
        {
            var crivList = (from a in db.CrivVouchers where a.CRIVVoucherId == id select a);
                            
            return View(crivList);
        }

    }
}