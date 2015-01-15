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
    public class Scorecard_Target_UploadController : Controller
    {
        private HovisDWEntities db = new HovisDWEntities();

        // GET: Scorecard_Target_Upload
        public ActionResult Index()
        {
            return View(db.t_Scorecard_Target_Details_Upload.ToList());
        }

        // GET: Scorecard_Target_Upload/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Target_Details_Upload t_Scorecard_Target_Details_Upload = db.t_Scorecard_Target_Details_Upload.Find(id);
            if (t_Scorecard_Target_Details_Upload == null)
            {
                return HttpNotFound();
            }
            return View(t_Scorecard_Target_Details_Upload);
        }

        // GET: Scorecard_Target_Upload/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Scorecard_Target_Upload/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Recid,Year,Period,SiteCode,MinorCatID,Target,Budget,Target2,Budget2,ChangedDate")] t_Scorecard_Target_Details_Upload t_Scorecard_Target_Details_Upload)
        {
            if (ModelState.IsValid)
            {
                db.t_Scorecard_Target_Details_Upload.Add(t_Scorecard_Target_Details_Upload);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_Scorecard_Target_Details_Upload);
        }

        // GET: Scorecard_Target_Upload/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Target_Details_Upload t_Scorecard_Target_Details_Upload = db.t_Scorecard_Target_Details_Upload.Find(id);
            if (t_Scorecard_Target_Details_Upload == null)
            {
                return HttpNotFound();
            }
            return View(t_Scorecard_Target_Details_Upload);
        }

        // POST: Scorecard_Target_Upload/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Recid,Year,Period,SiteCode,MinorCatID,Target,Budget,Target2,Budget2,ChangedDate")] t_Scorecard_Target_Details_Upload t_Scorecard_Target_Details_Upload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Scorecard_Target_Details_Upload).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_Scorecard_Target_Details_Upload);
        }

        // GET: Scorecard_Target_Upload/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Target_Details_Upload t_Scorecard_Target_Details_Upload = db.t_Scorecard_Target_Details_Upload.Find(id);
            if (t_Scorecard_Target_Details_Upload == null)
            {
                return HttpNotFound();
            }
            return View(t_Scorecard_Target_Details_Upload);
        }

        // POST: Scorecard_Target_Upload/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            t_Scorecard_Target_Details_Upload t_Scorecard_Target_Details_Upload = db.t_Scorecard_Target_Details_Upload.Find(id);
            db.t_Scorecard_Target_Details_Upload.Remove(t_Scorecard_Target_Details_Upload);
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
