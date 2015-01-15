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
    public class MinorCatController : Controller
    {
        private HovisDWEntities db = new HovisDWEntities();

        // GET: /MinorCat/
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Index()
        {
            return View(db.t_Scorecard_Minor_Categories.ToList());
        }

        // GET: /MinorCat/Details/5
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Minor_Categories t_scorecard_minor_categories = db.t_Scorecard_Minor_Categories.Find(id);
            if (t_scorecard_minor_categories == null)
            {
                return HttpNotFound();
            }
            return View(t_scorecard_minor_categories);
        }

        // GET: /MinorCat/Create
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /MinorCat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Create([Bind(Include = "ID,MinorCatCode")] t_Scorecard_Minor_Categories t_scorecard_minor_categories)
        {
            if (ModelState.IsValid)
            {
                db.t_Scorecard_Minor_Categories.Add(t_scorecard_minor_categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_scorecard_minor_categories);
        }

        // GET: /MinorCat/Edit/5
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Minor_Categories t_scorecard_minor_categories = db.t_Scorecard_Minor_Categories.Find(id);
            if (t_scorecard_minor_categories == null)
            {
                return HttpNotFound();
            }
            return View(t_scorecard_minor_categories);
        }

        // POST: /MinorCat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Edit([Bind(Include = "ID,MinorCatCode")] t_Scorecard_Minor_Categories t_scorecard_minor_categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_scorecard_minor_categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_scorecard_minor_categories);
        }

        // GET: /MinorCat/Delete/5
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Minor_Categories t_scorecard_minor_categories = db.t_Scorecard_Minor_Categories.Find(id);
            if (t_scorecard_minor_categories == null)
            {
                return HttpNotFound();
            }
            return View(t_scorecard_minor_categories);
        }

        // POST: /MinorCat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult DeleteConfirmed(int id)
        {
            t_Scorecard_Minor_Categories t_scorecard_minor_categories = db.t_Scorecard_Minor_Categories.Find(id);
            db.t_Scorecard_Minor_Categories.Remove(t_scorecard_minor_categories);
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
