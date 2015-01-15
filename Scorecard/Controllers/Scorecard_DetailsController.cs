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
    public class Scorecard_DetailsController : Controller
    {
        private HovisDWEntities db = new HovisDWEntities();

        // GET: t_Scorecard_Details
        [Authorize(Roles = "canEdit")]
        public ActionResult Index()
        {
            string UserIdentity = User.Identity.Name;
            string DefaultSite = "No Default Site";

            // Set the user name first
            if (UserIdentity == "")
            {
                UserIdentity = "Not Logged in";
            }

            // Get the default site for the user
            IQueryable<t_Scorecard_Master_UserSites> userSites = db.t_Scorecard_Master_UserSites
                .Where(c => c.UserName == UserIdentity);
            userSites = userSites.Take(1);
            foreach (var DefaultSites in userSites)
            {
                DefaultSite = Convert.ToString(DefaultSites.UserSiteCode);
            }

            string SiteIDchar = DefaultSite;

            var t_Scorecard_Details = db.t_Scorecard_Details.Include(t => t.t_Scorecard_Master_Categories).Include(t => t.t_Scorecard_Minor_Categories);
            return View(t_Scorecard_Details.ToList().Where(t => t.SiteCode == SiteIDchar).OrderByDescending(t => t.Year).ThenByDescending(t => t.Week));
        }

        // GET: t_Scorecard_Details/Details/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Details t_Scorecard_Details = db.t_Scorecard_Details.Find(id);
            if (t_Scorecard_Details == null)
            {
                return HttpNotFound();
            }
            return View(t_Scorecard_Details);
        }

        // GET: t_Scorecard_Details/Create
        [Authorize(Roles = "canEdit")]
        public ActionResult Create()
        {
            ViewBag.MasterCatID = new SelectList(db.t_Scorecard_Master_Categories, "ID", "MasterCatCode");
            ViewBag.MinorCatID = new SelectList(db.t_Scorecard_Minor_Categories, "ID", "MinorCatCode");
            return View();
        }

        // POST: t_Scorecard_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult Create([Bind(Include = "Recid,Year,Week,SiteCode,MasterCatID,MinorCatID,Actual,Target,Comments,ChangedBy,ChangedDate,Confirmed")] t_Scorecard_Details t_Scorecard_Details)
        {
            if (ModelState.IsValid)
            {
                db.t_Scorecard_Details.Add(t_Scorecard_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MasterCatID = new SelectList(db.t_Scorecard_Master_Categories, "ID", "MasterCatCode", t_Scorecard_Details.MasterCatID);
            ViewBag.MinorCatID = new SelectList(db.t_Scorecard_Minor_Categories, "ID", "MinorCatCode", t_Scorecard_Details.MinorCatID);
            return View(t_Scorecard_Details);
        }

        // GET: t_Scorecard_Details/Edit/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Details t_Scorecard_Details = db.t_Scorecard_Details.Find(id);
            if (t_Scorecard_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterCatID = new SelectList(db.t_Scorecard_Master_Categories, "ID", "MasterCatCode", t_Scorecard_Details.MasterCatID);
            ViewBag.MinorCatID = new SelectList(db.t_Scorecard_Minor_Categories, "ID", "MinorCatCode", t_Scorecard_Details.MinorCatID);
            return View(t_Scorecard_Details);
        }

        // POST: t_Scorecard_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit([Bind(Include = "Recid,Year,Week,SiteCode,MasterCatID,MinorCatID,Actual,Target,Comments,ChangedBy,ChangedDate,Confirmed")] t_Scorecard_Details t_Scorecard_Details)
        {
            if (ModelState.IsValid)
            {
                t_Scorecard_Details.ChangedBy = User.Identity.Name;
                t_Scorecard_Details.ChangedDate = DateTime.Now;
                db.Entry(t_Scorecard_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MasterCatID = new SelectList(db.t_Scorecard_Master_Categories, "ID", "MasterCatCode", t_Scorecard_Details.MasterCatID);
            ViewBag.MinorCatID = new SelectList(db.t_Scorecard_Minor_Categories, "ID", "MinorCatCode", t_Scorecard_Details.MinorCatID);
            return View(t_Scorecard_Details);
        }

        // GET: t_Scorecard_Details/Delete/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Details t_Scorecard_Details = db.t_Scorecard_Details.Find(id);
            if (t_Scorecard_Details == null)
            {
                return HttpNotFound();
            }
            return View(t_Scorecard_Details);
        }

        // POST: t_Scorecard_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult DeleteConfirmed(long id)
        {
            t_Scorecard_Details t_Scorecard_Details = db.t_Scorecard_Details.Find(id);
            db.t_Scorecard_Details.Remove(t_Scorecard_Details);
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
