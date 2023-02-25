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
    public class AccountHeadsController : Controller
    {
        private CloudDBEntities db = new CloudDBEntities();

        // GET: AccountHeads
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

            var tblAccountHead = db.tblAccountHead.Include(t => t.tblUser)
                                                  .Where(a => a.CompanyID == companyID && a.BranchID == branchID);
            return View(tblAccountHead.ToList());
        }

        // GET: AccountHeads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAccountHead tblAccountHead = db.tblAccountHead.Find(id);
            if (tblAccountHead == null)
            {
                return HttpNotFound();
            }
            return View(tblAccountHead);
        }

        // GET: AccountHeads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountHeads/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblAccountHead tblAccountHead)
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

            tblAccountHead.BranchID = branchID;
            tblAccountHead.CompanyID = companyID;
            tblAccountHead.UserID = userID;

            if (ModelState.IsValid)
            {
                var findHead = db.tblAccountHead.Where(a => a.CompanyID == companyID
                                                         && a.BranchID == branchID
                                                         && a.AccountHeadName == tblAccountHead.AccountHeadName).FirstOrDefault();
                if (findHead == null)
                {
                    db.tblAccountHead.Add(tblAccountHead);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Already Exist";
                }
            }

            return View(tblAccountHead);
        }

        // GET: AccountHeads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAccountHead tblAccountHead = db.tblAccountHead.Find(id);
            if (tblAccountHead == null)
            {
                return HttpNotFound();
            }

            return View(tblAccountHead);
        }

        // POST: AccountHeads/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblAccountHead tblAccountHead)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userID = 0;
            userID = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            tblAccountHead.UserID = userID;

            if (ModelState.IsValid)
            {
                var findHead = db.tblAccountHead.Where(a => a.CompanyID == tblAccountHead.CompanyID
                                                         && a.BranchID == tblAccountHead.BranchID
                                                         && a.AccountHeadName == tblAccountHead.AccountHeadName
                                                         && a.AccountHeadID != tblAccountHead.AccountHeadID).FirstOrDefault();
                if (findHead == null)
                {
                    db.Entry(tblAccountHead).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Already Exist";
                }
            }

            return View(tblAccountHead);
        }

        // GET: AccountHeads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAccountHead tblAccountHead = db.tblAccountHead.Find(id);
            if (tblAccountHead == null)
            {
                return HttpNotFound();
            }
            return View(tblAccountHead);
        }

        // POST: AccountHeads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblAccountHead tblAccountHead = db.tblAccountHead.Find(id);
            db.tblAccountHead.Remove(tblAccountHead);
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