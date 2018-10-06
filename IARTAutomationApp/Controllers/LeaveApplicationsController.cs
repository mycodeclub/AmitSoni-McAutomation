using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IARTAutomationApp.Models;
using System.Net.Mail;
using IARTAutomationApp.ViewModels;
using System.IO;

namespace IARTAutomationApp.Controllers
{

    public class LeaveApplicationsController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: LeaveApplications

        public ActionResult UserIndex()
        {
            int empcode = Convert.ToInt32(@Session["employeecode"]);
            var employeeLIs = db.LeaveApplications.Where(a => a.EmployeeCode == empcode).ToList().OrderByDescending(k => k.AppDate);
            return View(employeeLIs.ToList());
        }


        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            DateTime dt = DateTime.Now.Date;
            var EmpOnLeave = (from a in db.LeaveApplications where user.CustomerId == a.CustomerId && a.AppDate == dt.Date && a.Status == "Approved" orderby a.LeaveAccId descending select a).Count();
            ViewBag.EmpOnLeave = EmpOnLeave;
            return View(db.LeaveApplications.ToList());
        }

        // GET: LeaveApplications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveApplication leaveApplication = db.LeaveApplications.Find(id);
            if (leaveApplication == null)
            {
                return HttpNotFound();
            }
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Create
        public ActionResult Create()
        {
            ///////
            LeaveApplication empleave = new LeaveApplication();

            empleave.LeaveFromDate = DateTime.Now.Date;
            empleave.NoOfDays = 1;
            empleave.LeaveToDate = DateTime.Now.Date;
            empleave.AppDate = DateTime.Now.Date;
            List<LeaveTypeMaster> LeaveTypeklist = new List<LeaveTypeMaster>();
            LeaveTypeklist = (from a in db.LeaveTypeMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.LeaveTypeName = new SelectList(LeaveTypeklist, "LeaveTypeId", "LeaveTypeName");

            ///////
            return View(empleave);
        }
        public ActionResult UserCreate()
        {
            ///////
            int empcode = Convert.ToInt32(@Session["employeecode"]);
            LeaveApplication empleave = new LeaveApplication();

            empleave.LeaveFromDate = DateTime.Now.Date;
            empleave.NoOfDays = 1;
            empleave.LeaveToDate = DateTime.Now.Date;
            empleave.AppDate = DateTime.Now.Date;
            List<LeaveTypeMaster> LeaveTypeklist = new List<LeaveTypeMaster>();
            LeaveTypeklist = (from a in db.LeaveTypeMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.LeaveTypeName = new SelectList(LeaveTypeklist, "LeaveTypeId", "LeaveTypeName");
            empleave.EmployeeCode = empcode;
            ///////
            return View(empleave);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UserCreate([Bind(Include = "CustomerId,LeaveAccId,EmployeeCode,LeaveTypeName,LeaveFromDate,LeaveToDate,NoOfDays,AppDate,IsApproved,IsDeleted,CreatedDate")] LeaveApplication leaveApplication)
        {
            List<LeaveTypeMaster> LeaveTypeklist = new List<LeaveTypeMaster>();
            LeaveTypeklist = (from a in db.LeaveTypeMasters select a).ToList();

            //ViewBag.CountryList = CountryList;
            ViewBag.LeaveTypeName = new SelectList(LeaveTypeklist, "LeaveTypeId", "LeaveTypeName");
            if (ModelState.IsValid)
            {
                int lt = Convert.ToInt32(leaveApplication.LeaveTypeName);
                var leavetype = (from a in db.LeaveTypeMasters where a.LeaveTypeId == lt select a.LeaveTypeName).FirstOrDefault();
                leaveApplication.LeaveTypeName = leavetype;
                var inprocess = (from a in db.LeaveApplications where a.Status == "Pending" && a.EmployeeCode == leaveApplication.EmployeeCode select a).Count();
                if (inprocess == 0)
                {
                    leaveApplication.Status = "Pending";
                    int count = (from a in db.LeaveLedgers where a.EmployeeCode == leaveApplication.EmployeeCode && a.LeaveType == leaveApplication.LeaveTypeName select a.BalanceLeaves).Count();
                    var balanceLeave = (from a in db.LeaveLedgers where a.EmployeeCode == leaveApplication.EmployeeCode && a.LeaveType == leaveApplication.LeaveTypeName && a.BalanceLeaves >= leaveApplication.NoOfDays select a.BalanceLeaves).FirstOrDefault();
                    if ((balanceLeave > 0) || (count == 0))
                    {

                        db.LeaveApplications.Add(leaveApplication);
                        db.SaveChanges();
                        TempData["msg"] = "<script>alert('Your Leave Application is Submitted');</script>";
                        return RedirectToAction("UserIndex");
                    }
                    else
                    {

                        TempData["msg"] = "<script>alert('No Leaves in Balance');</script>";
                        return View(leaveApplication);
                    }

                }
                else
                {
                    TempData["msg"] = "<script>alert('Your Application is already in process');</script>";
                }
            }

            return View(leaveApplication);
        }


        // POST: LeaveApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,LeaveAccId,EmployeeCode,LeaveTypeName,LeaveFromDate,LeaveToDate,NoOfDays,AppDate,IsApproved,IsDeleted,CreatedDate")] LeaveApplication leaveApplication)
        {
            List<LeaveTypeMaster> LeaveTypeklist = new List<LeaveTypeMaster>();
            LeaveTypeklist = (from a in db.LeaveTypeMasters select a).ToList();

            //ViewBag.CountryList = CountryList;
            ViewBag.LeaveTypeName = new SelectList(LeaveTypeklist, "LeaveTypeId", "LeaveTypeName");
            if (ModelState.IsValid)
            {
                int lt = Convert.ToInt32(leaveApplication.LeaveTypeName);
                var leavetype = (from a in db.LeaveTypeMasters where a.LeaveTypeId == lt select a.LeaveTypeName).FirstOrDefault();
                leaveApplication.LeaveTypeName = leavetype;
                var inprocess = (from a in db.LeaveApplications where a.Status == "Pending" && a.EmployeeCode == leaveApplication.EmployeeCode select a).Count();
                if (inprocess == 0)
                {
                    leaveApplication.Status = "Pending";
                    int count = (from a in db.LeaveLedgers where a.EmployeeCode == leaveApplication.EmployeeCode && a.LeaveType == leaveApplication.LeaveTypeName select a.BalanceLeaves).Count();
                    var balanceLeave = (from a in db.LeaveLedgers where a.EmployeeCode == leaveApplication.EmployeeCode && a.LeaveType == leaveApplication.LeaveTypeName && a.BalanceLeaves >= leaveApplication.NoOfDays select a.BalanceLeaves).FirstOrDefault();
                    if ((balanceLeave > 0) || (count == 0))
                    {

                        db.LeaveApplications.Add(leaveApplication);
                        db.SaveChanges();
                        TempData["msg"] = "<script>alert('Your Leave Application is Submitted');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        TempData["msg"] = "<script>alert('No Leaves in Balance');</script>";
                        return View(leaveApplication);
                    }

                }
                else
                {
                    TempData["msg"] = "<script>alert('Your Application is already in process');</script>";
                }
            }

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Edit/5
        public ActionResult Edit(int? id)
        {
            List<LeaveTypeMaster> LeaveTypeklist = new List<LeaveTypeMaster>();
            LeaveTypeklist = (from a in db.LeaveTypeMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.LeaveTypeName = new SelectList(LeaveTypeklist, "LeaveTypeId", "LeaveTypeName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveApplication leaveApplication = db.LeaveApplications.Find(id);
            if (leaveApplication == null)
            {
                return HttpNotFound();
            }
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,LeaveAccId,EmployeeCode,LeaveTypeName,LeaveFromDate,LeaveToDate,NoOfDays,AppDate,IsApproved,IsDeleted,CreatedDate")] LeaveApplication leaveApplication)
        {
            try
            {
                decimal balanceLeaveM = 0;
                int count = (from a in db.LeaveLedgers where a.EmployeeCode == leaveApplication.EmployeeCode && a.LeaveType == leaveApplication.LeaveTypeName select a.BalanceLeaves).Count();
                var balanceLeave = (from a in db.LeaveLedgers where a.EmployeeCode == leaveApplication.EmployeeCode && a.LeaveType == leaveApplication.LeaveTypeName && a.BalanceLeaves >= leaveApplication.NoOfDays select a.BalanceLeaves).FirstOrDefault();


                var email = (from a in db.EmployeePIs where a.EmployeeCode == leaveApplication.EmployeeCode select a.EmpEmailId).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    int lt = Convert.ToInt32(leaveApplication.LeaveTypeName);
                    var leavetype = (from a in db.LeaveTypeMasters where a.LeaveTypeId == lt select a.LeaveTypeName).FirstOrDefault();
                    leaveApplication.LeaveTypeName = leavetype;

                    if (count == 0)
                    {
                        balanceLeaveM = (from a in db.LeaveMasters where a.LeaveTypeId == lt select a.LeaveCount).FirstOrDefault();
                        balanceLeave = balanceLeaveM;
                    }
                    if (leaveApplication.IsApproved == true)
                        leaveApplication.Status = "Approved";
                    else
                        leaveApplication.Status = "Not Approved";
                    db.Entry(leaveApplication).State = EntityState.Modified;
                    db.SaveChanges();
                    /////////////////
                    LeaveLedger leaveledger = new Models.LeaveLedger();
                    leaveledger.EmployeeCode = leaveApplication.EmployeeCode;
                    leaveledger.ConsumedLeaves = leaveApplication.NoOfDays;
                    leaveledger.BalanceLeaves = balanceLeave - leaveApplication.NoOfDays;
                    leaveledger.LeaveType = leaveApplication.LeaveTypeName;
                    DateTime appdt = Convert.ToDateTime(leaveApplication.AppDate);
                    leaveledger.FiscalYear = appdt.Year.ToString() + "-" + (appdt.Year + 1).ToString().Substring(2, 2);
                    db.LeaveLedgers.Add(leaveledger);
                    db.SaveChanges();

                    EmailModel model = new EmailModel();
                    model.Email = "toshuklagovind.2020@gmail.com";
                    model.Password = "jamesbond@123";
                    model.Subject = "Leave Application Status";
                    model.Body = "Your Leave Application is " + leaveApplication.Status;

                    model.To = email;

                    using (MailMessage mm = new MailMessage(model.Email, model.To))
                    {
                        mm.Subject = model.Subject;
                        mm.Body = model.Body;

                        mm.IsBodyHtml = false;
                        using (SmtpClient smtp = new SmtpClient())
                        {
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;

                            NetworkCredential NetworkCred = new NetworkCredential(model.Email, model.Password);
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            ViewBag.Message = "Email sent.";
                        }
                    }
                    TempData["msg"] = "<script>alert('Mail is Successfully Send');</script>";
                    return RedirectToAction("Index");
                }
                return View(leaveApplication);
            }
            catch (Exception ext)
            {
                TempData["msg"] = "<script>alert('Mail is failed due to Server error');</script>";
                return RedirectToAction("Index");
            }
        }



        // GET: LeaveApplications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveApplication leaveApplication = db.LeaveApplications.Find(id);
            if (leaveApplication == null)
            {
                return HttpNotFound();
            }
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveApplication leaveApplication = db.LeaveApplications.Find(id);
            db.LeaveApplications.Remove(leaveApplication);
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
