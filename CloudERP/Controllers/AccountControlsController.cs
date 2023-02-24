using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccess;

namespace CloudERP.Controllers
{
    public class AccountControlsController : Controller
    {
        private CloudDBEntities db = new CloudDBEntities();

        // GET: AccountControls
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int companyID = 0;
            int branchID = 0;
            int userID = 0;

            companyID = Convert.ToInt32(Convert.ToString(Session["CompanyID"]));
            branchID = Convert.ToInt32(Convert.ToString(Session["BranchID"]));
            userID = Convert.ToInt32(Convert.ToString(Session["UserID"]));

            var tblAccountControl = db.tblAccountControl.Include(t => t.tblBranch).Include(t => t.tblUser).Include(t => t.tblCompany)
                                                        .Where(a => a.CompanyID == companyID && a.BranchID == branchID);
            return View(tblAccountControl.ToList());
        }

        // GET: AccountControls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAccountControl tblAccountControl = db.tblAccountControl.Find(id);
            if (tblAccountControl == null)
            {
                return HttpNotFound();
            }
            return View(tblAccountControl);
        }

        // GET: AccountControls/Create
        public ActionResult Create()
        {
            ViewBag.AccountHeadID = new SelectList(db.tblAccountHead, "AccountHeadID", "AccountHeadName");
            return View();
        }

        // POST: AccountControls/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblAccountControl tblAccountControl)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int companyID = 0;
            int branchID = 0;
            int userID = 0;

            companyID = Convert.ToInt32(Convert.ToString(Session["CompanyID"]));
            branchID = Convert.ToInt32(Convert.ToString(Session["BranchID"]));
            userID = Convert.ToInt32(Convert.ToString(Session["UserID"]));

            tblAccountControl.BranchID = branchID;
            tblAccountControl.CompanyID = companyID;
            tblAccountControl.UserID = userID;

            if (ModelState.IsValid)
            {
                var findControls = db.tblAccountControl.Where(a => a.CompanyID == companyID
                                                                && a.BranchID == branchID
                                                                && a.AccountControlName == tblAccountControl.AccountControlName).FirstOrDefault();
                if (findControls == null)
                {
                    db.tblAccountControl.Add(tblAccountControl);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Already Exist";
                }
            }

            ViewBag.AccountHeadID = new SelectList(db.tblAccountHead, "AccountHeadID", "AccountHeadName", tblAccountControl.AccountHeadID);
            return View(tblAccountControl);
        }

        // GET: AccountControls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAccountControl tblAccountControl = db.tblAccountControl.Find(id);
            if (tblAccountControl == null)
            {
                return HttpNotFound();
            }

            ViewBag.AccountHeadID = new SelectList(db.tblAccountHead, "AccountHeadID", "AccountHeadName", tblAccountControl.AccountHeadID);
            return View(tblAccountControl);
        }

        // POST: AccountControls/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblAccountControl tblAccountControl)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userID = 0;
            userID = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            tblAccountControl.UserID = userID;

            if (ModelState.IsValid)
            {
                var findControls = db.tblAccountControl.Where(a => a.CompanyID == tblAccountControl.CompanyID
                                                                && a.BranchID == tblAccountControl.BranchID
                                                                && a.AccountControlName == tblAccountControl.AccountControlName
                                                                && a.AccountControlID != tblAccountControl.AccountControlID).FirstOrDefault();
                if (findControls == null)
                {
                    db.Entry(tblAccountControl).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Already Exist";
                }
            }

            ViewBag.AccountHeadID = new SelectList(db.tblAccountHead, "AccountHeadID", "AccountHeadName", tblAccountControl.AccountHeadID);
            return View(tblAccountControl);
        }

        // GET: AccountControls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAccountControl tblAccountControl = db.tblAccountControl.Find(id);
            if (tblAccountControl == null)
            {
                return HttpNotFound();
            }
            return View(tblAccountControl);
        }

        // POST: AccountControls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblAccountControl tblAccountControl = db.tblAccountControl.Find(id);
            db.tblAccountControl.Remove(tblAccountControl);
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
