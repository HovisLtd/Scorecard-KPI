using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Scorecard.Models;
using System.Data.Entity.Validation;

namespace Scorecard.Controllers
{
    public class ScorecardTemplateController : Controller
    {
        private HovisDWEntities db = new HovisDWEntities();

        // GET: /ScorecardTemplate/
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Index()
        {
            var t_scorecard_master_template = db.t_Scorecard_Master_Template.Include(t => t.t_Scorecard_Master_Categories).Include(t => t.t_Scorecard_Minor_Categories).Include(t => t.v_PTLStaff_MasterData_Plants);
            return View(t_scorecard_master_template.ToList()
                .OrderByDescending(c => c.SiteCode)
                .ThenByDescending(c => c.MinorCatID));
        }

        // GET: /ScorecardTemplate/ - this provides the measures
        [Authorize(Roles = "canEdit")]
        public ActionResult IndexSubmit()
        {
            string UserIdentity = User.Identity.Name;
            string DefaultSite = "No Default Site";
            string ToDaysDate = DateTime.Now.ToString("yyyyMMdd");
            string FinYear = "";
            string FinWeek = "";


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

            // get the financial week and year
            IQueryable<v_Hovis_Calendar> fincalendars = db.v_Hovis_Calendar
                .Where(c => c.CalendarDateChar == ToDaysDate);
            fincalendars = fincalendars.Take(1);
            foreach (var WeekYears in fincalendars)
            {
                FinWeek = Convert.ToString(WeekYears.FinancialWeek);
                FinYear = Convert.ToString(WeekYears.FinancialYear);
            }

            ViewBag.UserIdentity = UserIdentity;
            ViewBag.DefaultSite = DefaultSite;
            ViewBag.FinWeek = FinWeek;
            ViewBag.FinYear = FinYear;

            //var sites = db.v_PTLStaff_MasterData_Plants.OrderBy(q => q.PlantDesc).ToList();
            //ViewBag.SelectedSite = new SelectList(sites, "Plant", "PlantDesc", SelectedSite);
            //int SiteID = SelectedSite.GetValueOrDefault();
            //string SiteIDchar = Convert.ToString(SiteID);

            string SiteIDchar = DefaultSite;

            IQueryable<t_Scorecard_Master_Template> t_scorecard_master_templates = db.t_Scorecard_Master_Template
                .Where(c => c.SiteCode == SiteIDchar)
                .Include(t => t.t_Scorecard_Master_Categories).Include(t => t.t_Scorecard_Minor_Categories).Include(t => t.v_PTLStaff_MasterData_Plants);
            var sql = t_scorecard_master_templates.ToString();
            return View(t_scorecard_master_templates.ToList());


                //.Where(c => !SelectedSite.HasValue || Convert.ToInt32(c.SiteCode) == SiteID)

            //var t_scorecard_master_template = db.t_Scorecard_Master_Template.Include(t => t.t_Scorecard_Master_Categories).Include(t => t.t_Scorecard_Minor_Categories).Include(t => t.v_PTLStaff_MasterData_Plants);
            //return View(t_scorecard_master_template.ToList());
        }

        // GET: /ScorecardTemplate/Details/5
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Master_Template t_scorecard_master_template = db.t_Scorecard_Master_Template.Find(id);
            if (t_scorecard_master_template == null)
            {
                return HttpNotFound();
            }
            return View(t_scorecard_master_template);
        }

        // GET: /ScorecardTemplate/Create
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Create()
        {
            ViewBag.MasterCatID = new SelectList(db.t_Scorecard_Master_Categories, "ID", "MasterCatCode");
            ViewBag.MinorCatID = new SelectList(db.t_Scorecard_Minor_Categories, "ID", "MinorCatCode");
            ViewBag.SiteCode = new SelectList(db.v_PTLStaff_MasterData_Plants, "Plant", "PlantDesc");
            return View();
        }

