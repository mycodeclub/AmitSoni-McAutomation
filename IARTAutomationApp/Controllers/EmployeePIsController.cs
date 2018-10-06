using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IARTAutomationApp.Models;
using System.IO;
using IARTAutomationApp.ViewModels;

namespace IARTAutomationApp.Controllers
{
    public class EmployeePIsController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();


        public ActionResult UserIndex()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            int empcode = Convert.ToInt32(@Session["employeecode"]);
            var employeePIs = db.EmployeePIs.Where(a => a.EmployeeCode == empcode && user.CustomerId == a.CustomerId).ToList();
            return View(employeePIs.ToList());
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

        public ActionResult GetLGAList(string stateId)
        {
            List<SelectListItem> licity = new List<SelectListItem>();

            try
            {
                var stateIDs = (db.StateMasters.Where(x => x.State == stateId).Select(x => x.Id).FirstOrDefault());
                int stateID = Convert.ToInt32(stateIDs.ToString());
                var City = db.CityMasters.Where(x => x.StateId == stateID).ToList();

                licity.Add(new SelectListItem { Text = "--Select LGA--", Value = "0" });
                if (City != null)
                {
                    foreach (var l in City)
                    {
                        licity.Add(new SelectListItem { Text = l.City, Value = l.City.ToString() });

                    }
                }
                return Json(new SelectList(licity, "Value", "Text", JsonRequestBehavior.AllowGet));

            }
            catch (Exception ext)
            {
                return Json(new SelectList(licity, "Value", "Text", JsonRequestBehavior.AllowGet));

            }
        }

        // GET: EmployeePIs


