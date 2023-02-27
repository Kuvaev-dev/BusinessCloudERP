using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudERP.Controllers
{
    public class FinancialYearsViewController : Controller
    {
        private CloudDBEntities db = new CloudDBEntities();

        // GET: FinancialYears
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userID = 0;
            userID = Convert.ToInt32(Convert.ToString(Session["UserID"]));

            var tblFinancialYear = db.tblFinancialYear.ToList();
            return View(tblFinancialYear);
        }
    }
}