        // POST: /ScorecardTemplate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Create([Bind(Include = "Recid,SiteCode,MasterCatID,MinorCatID,Actual,Target,Comments,LastChangedBy,LastChangedDate,Confirmed")] t_Scorecard_Master_Template t_scorecard_master_template)
        {
            if (ModelState.IsValid)
            {
                db.t_Scorecard_Master_Template.Add(t_scorecard_master_template);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MasterCatID = new SelectList(db.t_Scorecard_Master_Categories, "ID", "MasterCatCode", t_scorecard_master_template.MasterCatID);
            ViewBag.MinorCatID = new SelectList(db.t_Scorecard_Minor_Categories, "ID", "MinorCatCode", t_scorecard_master_template.MinorCatID);
            ViewBag.SiteCode = new SelectList(db.v_PTLStaff_MasterData_Plants, "Plant", "PlantDesc", t_scorecard_master_template.SiteCode);
            return View(t_scorecard_master_template);
        }

        // GET: /ScorecardTemplate/Edit/5
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Master_Template t_scorecard_master_template = db.t_Scorecard_Master_Template.Find(id);
            if (t_scorecard_master_template == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterCatID = new SelectList(db.t_Scorecard_Master_Categories, "ID", "MasterCatCode", t_scorecard_master_template.MasterCatID);
            ViewBag.MinorCatID = new SelectList(db.t_Scorecard_Minor_Categories, "ID", "MinorCatCode", t_scorecard_master_template.MinorCatID);
            ViewBag.SiteCode = new SelectList(db.v_PTLStaff_MasterData_Plants, "Plant", "PlantDesc", t_scorecard_master_template.SiteCode);
            return View(t_scorecard_master_template);
        }

        // POST: /ScorecardTemplate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Edit([Bind(Include = "Recid,SiteCode,MasterCatID,MinorCatID,Actual,Target,Comments,LastChangedBy,LastChangedDate,Confirmed")] t_Scorecard_Master_Template t_scorecard_master_template)
        {
            if (ModelState.IsValid)
            {
                t_scorecard_master_template.LastChangedBy = User.Identity.Name;
                t_scorecard_master_template.LastChangedDate = DateTime.Now;
                db.Entry(t_scorecard_master_template).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MasterCatID = new SelectList(db.t_Scorecard_Master_Categories, "ID", "MasterCatCode", t_scorecard_master_template.MasterCatID);
            ViewBag.MinorCatID = new SelectList(db.t_Scorecard_Minor_Categories, "ID", "MinorCatCode", t_scorecard_master_template.MinorCatID);
            ViewBag.SiteCode = new SelectList(db.v_PTLStaff_MasterData_Plants, "Plant", "PlantDesc", t_scorecard_master_template.SiteCode);
            return View(t_scorecard_master_template);
        }

        // GET: /ScorecardTemplate/Edit/5 - change measures
        [Authorize(Roles = "canEdit")]
        public ActionResult EditSubmit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Master_Template t_scorecard_master_template = db.t_Scorecard_Master_Template.Find(id);
            if (t_scorecard_master_template == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterCatID = new SelectList(db.t_Scorecard_Master_Categories, "ID", "MasterCatCode", t_scorecard_master_template.MasterCatID);
            ViewBag.MinorCatID = new SelectList(db.t_Scorecard_Minor_Categories, "ID", "MinorCatCode", t_scorecard_master_template.MinorCatID);
            ViewBag.SiteCode = new SelectList(db.v_PTLStaff_MasterData_Plants, "Plant", "PlantDesc", t_scorecard_master_template.SiteCode);
            return View(t_scorecard_master_template);
        }

        // POST: /ScorecardTemplate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult EditSubmit([Bind(Include = "Recid,SiteCode,MasterCatID,MinorCatID,Actual,Target,Comments,LastChangedBy,LastChangedDate,Confirmed")] t_Scorecard_Master_Template t_scorecard_master_template)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_scorecard_master_template).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexSubmit");
            }
            ViewBag.MasterCatID = new SelectList(db.t_Scorecard_Master_Categories, "ID", "MasterCatCode", t_scorecard_master_template.MasterCatID);
            ViewBag.MinorCatID = new SelectList(db.t_Scorecard_Minor_Categories, "ID", "MinorCatCode", t_scorecard_master_template.MinorCatID);
            ViewBag.SiteCode = new SelectList(db.v_PTLStaff_MasterData_Plants, "Plant", "PlantDesc", t_scorecard_master_template.SiteCode);
            return View(t_scorecard_master_template);
        }

