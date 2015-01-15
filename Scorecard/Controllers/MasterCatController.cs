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
    public class MasterCatController : Controller
    {
        private HovisDWEntities db = new HovisDWEntities();

        // GET: /MasterCat/
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Index()
        {
            return View(db.t_Scorecard_Master_Categories.ToList());
        }

        // GET: /MasterCat/Details/5
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Master_Categories t_scorecard_master_categories = db.t_Scorecard_Master_Categories.Find(id);
            if (t_scorecard_master_categories == null)
            {
                return HttpNotFound();
            }
            return View(t_scorecard_master_categories);
        }

        // GET: /MasterCat/Create
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /MasterCat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Create([Bind(Include = "ID,MasterCatCode")] t_Scorecard_Master_Categories t_scorecard_master_categories)
        {
            if (ModelState.IsValid)
            {
                db.t_Scorecard_Master_Categories.Add(t_scorecard_master_categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_scorecard_master_categories);
        }

        // GET: /MasterCat/Edit/5
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Master_Categories t_scorecard_master_categories = db.t_Scorecard_Master_Categories.Find(id);
            if (t_scorecard_master_categories == null)
            {
                return HttpNotFound();
            }
            return View(t_scorecard_master_categories);
        }

        // POST: /MasterCat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Edit([Bind(Include = "ID,MasterCatCode")] t_Scorecard_Master_Categories t_scorecard_master_categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_scorecard_master_categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_scorecard_master_categories);
        }

        // GET: /MasterCat/Delete/5
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Master_Categories t_scorecard_master_categories = db.t_Scorecard_Master_Categories.Find(id);
            if (t_scorecard_master_categories == null)
            {
                return HttpNotFound();
            }
            return View(t_scorecard_master_categories);
        }

        // POST: /MasterCat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult DeleteConfirmed(int id)
        {
            t_Scorecard_Master_Categories t_scorecard_master_categories = db.t_Scorecard_Master_Categories.Find(id);
            db.t_Scorecard_Master_Categories.Remove(t_scorecard_master_categories);
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
