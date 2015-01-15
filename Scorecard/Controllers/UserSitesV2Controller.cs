using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Scorecard.Models;

namespace Scorecard.Controllers
{
    public class UserSitesV2Controller : Controller
    {
        private HovisDWEntities db = new HovisDWEntities();

        // GET: UserSitesV2
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Index()
        {
            var t_Scorecard_Master_UserSites = db.t_Scorecard_Master_UserSites.Include(t => t.v_PTLStaff_MasterData_Plants);
            return View(t_Scorecard_Master_UserSites.ToList());
        }

        // GET: UserSitesV2/Details/5
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Master_UserSites t_Scorecard_Master_UserSites = db.t_Scorecard_Master_UserSites.Find(id);
            if (t_Scorecard_Master_UserSites == null)
            {
                return HttpNotFound();
            }
            return View(t_Scorecard_Master_UserSites);
        }

        // GET: UserSitesV2/Create
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Create()
        {
            ViewBag.UserSiteCode = new SelectList(db.v_PTLStaff_MasterData_Plants, "Plant", "PlantDesc");
            return View();
        }

        // POST: UserSitesV2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Create([Bind(Include = "ID,UserName,UserSiteCode")] t_Scorecard_Master_UserSites t_Scorecard_Master_UserSites)
        {
            if (ModelState.IsValid)
            {
                db.t_Scorecard_Master_UserSites.Add(t_Scorecard_Master_UserSites);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserSiteCode = new SelectList(db.v_PTLStaff_MasterData_Plants, "Plant", "PlantDesc", t_Scorecard_Master_UserSites.UserSiteCode);
            return View(t_Scorecard_Master_UserSites);
        }

        // GET: UserSitesV2/Edit/5
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Master_UserSites t_Scorecard_Master_UserSites = db.t_Scorecard_Master_UserSites.Find(id);
            if (t_Scorecard_Master_UserSites == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserSiteCode = new SelectList(db.v_PTLStaff_MasterData_Plants, "Plant", "PlantDesc", t_Scorecard_Master_UserSites.UserSiteCode);
            return View(t_Scorecard_Master_UserSites);
        }

        // POST: UserSitesV2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Edit([Bind(Include = "ID,UserName,UserSiteCode")] t_Scorecard_Master_UserSites t_Scorecard_Master_UserSites)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Scorecard_Master_UserSites).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserSiteCode = new SelectList(db.v_PTLStaff_MasterData_Plants, "Plant", "PlantDesc", t_Scorecard_Master_UserSites.UserSiteCode);
            return View(t_Scorecard_Master_UserSites);
        }

        // GET: UserSitesV2/Delete/5
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Master_UserSites t_Scorecard_Master_UserSites = db.t_Scorecard_Master_UserSites.Find(id);
            if (t_Scorecard_Master_UserSites == null)
            {
                return HttpNotFound();
            }
            return View(t_Scorecard_Master_UserSites);
        }

        // POST: UserSitesV2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult DeleteConfirmed(int id)
        {
            t_Scorecard_Master_UserSites t_Scorecard_Master_UserSites = db.t_Scorecard_Master_UserSites.Find(id);
            db.t_Scorecard_Master_UserSites.Remove(t_Scorecard_Master_UserSites);
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
