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
    public class JobSeekersController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: JobSeekers
        public ActionResult Index()
        {
            // var jobSeekers = db.JobSeekers.Include(j => j.Location);
            //return View(jobSeekers.ToList());
            ViewData["jobseeker"] = GetJobSeeker();
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            ViewData["jobseeker"] = GetJobSeeker();
            string selectCriteria = form["select"].ToString();
            if (selectCriteria.Contains("CreateJobSeeker"))
            {
                return View("Create");
            }
            else if (selectCriteria.Contains("EditJobSeeker"))
            {
                return RedirectToAction("Create");
            }
            else if (selectCriteria.Contains("searchJobs"))
            {
                return RedirectToAction("Index", "JobPosting");
            }
            else if (selectCriteria.Contains("videoresume"))
            {
                return RedirectToAction("Index", "Video");
            }
            else if (selectCriteria.Contains("searchComp"))
            {
                return RedirectToAction("CompanySearch", "Companies");
            }
            else
            {
                return View();
            }

        }

        public SelectList GetJobSeeker()
        {
            var recruiter = from JobSeeker in db.JobSeekers
                            group JobSeeker by JobSeeker.UserName into unique
                            select unique.FirstOrDefault();
            return new SelectList(recruiter, "UserName", "UserName", "JSId");
        }
        // GET: JobSeekers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobSeeker jobSeeker = db.JobSeekers.Find(id);
            if (jobSeeker == null)
            {
                return HttpNotFound();
            }
            return View(jobSeeker);
        }

        // GET: JobSeekers/Create
        public ActionResult Create()
        {
            ViewBag.JSId = new SelectList(db.Locations, "LocationId", "AptNum");
            return View();
        }

        // POST: JobSeekers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JSId,SkillSummary,PhoneNumber,Visibility,UserName")] JobSeeker jobSeeker)
        {
            if (ModelState.IsValid)
            {
                db.JobSeekers.Add(jobSeeker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JSId = new SelectList(db.Locations, "LocationId", "AptNum", jobSeeker.JSId);
            return View(jobSeeker);
        }

        // GET: JobSeekers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobSeeker jobSeeker = db.JobSeekers.Find(id);
            if (jobSeeker == null)
            {
                return HttpNotFound();
            }
            ViewBag.JSId = new SelectList(db.Locations, "LocationId", "AptNum", jobSeeker.JSId);
            return View(jobSeeker);
        }

        // POST: JobSeekers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JSId,SkillSummary,PhoneNumber,Visibility,UserName")] JobSeeker jobSeeker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobSeeker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JSId = new SelectList(db.Locations, "LocationId", "AptNum", jobSeeker.JSId);
            return View(jobSeeker);
        }

        // GET: JobSeekers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobSeeker jobSeeker = db.JobSeekers.Find(id);
            if (jobSeeker == null)
            {
                return HttpNotFound();
            }
            return View(jobSeeker);
        }

        // POST: JobSeekers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobSeeker jobSeeker = db.JobSeekers.Find(id);
            db.JobSeekers.Remove(jobSeeker);
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
