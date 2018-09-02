using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Controllers
{
    public class IARTAccountController : Controller
    {
        // GET: IARTAccount
        public ActionResult PaymentVoucher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PaymentVoucher(FormCollection fc)
        {
            return View();
        }

        public ActionResult EXPENDITURE()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EXPENDITURE(FormCollection fc)
        {
            return View();
        }
        public ActionResult PurchaseDayBook()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PurchaseDayBook(FormCollection fc)
        {
            return View();
        }

        public ActionResult RevenueDayBook()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RevenueDayBook(FormCollection fc)
        {
            return View();
        }
        public ActionResult CapitalCashbookReceipt()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CapitalCashbookReceipt(FormCollection fc)
        {
            return View();
        }
        public ActionResult CapitalCashbookPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CapitalCashbookPayment(FormCollection fc)
        {
            return View();
        }

        
    }
}