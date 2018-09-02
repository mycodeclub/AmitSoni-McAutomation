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
    public class EmployeeMIsController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();


        public ActionResult UserIndex()
        {

            int empcode = Convert.ToInt32(@Session["employeecode"]);



            var employeeMIs = db.EmployeeMIs.Where(a => a.EmployeeCode  == empcode).ToList();
                return View(employeeMIs.ToList());

                //if(     User.Identity.Name)


            
            return View();
        }

        [HttpPost]
        public JsonResult AutoEmployeeCode(string Prefix)
        {
            //Note : you can bind same list from database  
            ViewBag.emp = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode").ToList();
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
        //public ActionResult FullDetails(int? id)
        //{
        //    var empDetails = from a in db.EmployeeGIs
        //                     where a.EmployeeCode == id
        //                     from b in db.EmployeeAIs
        //                     where b.EmployeeCode == id
        //                     from c in db.EmployeePIs
        //                     where c.EmployeeCode == id
        //                     from d in db.EmployeeMIs
        //                     where d.EmployeeCode == id
        //                     from e in db.EmployeeSIs
        //                     where e.EmployeeCode == id
        //                     from f in db.EmpAIAssociations
        //                     where f.EmployeeCode == id
        //                     from g in db.EmpAIConferences
        //                     where g.EmployeeCode == id
        //                     select new EmployeeDetails() { employeeGI = a, employeeAI = b, employeePI = c, employeeMI = d, employeeSI = e, empassociation = f, empconference = g };


        //    return View(empDetails);
        //}
        // GET: EmployeeMIs

        public ActionResult Index()
        {
            var NoofEmp = (from a in db.EmployeeMIs select a).ToList().Count();
            ViewBag.NoOfStaff = NoofEmp;
            var employeeMIs = db.EmployeeMIs.Include(e => e.EmployeeGI);
            return View(employeeMIs.ToList());
        }

        public ActionResult MidLevelIndex()
        {
            var NoofEmp = (from a in db.EmployeeMIs select a).ToList().Count();
            ViewBag.NoOfStaff = NoofEmp;
            var employeeMIs = db.EmployeeMIs.Include(e => e.EmployeeGI);
            return View(employeeMIs.ToList());
        }

        // GET: EmployeeMIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeMI employeeMI = db.EmployeeMIs.Find(id);
            if (employeeMI == null)
            {
                return HttpNotFound();
            }
            return View(employeeMI);
        }

        // GET: EmployeeMIs/Create

        #region MidLevel
        public ActionResult MidLevelCreate()
        {
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode");
            return View();
        }

        // POST: EmployeeMIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MidLevelCreate([Bind(Include = "EmployeeMIId,EmployeeCode,NhisNo,NhisProvider,BloodGroup,BloodGenotype,CreatedDate,IsDeleted")] EmployeeMI employeeMI)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int empidcount = (from a in db.EmployeeMIs where a.EmployeeCode == employeeMI.EmployeeCode select a).Count();
                    if (empidcount == 0)
                    {
                        employeeMI.IsDeleted = false;
                        employeeMI.CreatedDate = DateTime.Now.Date;
                        db.EmployeeMIs.Add(employeeMI);
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

                TempData["msg"] = "Record is not Added,Try again";

            }



            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeMI.EmployeeCode);
            return View(employeeMI);
        }

        // GET: EmployeeMIs/Edit/5
        public ActionResult MidLevelEdit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeMI employeeMI = db.EmployeeMIs.Find(id);
            if (employeeMI == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeMI.EmployeeCode);
            return View(employeeMI);
        }

        // POST: EmployeeMIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MIdLevelEdit([Bind(Include = "EmployeeMIId,EmployeeCode,NhisNo,NhisProvider,BloodGroup,BloodGenotype,CreatedDate,IsDeleted")] EmployeeMI employeeMI)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeeMI.EmployeeMIId = (from a in db.EmployeeMIs where a.EmployeeCode == employeeMI.EmployeeCode select a.EmployeeMIId).FirstOrDefault();
                    employeeMI.CreatedDate = DateTime.Now;
                    employeeMI.IsDeleted = false;


                    db.Entry(employeeMI).State = EntityState.Modified;
                    db.SaveChanges();


                    ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeMI.EmployeeCode);

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
                TempData["msg"] = "Record is not Updated,Try again";

            }
            return View(employeeMI);
        }

        // GET: EmployeeMIs/Delete/5
        

        #endregion
        public ActionResult Create()
        {
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode");
            return View();
        }

        // POST: EmployeeMIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeMIId,EmployeeCode,NhisNo,NhisProvider,BloodGroup,BloodGenotype,CreatedDate,IsDeleted")] EmployeeMI employeeMI)
        {
            var isAlready = (from a in db.EmployeeMIs where a.EmployeeCode == employeeMI.EmployeeCode select a.EmployeeCode).Count();
            if (isAlready == 0)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        int empidcount = (from a in db.EmployeeMIs where a.EmployeeCode == employeeMI.EmployeeCode select a).Count();
                        if (empidcount == 0)
                        {
                            employeeMI.IsDeleted = false;
                            employeeMI.CreatedDate = DateTime.Now.Date;
                            db.EmployeeMIs.Add(employeeMI);
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

                    TempData["msg"] = "Record is not Added,Try again";

                }



                ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeMI.EmployeeCode);
                return View(employeeMI);
            }
            else
            {
                TempData["successmsg"] = "";

                TempData["msg"] = "This Record is Already exist";
                return View(employeeMI);
            }
        }

        // GET: EmployeeMIs/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeMI employeeMI = db.EmployeeMIs.Find(id);
            if (employeeMI == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeMI.EmployeeCode);
            return View(employeeMI);
        }

        // POST: EmployeeMIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeMIId,EmployeeCode,NhisNo,NhisProvider,BloodGroup,BloodGenotype,CreatedDate,IsDeleted")] EmployeeMI employeeMI)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeeMI.EmployeeMIId = (from a in db.EmployeeMIs where a.EmployeeCode == employeeMI.EmployeeCode select a.EmployeeMIId).FirstOrDefault();
                    employeeMI.CreatedDate = DateTime.Now;
                    employeeMI.IsDeleted = false;


                    db.Entry(employeeMI).State = EntityState.Modified;
                    db.SaveChanges();
                   

                    ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeMI.EmployeeCode);

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
                TempData["msg"] = "Record is not Updated,Try again";

            }
                return View(employeeMI);
        }

        // GET: EmployeeMIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeMI employeeMI = db.EmployeeMIs.Find(id);
            if (employeeMI == null)
            {
                return HttpNotFound();
            }
            return View(employeeMI);
        }

        // POST: EmployeeMIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                EmployeeMI employeeMI = db.EmployeeMIs.Find(id);
                db.EmployeeMIs.Remove(employeeMI);
                db.SaveChanges();
                db.SaveChanges();
                TempData["successmsg"] = "Record is Successfully Deleted";
                TempData["msg"] = "";
            }
            catch (Exception ext)
            {
                TempData["successmsg"] = "";
                  TempData["msg"] = "Record is not Deleted,Please try again";
            }
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
