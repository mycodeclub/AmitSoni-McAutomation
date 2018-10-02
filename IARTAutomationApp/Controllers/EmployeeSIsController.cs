using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;

namespace IARTAutomationApp.Controllers
{
    public class EmployeeSIsController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();


        public ActionResult UserIndex()
        {

            int empcode = Convert.ToInt32(@Session["employeecode"]);


            var employeeSIs = db.EmployeeSIs.Where(a => a.EmployeeCode == empcode).ToList();
            return View(employeeSIs.ToList());

            //if(     User.Identity.Name)



            return View();
        }
        public ActionResult GetBankList(string banktypeId)
        {
            List<SelectListItem> libanks = new List<SelectListItem>();

            try
            {
                var stateIDs = (db.BankTypeMasters.Where(x => x.BankTypeName == banktypeId).Select(x => x.BankTypeId).FirstOrDefault());
                int stateID = Convert.ToInt32(stateIDs.ToString());
                var Banks = db.BankMasters.Where(x => x.BankTypeId == stateID).ToList();

                libanks.Add(new SelectListItem { Text = "--Select Bank--", Value = "0" });
                if (Banks != null)
                {
                    foreach (var l in Banks)
                    {
                        libanks.Add(new SelectListItem { Text = l.BankName, Value = l.BankName.ToString() });

                    }
                }
                return Json(new SelectList(libanks, "Value", "Text", JsonRequestBehavior.AllowGet));

            }
            catch (Exception ext)
            {
                return Json(new SelectList(libanks, "Value", "Text", JsonRequestBehavior.AllowGet));

            }
        }


