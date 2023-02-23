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
            var tblAccountControl = db.tblAccountControl.Include(t => t.tblBranch).Include(t => t.tblUser).Include(t => t.tblCompany);
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
            ViewBag.BranchID = new SelectList(db.tblBranch, "BranchID", "BranchName");
            ViewBag.UserID = new SelectList(db.tblUser, "UserID", "FullName");
            ViewBag.CompanyID = new SelectList(db.tblCompany, "CompanyID", "Name");
            return View();
        }

        // POST: AccountControls/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountControlID,CompanyID,BranchID,AccountHeadID,AccountControlName,UserID")] tblAccountControl tblAccountControl)
        {
            if (ModelState.IsValid)
            {
                db.tblAccountControl.Add(tblAccountControl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchID = new SelectList(db.tblBranch, "BranchID", "BranchName", tblAccountControl.BranchID);
            ViewBag.UserID = new SelectList(db.tblUser, "UserID", "FullName", tblAccountControl.UserID);
            ViewBag.CompanyID = new SelectList(db.tblCompany, "CompanyID", "Name", tblAccountControl.CompanyID);
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
            ViewBag.BranchID = new SelectList(db.tblBranch, "BranchID", "BranchName", tblAccountControl.BranchID);
            ViewBag.UserID = new SelectList(db.tblUser, "UserID", "FullName", tblAccountControl.UserID);
            ViewBag.CompanyID = new SelectList(db.tblCompany, "CompanyID", "Name", tblAccountControl.CompanyID);
            return View(tblAccountControl);
        }

        // POST: AccountControls/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountControlID,CompanyID,BranchID,AccountHeadID,AccountControlName,UserID")] tblAccountControl tblAccountControl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAccountControl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.tblBranch, "BranchID", "BranchName", tblAccountControl.BranchID);
            ViewBag.UserID = new SelectList(db.tblUser, "UserID", "FullName", tblAccountControl.UserID);
            ViewBag.CompanyID = new SelectList(db.tblCompany, "CompanyID", "Name", tblAccountControl.CompanyID);
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