        public ActionResult Index()
        {
            var user = (UserMaster)Session["User"];
            var employeePIs = db.EmployeePIs.Include(e => e.EmployeeGI).Where(e => e.CustomerId == user.CustomerId);
            var NoofEmp = employeePIs.Count();
            return View(db.EmployeePIs.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeePI employeePI = db.EmployeePIs.Find(id);
            if (employeePI == null)
            {
                return HttpNotFound();
            }
            return View(employeePI);
        }



        // GET: EmployeePIs/Details/5


        // GET: EmployeePIs/Create
        public ActionResult Create()
        {
            List<CityMaster> CityList = new List<CityMaster>();
            CityList = (from CityName in db.CityMasters select CityName).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.LGAextOfKins = new SelectList(CityList, "City", "City");

            List<StateMaster> StateList = new List<StateMaster>();
            StateList = (from State in db.StateMasters select State).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.StateNextOfKins = new SelectList(StateList, "State", "State");

            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode");
            return View();
        }

        // POST: EmployeePIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,EmployeePIId,EmployeeCode,EmpEmailId,PermanentAddress,MobileNo,EmailIdKin,KinName,AddressNextOfKin,StateNextOfKin,LGAextOfKin,Relation,PhoneNoNextOfKin,NameOfStaffBenificiary,PhoneOfStaffBenificiary,AddressOfStaffBenificiary,EmployeeStatus,CreatedDate,IsDeleted")] EmployeePI employeePI)
        {
            var user = (UserMaster)Session["User"];
            List<CityMaster> CityList = new List<CityMaster>();
            CityList = (from CityName in db.CityMasters select CityName).ToList();
            ViewBag.LGAextOfKins = new SelectList(CityList, "City", "City");
            List<StateMaster> StateList = new List<StateMaster>();
            StateList = (from State in db.StateMasters select State).ToList();
            ViewBag.StateNextOfKins = new SelectList(StateList, "State", "State");
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode");
            var isAlready = (from a in db.EmployeePIs where a.EmployeeCode == employeePI.EmployeeCode select a.EmployeeCode).Count();
            if (isAlready == 0)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        int empidcount = (from a in db.EmployeePIs where a.EmployeeCode == employeePI.EmployeeCode select a).Count();
                        if (empidcount == 0)
                        {
                            int DuplicateEmailMobile = (from a in db.EmployeePIs where a.EmpEmailId == employeePI.EmpEmailId && a.MobileNo == employeePI.MobileNo select a).ToList().Count();
                            if (DuplicateEmailMobile == 0)
                            {
                                employeePI.CreatedDate = DateTime.Now.Date;
                                db.EmployeePIs.Add(employeePI);
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
                            else
                            {
                                TempData["msg"] = "Email Id or Mobile No. is already exist";
                                return View(employeePI);
                            }
                        }


                    }


                }
                catch (Exception ext)
                {
                    TempData["successmsg"] = "";
                    TempData["msg"] = "Record is not Added,Please try again";

                }

                ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeePI.EmployeeCode);
                return View(employeePI);
            }
            else
            {
                TempData["successmsg"] = "";
                TempData["msg"] = "This Record is Already Exist";
                return View(employeePI);
            }
        }

        // GET: EmployeePIs/Edit/5
        public ActionResult Edit(int? id)
        {
            List<CityMaster> CityList = new List<CityMaster>();
            CityList = (from CityName in db.CityMasters select CityName).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.LGAextOfKins = new SelectList(CityList, "City", "City");

            List<StateMaster> StateList = new List<StateMaster>();
            StateList = (from State in db.StateMasters select State).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.StateNextOfKins = new SelectList(StateList, "State", "State");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeePI employeePI = db.EmployeePIs.Find(id);
            if (employeePI == null)
            {
                return HttpNotFound();
            }

            return View(employeePI);
        }

        // POST: EmployeePIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,EmployeePIId,EmployeeCode,EmpEmailId,PermanentAddress,MobileNo,EmailIdKin,KinName,AddressNextOfKin,StateNextOfKin,LGAextOfKin,Relation,PhoneNoNextOfKin,NameOfStaffBenificiary,PhoneOfStaffBenificiary,AddressOfStaffBenificiary,EmployeeStatus,CreatedDate,IsDeleted")] EmployeePI employeePI)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeePI.EmployeePIId = (from a in db.EmployeePIs where a.EmployeeCode == employeePI.EmployeeCode select a.EmployeePIId).FirstOrDefault();
                    employeePI.CreatedDate = DateTime.Now;
                    employeePI.IsDeleted = false;
                    db.Entry(employeePI).State = EntityState.Modified;
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
                TempData["msg"] = "Record is not Updated,Try again";
                TempData["successmsg"] = "";
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeePI.EmployeeCode);
            return View(employeePI);
        }


        #region MidLevelAdmin
        public ActionResult MidLevelIndex()
        {
            var NoofEmp = (from a in db.EmployeePIs select a).ToList().Count();
            ViewBag.NoOfStaff = NoofEmp;
            var employeePIs = db.EmployeePIs.Include(e => e.EmployeeGI);

            return View(db.EmployeePIs.ToList());
        }
        // GET: EmployeePIs/Details/5


        // GET: EmployeePIs/Create
        public ActionResult MidLevelCreate()
        {
            List<CityMaster> CityList = new List<CityMaster>();
            CityList = (from CityName in db.CityMasters select CityName).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.LGAextOfKin = new SelectList(CityList, "Id", "City");

            List<StateMaster> StateList = new List<StateMaster>();
            StateList = (from State in db.StateMasters select State).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.StateNextOfKin = new SelectList(StateList, "Id", "State");

            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode");
            return View();
        }

        // POST: EmployeePIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MidLevelCreate([Bind(Include = "CustomerId,EmployeePIId,EmployeeCode,EmpEmailId,PermanentAddress,MobileNo,EmailIdKin,KinName,AddressNextOfKin,StateNextOfKin,LGAextOfKin,Relation,PhoneNoNextOfKin,NameOfStaffBenificiary,PhoneOfStaffBenificiary,AddressOfStaffBenificiary,EmployeeStatus,CreatedDate,IsDeleted")] EmployeePI employeePI)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int empidcount = (from a in db.EmployeePIs where a.EmployeeCode == employeePI.EmployeeCode select a).Count();
                    if (empidcount == 0)
                    {
                        int DuplicateEmailMobile = (from a in db.EmployeePIs where a.EmpEmailId == employeePI.EmpEmailId && a.MobileNo == employeePI.MobileNo select a).ToList().Count();
                        if (DuplicateEmailMobile == 0)
                        {

                            employeePI.CreatedDate = DateTime.Now.Date;
                            db.EmployeePIs.Add(employeePI);
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
                        else
                        {
                            TempData["msg"] = "Email Id or Mobile No. is already exist";
                            return View(employeePI);
                        }
                    }


                }


            }
            catch (Exception ext)
            {
                TempData["successmsg"] = "";
                TempData["msg"] = "Record is not Added,Please try again";

            }

            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeePI.EmployeeCode);
            return View(employeePI);
        }

        // GET: EmployeePIs/Edit/5
        public ActionResult MidLevelEdit(int? id)
        {
            List<CityMaster> CityList = new List<CityMaster>();
            CityList = (from CityName in db.CityMasters select CityName).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.LGAextOfKin = new SelectList(CityList, "Id", "City");

            List<StateMaster> StateList = new List<StateMaster>();
            StateList = (from State in db.StateMasters select State).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.StateNextOfKin = new SelectList(StateList, "Id", "State");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeePI employeePI = db.EmployeePIs.Find(id);
            if (employeePI == null)
            {
                return HttpNotFound();
            }

            return View(employeePI);
        }

        // POST: EmployeePIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MidLevelEdit([Bind(Include = "CustomerId,EmployeePIId,EmployeeCode,EmpEmailId,PermanentAddress,MobileNo,EmailIdKin,KinName,AddressNextOfKin,StateNextOfKin,LGAextOfKin,Relation,PhoneNoNextOfKin,NameOfStaffBenificiary,PhoneOfStaffBenificiary,AddressOfStaffBenificiary,EmployeeStatus,CreatedDate,IsDeleted")] EmployeePI employeePI)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeePI.EmployeePIId = (from a in db.EmployeePIs where a.EmployeeCode == employeePI.EmployeeCode select a.EmployeePIId).FirstOrDefault();
                    employeePI.CreatedDate = DateTime.Now;
                    employeePI.IsDeleted = false;
                    db.Entry(employeePI).State = EntityState.Modified;
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
                TempData["msg"] = "Record is not Updated,Try again";
                TempData["successmsg"] = "";
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeePI.EmployeeCode);
            return View(employeePI);
        }

        #endregion

        // GET: EmployeePIs/Delete/5


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeePI employeePI = db.EmployeePIs.Find(id);
            if (employeePI == null)
            {
                return HttpNotFound();
            }
            return View(employeePI);
        }

        // POST: EmployeePIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                EmployeePI employeePI = db.EmployeePIs.Find(id);
                db.EmployeePIs.Remove(employeePI);
                db.SaveChanges();
                TempData["successmsg"] = "Record is Successfully Deleted";
                TempData["msg"] = "";
                return RedirectToAction("Index");
            }
            catch (Exception ext)
            {
                TempData["successmsg"] = "";
                TempData["msg"] = "Record is not Deleted,Try again";
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
