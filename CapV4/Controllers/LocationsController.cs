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
    public class LocationsController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: Locations
        public ActionResult Index()
        {
            return View(db.Locations.ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create(int? id)
        {
            var jobpost = from JobPostings in db.JobPostings
                          where JobPostings.JobPostId == id
                          group JobPostings by JobPostings.JobTitle into uniqueValue
                          select uniqueValue.FirstOrDefault();

            ViewData["JobPostId"] = new SelectList(jobpost, "JobTitle", "JobTitle");
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationId,AptNum,StreetAdd,City,Province,Country,PostalCode")] Location location, FormCollection form)
        {
            string jobTitle = form["JobPostId"].ToString();
            int jobpost = (from JobPostings in db.JobPostings
                          where JobPostings.JobTitle.Contains(jobTitle)
                           select JobPostings.JobPostId).SingleOrDefault();
            int id = Convert.ToInt16(jobpost);
            var jobpostid = from JobPostings in db.JobPostings
                          where JobPostings.JobPostId == id
                          group JobPostings by JobPostings.JobTitle into uniqueValue
                          select uniqueValue.FirstOrDefault();

            ViewData["JobPostId"] = new SelectList(jobpostid, "JobTitle", "JobTitle");
            Location loc = new Location();          

            if (ModelState.IsValid)
            {
                loc.JobPosting.JobPostId = Convert.ToInt16(id);
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var locationId = (from Location in db.Locations
                           where Location.JobPosting.JobPostId == id
                           select Location.LocationId).SingleOrDefault();
            Location location = db.Locations.Find(locationId);
           
            if (location == null)
            {
                return HttpNotFound();
            }
                     
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationId,AptNum,StreetAdd,City,Province,Country,PostalCode")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Recruiters");
            }
            return View(location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
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