        // GET: /ScorecardTemplate/Delete/5
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Scorecard_Master_Template t_scorecard_master_template = db.t_Scorecard_Master_Template.Find(id);
            if (t_scorecard_master_template == null)
            {
                return HttpNotFound();
            }
            return View(t_scorecard_master_template);
        }

        // POST: /ScorecardTemplate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult DeleteConfirmed(long id)
        {
            t_Scorecard_Master_Template t_scorecard_master_template = db.t_Scorecard_Master_Template.Find(id);
            db.t_Scorecard_Master_Template.Remove(t_scorecard_master_template);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DataSubmit")]
        [Authorize(Roles = "canEdit")]
        public ActionResult DataSubmit(string FinYear, string FinWeek, string UserIdentity, string DefaultSite)
        {
            int FinYearint = Convert.ToInt32(FinYear);
            int FinWeekint = Convert.ToInt32(FinWeek);
            //Check to see if data already exists
            //var deletescorecardrows =
            //    from scorecarddetails in db.t_Scorecard_Details
            //    where (scorecarddetails.Year == Convert.ToInt32(FinYear) && scorecarddetails.Week == Convert.ToInt32(FinWeek) && scorecarddetails.SiteCode == DefaultSite)
            //    select scorecarddetails;

            IQueryable<t_Scorecard_Details> deletescorecardrows = db.t_Scorecard_Details
                .Where(c => c.SiteCode == DefaultSite && c.Year == FinYearint && c.Week == FinWeekint);

            foreach (var deletescorcard in deletescorecardrows)
                {
                    db.t_Scorecard_Details.Remove(deletescorcard);
                }
            db.SaveChanges();
 

            var Newdetail = new t_Scorecard_Details();

            IQueryable<t_Scorecard_Master_Template> scorecardtemplates = db.t_Scorecard_Master_Template
                .Where(c => c.SiteCode == DefaultSite);

            foreach (var Scorecardt in scorecardtemplates)
            {
                Newdetail = new t_Scorecard_Details();
                Newdetail.SiteCode = Scorecardt.SiteCode;
                Newdetail.Year = Convert.ToInt32(FinYear);
                Newdetail.Week = Convert.ToInt32(FinWeek);
                Newdetail.MasterCatID = Scorecardt.MasterCatID;
                Newdetail.MinorCatID = Scorecardt.MinorCatID;
                Newdetail.Actual = Scorecardt.Actual;
                Newdetail.Target = Scorecardt.Target;
                Newdetail.Comments = Scorecardt.Comments;
                Newdetail.ChangedBy = UserIdentity;
                Newdetail.ChangedDate = DateTime.Now;
                Newdetail.Confirmed = "Y";
                db.t_Scorecard_Details.Add(Newdetail);
            }
            db.SaveChanges();

            //ViewBag.SubMessage = "Your data has been submitted";
            return RedirectToAction("Index","Scorecard_Details");
         }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateSitesDropDownList(object selectedSite = null)
        {
            var sitesQuery = from d in db.v_PTLStaff_MasterData_Plants
                                   orderby d.PlantDesc
                                   select d;
            ViewBag.RecID = new SelectList(sitesQuery, "Plant", "PlantDesc", selectedSite);
        }
    }
}
