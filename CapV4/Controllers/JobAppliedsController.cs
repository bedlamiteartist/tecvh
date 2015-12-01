using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapModel;

namespace CapV4.Controllers
{
    public class JobAppliedsController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: JobApplieds
        public ActionResult Index(int? id)
        {
            var jobApplieds = db.JobApplieds.Include(j => j.JobPosting).Include(j => j.JobSeeker)
                                .Where(j => j.JobPosting.RecruiterRecId == id);
            return View(jobApplieds.ToList());
        }

        // GET: JobApplieds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplied jobApplied = db.JobApplieds.Find(id);
            if (jobApplied == null)
            {
                return HttpNotFound();
            }
            return View(jobApplied);
        }

        // GET: JobApplieds/Create
        public ActionResult Create(int id)
        {
            int jid = Convert.ToInt16(id);
            var jobPostingID = from JobPosting in db.JobPostings
                               where JobPosting.JobPostId.Equals(id)
                               group JobPosting by JobPosting.JobPostId into unique
                               select unique.FirstOrDefault();
           // ViewBag.ApplicationDate = (DateTime.Now.Date).ToString();
            ViewBag.JobPostingJobPostId = new SelectList(jobPostingID, "JobPostId", "JobTitle");
            ViewBag.JobSeekerJSId = new SelectList(db.JobSeekers, "JSId", "UserName");
            return View();
        }

        // POST: JobApplieds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobAppId,ApplicationDate,AppliedMessage,JobPostingJobPostId,JobSeekerJSId")] JobApplied jobApplied)
        {
            if (ModelState.IsValid)
            {
                db.JobApplieds.Add(jobApplied);
                db.SaveChanges();
                return RedirectToAction("Index", "JobPosting");
            }

            ViewBag.JobPostingJobPostId = new SelectList(db.JobPostings, "JobPostId", "JobTitle", jobApplied.JobPostingJobPostId);
            ViewBag.JobSeekerJSId = new SelectList(db.JobSeekers, "JSId", "SkillSummary", jobApplied.JobSeekerJSId);
            return View(jobApplied);
        }

        // GET: JobApplieds/Edit/5
        public ActionResult Edit(int? id)
        {
            int jid = Convert.ToInt16(id);
            var jobPostingID = from JobPosting  in db.JobPostings
                               where JobPosting.JobPostId.Equals(id)
                               group JobPosting by JobPosting.JobPostId into unique
                          select unique.FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplied jobApplied = db.JobApplieds.Find(id);
            if (jobApplied == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobPostingJobPostId = new SelectList(jobPostingID, "JobPostId", "JobTitle", jobApplied.JobPostingJobPostId);
            ViewBag.JobSeekerJSId = new SelectList(db.JobSeekers, "JSId", "SkillSummary", jobApplied.JobSeekerJSId);
            return View(jobApplied);
        }

        // POST: JobApplieds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobAppId,ApplicationDate,AppliedMessage,JobPostingJobPostId,JobSeekerJSId")] JobApplied jobApplied)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobApplied).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobPostingJobPostId = new SelectList(db.JobPostings, "JobPostId", "JobTitle", jobApplied.JobPostingJobPostId);
            ViewBag.JobSeekerJSId = new SelectList(db.JobSeekers, "JSId", "SkillSummary", jobApplied.JobSeekerJSId);
            return View(jobApplied);
        }

        // GET: JobApplieds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplied jobApplied = db.JobApplieds.Find(id);
            if (jobApplied == null)
            {
                return HttpNotFound();
            }
            return View(jobApplied);
        }

        // POST: JobApplieds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobApplied jobApplied = db.JobApplieds.Find(id);
            db.JobApplieds.Remove(jobApplied);
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