        [HttpPost]
        public JsonResult AutoEmployeeCode(string Prefix)
        {
            var user = (UserMaster)Session["User"];

            //Note : you can bind same list from database  
            ViewBag.emp = new SelectList(db.EmployeeGIs.Where(e => e.CustomerId == user.CustomerId), "EmployeeCode", "EmployeeCode").ToList();
            List<SelectListItem> objlist = ViewBag.emp;
            //Searching records from list using LINQ query  
            var emplist = (from N in objlist
                           where N.Value.ToString().StartsWith(Prefix)
                           select new { N.Value });
            return Json(emplist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FullDetails(int? id)
        {
            EmployeeAll empall = new EmployeeAll();

            empall.employeegi = (from a in db.EmployeeGIs where a.EmployeeCode == id select a).ToList();
            empall.employeeai = (from a in db.EmployeeAIs where a.EmployeeCode == id select a).ToList();
            empall.employeemi = (from a in db.EmployeeMIs where a.EmployeeCode == id select a).ToList();
            empall.employeesi = (from a in db.EmployeeSIs where a.EmployeeCode == id select a).ToList();
            empall.employeepi = (from a in db.EmployeePIs where a.EmployeeCode == id select a).ToList();
            empall.employeeassociation = (from a in db.EmpAIAssociations where a.EmployeeCode == id select a).ToList();
            empall.employeeconference = (from a in db.EmpAIConferences where a.EmployeeCode == id select a).ToList();

            empall.employeeleaveledger = (from a in db.LeaveLedgers where a.EmployeeCode == id select a).ToList();

            return View(empall);

        }
        // GET: EmployeeSIs
        public ActionResult Index()
        {
            var NoofEmpConh = (from a in db.EmployeeSIs where a.SalaryScale == "CONHESS" select a).ToList().Count();
            ViewBag.NoofEmpConh = NoofEmpConh;
            var NoofEmpCont = (from a in db.EmployeeSIs where a.SalaryScale == "CONTTISS" select a).ToList().Count();
            ViewBag.NoofEmpCont = NoofEmpCont;
            var NoofEmpConu = (from a in db.EmployeeSIs where a.SalaryScale == "CONUASS" select a).ToList().Count();
            ViewBag.NoofEmpConu = NoofEmpConu;
            var NoofEmpConm = (from a in db.EmployeeSIs where a.SalaryScale == "CONMESS" select a).ToList().Count();
            ViewBag.NoofEmpConm = NoofEmpConm;
            var employeeSIs = db.EmployeeSIs.Include(e => e.EmployeeGI);
            return View(employeeSIs.ToList());
        }



        // GET: EmployeeSIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSI employeeSI = db.EmployeeSIs.Find(id);
            if (employeeSI == null)
            {
                return HttpNotFound();
            }
            return View(employeeSI);
        }



        // GET: EmployeeSIs/Create
        public ActionResult Create()
        {
            List<BankTypeMaster> BankTypelist = new List<BankTypeMaster>();
            BankTypelist = (from a in db.BankTypeMasters select a).ToList();
            ViewBag.BankTypes = new SelectList(BankTypelist, " BankTypeName", "BankTypeName");

            ///////////////////Bank list
            List<BankMaster> Banklist = new List<BankMaster>();
            Banklist = (from a in db.BankMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.NameOfBankss = new SelectList(Banklist, " BankName", "BankName");
            //////////////////
            ///////////////////Bank list
            List<PFAMaster> PFAlist = new List<PFAMaster>();
            PFAlist = (from a in db.PFAMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.PFAs = new SelectList(PFAlist, " PFAId", "PFAName");
            //////////////////


            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode");
            return View();
        }

        // POST: EmployeeSIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeSIId,EmployeeCode,CurrentPosting,BankType,NameOfBanks,BankBranch,AccountType,AccountNumber,AccountName,PFA,RSAPinNo,SalaryScale,CreatedDate,IsDeleted")] EmployeeSI employeeSI)
        {
            var isAlready = (from a in db.EmployeeSIs where a.EmployeeCode == employeeSI.EmployeeCode select a.EmployeeCode).Count();
            if (isAlready == 0)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        int empidcount = (from a in db.EmployeeSIs where a.EmployeeCode == employeeSI.EmployeeCode select a).Count();
                        if (empidcount == 0)
                        {
                            employeeSI.CreatedDate = DateTime.Now.Date;
                            db.EmployeeSIs.Add(employeeSI);
                            db.SaveChanges();
                            TempData["successmsg"] = "Record is Successfully Added";
                            TempData["msg"] = "";
                            int empcode = Convert.ToInt32(@Session["employeecode"]);

                            DateTime dttoday = DateTime.Now.Date;
                            var Role = (from a in db.UserMasters where a.EmployeeCode == empcode select a.RoleId).FirstOrDefault();
                            var RoleName = (from b in db.RoleMasters where b.RoleId == Role select b.RoleName).FirstOrDefault();
                            if (RoleName == "Admin" || RoleName == "Super Admin")
                            {
                                return RedirectToAction("Index");
                            }
                            else if (RoleName == "Mid Level Admin")
                            {
                                return RedirectToAction("MidLevelIndex");
                            }
                            else if (RoleName == "Low Level Admin")
                            {
                                return RedirectToAction("MidLevelIndex");
                            }
                            else
                            {
                                return RedirectToAction("UserIndex");
                            }
                        }
                    }
                }
                catch (Exception ext)
                {
                    TempData["successmsg"] = "";
                    TempData["msg"] = "Record is not Added,try again";

                }


                ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeSI.EmployeeCode);
                return View(employeeSI);
            }
            else
            {
                TempData["successmsg"] = "";
                TempData["msg"] = "This Record is Already Exist";
                return View(employeeSI);
            }
        }

        // GET: EmployeeSIs/Edit/5
        public ActionResult Edit(int? id)
        {
            List<BankTypeMaster> BankTypelist = new List<BankTypeMaster>();
            BankTypelist = (from a in db.BankTypeMasters select a).ToList();
            ViewBag.BankTypes = new SelectList(BankTypelist, " BankTypeName", "BankTypeName");

            ///////////////////Bank list
            List<BankMaster> Banklist = new List<BankMaster>();
            Banklist = (from a in db.BankMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.NameOfBankss = new SelectList(Banklist, " BankName", "BankName");
            //////////////////
            ///////////////////Bank list
            List<PFAMaster> PFAlist = new List<PFAMaster>();
            PFAlist = (from a in db.PFAMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.PFAs = new SelectList(PFAlist, " PFAId", "PFAName");
            //////////////////
            //////////////////
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSI employeeSI = db.EmployeeSIs.Find(id);
            if (employeeSI == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeSI.EmployeeCode);
            return View(employeeSI);
        }

        // POST: EmployeeSIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeSIId,EmployeeCode,CurrentPosting,BankType,NameOfBanks,BankBranch,AccountType,AccountNumber,AccountName,PFA,RSAPinNo,SalaryScale,CreatedDate,IsDeleted")] EmployeeSI employeeSI)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeeSI.EmployeeSIId = (from a in db.EmployeeSIs where a.EmployeeCode == employeeSI.EmployeeCode select a.EmployeeSIId).FirstOrDefault();
                    employeeSI.CreatedDate = DateTime.Now;
                    employeeSI.IsDeleted = false;

                    db.Entry(employeeSI).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["successmsg"] = "Record is Successfully Updated";
                    TempData["msg"] = "";

                    int empcode = Convert.ToInt32(@Session["employeecode"]);

                    DateTime dttoday = DateTime.Now.Date;
                    var Role = (from a in db.UserMasters where a.EmployeeCode == empcode select a.RoleId).FirstOrDefault();
                    var RoleName = (from b in db.RoleMasters where b.RoleId == Role select b.RoleName).FirstOrDefault();
                    if (RoleName == "Admin" || RoleName == "Super Admin")
                    {
                        return RedirectToAction("Index");
                    }
                    else if (RoleName == "Mid Level Admin")
                    {
                        return RedirectToAction("MidLevelIndex");
                    }
                    else if (RoleName == "Low Level Admin")
                    {
                        return RedirectToAction("MidLevelIndex");
                    }
                    else
                    {
                        return RedirectToAction("UserIndex");
                    }
                }
            }
            catch (Exception ext)
            {
                TempData["successmsg"] = "";
                TempData["msg"] = "Record is not Updated";

            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeSI.EmployeeCode);
            return View(employeeSI);
        }


        #region MidLevelAdmin
        public ActionResult MidLevelIndex()
        {
            var NoofEmpConh = (from a in db.EmployeeSIs where a.SalaryScale == "CONHESS" select a).ToList().Count();
            ViewBag.NoofEmpConh = NoofEmpConh;
            var NoofEmpCont = (from a in db.EmployeeSIs where a.SalaryScale == "CONTTISS" select a).ToList().Count();
            ViewBag.NoofEmpCont = NoofEmpCont;
            var NoofEmpConu = (from a in db.EmployeeSIs where a.SalaryScale == "CONUASS" select a).ToList().Count();
            ViewBag.NoofEmpConu = NoofEmpConu;
            var NoofEmpConm = (from a in db.EmployeeSIs where a.SalaryScale == "CONMESS" select a).ToList().Count();
            ViewBag.NoofEmpConm = NoofEmpConm;
            var employeeSIs = db.EmployeeSIs.Include(e => e.EmployeeGI);
            return View(employeeSIs.ToList());
        }
        // GET: EmployeeSIs/Create
        public ActionResult MidLevelCreate()
        {
            List<BankTypeMaster> BankTypelist = new List<BankTypeMaster>();
            BankTypelist = (from a in db.BankTypeMasters select a).ToList();
            ViewBag.BankType = new SelectList(BankTypelist, " BankTypeId", "BankTypeName");

            ///////////////////Bank list
            List<BankMaster> Banklist = new List<BankMaster>();
            Banklist = (from a in db.BankMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.NameOfBanks = new SelectList(Banklist, " BankId", "BankName");
            //////////////////
            ///////////////////Bank list
            List<PFAMaster> PFAlist = new List<PFAMaster>();
            PFAlist = (from a in db.PFAMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.PFA = new SelectList(PFAlist, " PFAId", "PFAName");
            //////////////////


            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode");
            return View();
        }

        // POST: EmployeeSIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MidLevelCreate([Bind(Include = "EmployeeSIId,EmployeeCode,CurrentPosting,BankType,NameOfBanks,BankBranch,AccountType,AccountNumber,AccountName,PFA,RSAPinNo,SalaryScale,CreatedDate,IsDeleted")] EmployeeSI employeeSI)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int empidcount = (from a in db.EmployeeSIs where a.EmployeeCode == employeeSI.EmployeeCode select a).Count();
                    if (empidcount == 0)
                    {
                        employeeSI.CreatedDate = DateTime.Now.Date;
                        db.EmployeeSIs.Add(employeeSI);
                        db.SaveChanges();
                        TempData["successmsg"] = "Record is Successfully Added";
                        TempData["msg"] = "";
                        int empcode = Convert.ToInt32(@Session["employeecode"]);

                        DateTime dttoday = DateTime.Now.Date;
                        var Role = (from a in db.UserMasters where a.EmployeeCode == empcode select a.RoleId).FirstOrDefault();
                        var RoleName = (from b in db.RoleMasters where b.RoleId == Role select b.RoleName).FirstOrDefault();
                        return RedirectToAction("MidLevelIndex");
                        //if (RoleName == "Admin" || RoleName == "Super Admin")
                        //{
                        //    return RedirectToAction("Index");
                        //}
                        //else if (RoleName == "Mid Level Admin")
                        //{
                        //    return RedirectToAction("MidLevelIndex");
                        //}
                        //else if (RoleName == "Low Level Admin")
                        //{
                        //    return RedirectToAction("MidLevelIndex");
                        //}
                        //else
                        //{
                        //    return RedirectToAction("UserIndex");
                        //}
                    }
                }
            }
            catch (Exception ext)
            {
                TempData["successmsg"] = "";
                TempData["msg"] = "Record is not Added,try again";

            }


            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeSI.EmployeeCode);
            return View(employeeSI);
        }

        // GET: EmployeeSIs/Edit/5
        public ActionResult MidLevelEdit(int? id)
        {
            List<BankTypeMaster> BankTypelist = new List<BankTypeMaster>();
            BankTypelist = (from a in db.BankTypeMasters select a).ToList();
            ViewBag.BankType = new SelectList(BankTypelist, " BankTypeId", "BankTypeName");

            ///////////////////Bank list
            List<BankMaster> Banklist = new List<BankMaster>();
            Banklist = (from a in db.BankMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.NameOfBanks = new SelectList(Banklist, " BankId", "BankName");
            //////////////////
            ///////////////////Bank list
            List<PFAMaster> PFAlist = new List<PFAMaster>();
            PFAlist = (from a in db.PFAMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.PFA = new SelectList(PFAlist, " PFAId", "PFAName");
            //////////////////
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSI employeeSI = db.EmployeeSIs.Find(id);
            if (employeeSI == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeSI.EmployeeCode);
            return View(employeeSI);
        }

        // POST: EmployeeSIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MidLevelEdit([Bind(Include = "EmployeeSIId,EmployeeCode,CurrentPosting,BankType,NameOfBanks,BankBranch,AccountType,AccountNumber,AccountName,PFA,RSAPinNo,SalaryScale,CreatedDate,IsDeleted")] EmployeeSI employeeSI)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeeSI.EmployeeSIId = (from a in db.EmployeeSIs where a.EmployeeCode == employeeSI.EmployeeCode select a.EmployeeSIId).FirstOrDefault();
                    employeeSI.CreatedDate = DateTime.Now;
                    employeeSI.IsDeleted = false;

                    db.Entry(employeeSI).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["successmsg"] = "Record is Successfully Updated";
                    TempData["msg"] = "";

                    int empcode = Convert.ToInt32(@Session["employeecode"]);

                    DateTime dttoday = DateTime.Now.Date;
                    var Role = (from a in db.UserMasters where a.EmployeeCode == empcode select a.RoleId).FirstOrDefault();
                    var RoleName = (from b in db.RoleMasters where b.RoleId == Role select b.RoleName).FirstOrDefault();
                    return RedirectToAction("MidLevelIndex");
                    //if (RoleName == "Admin" || RoleName == "Super Admin")
                    //{
                    //    return RedirectToAction("Index");
                    //}
                    //else if (RoleName == "Mid Level Admin")
                    //{
                    //    return RedirectToAction("MidLevelIndex");
                    //}
                    //else if (RoleName == "Low Level Admin")
                    //{
                    //    return RedirectToAction("MidLevelIndex");
                    //}
                    //else
                    //{
                    //    return RedirectToAction("UserIndex");
                    //}
                }
            }
            catch (Exception ext)
            {
                TempData["successmsg"] = "";
                TempData["msg"] = "Record is not Updated";

            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeSI.EmployeeCode);
            return View(employeeSI);
        }

        #endregion
        // GET: EmployeeSIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSI employeeSI = db.EmployeeSIs.Find(id);
            if (employeeSI == null)
            {
                return HttpNotFound();
            }
            return View(employeeSI);
        }

        // POST: EmployeeSIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                EmployeeSI employeeSI = db.EmployeeSIs.Find(id);
                db.EmployeeSIs.Remove(employeeSI);
                db.SaveChanges();
                TempData["successmsg"] = "Record is Successfully Deleted";
                return RedirectToAction("Index");
            }

            catch (Exception ext)
            {
                TempData["successmsg"] = "Record is not Deleted,try again";
                return RedirectToAction("Index");
            }


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
