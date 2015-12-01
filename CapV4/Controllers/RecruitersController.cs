using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CapModel;

namespace CapV4.Controllers
{ 

    public class RecruitersController : Controller
    {
        Model1Container db = new Model1Container();

        // GET: Recruiters
        public ActionResult Index()
        {
            ViewData["message"] = "Welcome to Interviewtube !!";
            ViewData["recuiter"] = GetRecruiter();
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {   ViewData["message"]  = "Welcome to Interviewtube !!";
            ViewData["recuiter"] = GetRecruiter();
            ViewData["company"] = GetCompany();
            ViewData["country"] = GetCountries();
            ViewData["province"] = GetProvince();
            ViewData["city"] = GetCities();
            ViewData["category"] = GetCategories();
            ViewData["subcategory"] = GetSubcategories();
            string selectCriteria = form["select"].ToString();
            string recuiter = form["recuiter"].ToString();
            int recId = (from Recruiter in db.Recruiters
                          where Recruiter.UserName.Contains(recuiter)
                          select Recruiter.RecId).SingleOrDefault();

            var recAccess = from Recruiter in db.Recruiters
                            where Recruiter.RecId == recId
                            select Recruiter.HasAccess;
            if (recAccess.Contains("Yes"))
            {
                if (selectCriteria.Contains("CreateRec"))
                {
                    return View("Create");
                }
                else if (selectCriteria.Contains("EditRec"))
                {
                    return RedirectToAction("Edit", "Recruiters", new { id = recId });
                }
                else if (selectCriteria.Contains("postjobs"))
                {
                    return RedirectToAction("Create", "JobPosting");
                }
                else if (selectCriteria.Contains("searchusers"))
                {
                    return RedirectToAction("Create", "");
                }
                else if (selectCriteria.Contains("jobapplied"))
                {
                    return RedirectToAction("Index", "JobApplieds", new { id = recId });
                }
                else if (selectCriteria.Contains("reviewPosts"))
                {
                    return RedirectToAction("JobPosts", "JobPosting", new { id = recId });
                }
                else
                {
                    return View();
                }
            }
            else if (recAccess.Contains("No"))
            {
                ViewData["message"] = "Access Denied !!";
                if (selectCriteria.Contains("CreateRec"))
                {
                    return View("Create");
                }
                else if (selectCriteria.Contains("EditRec"))
                {
                    return RedirectToAction("Edit", "Recruiters", new { id = recId });
                }
                else if (selectCriteria.Contains("postjobs"))
                {
                    return View();
                }
                else if (selectCriteria.Contains("searchusers"))
                {
                    return View();
                }
                else if (selectCriteria.Contains("jobapplied"))
                {
                    return View();
                }
                else if (selectCriteria.Contains("reviewPosts"))
                {
                    return View();
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult ManageRecuiters(int? id)
        {
            //var recruiterList = from Recruiter in db.Recruiters
            //                    where Recruiter.CompanyCompId.Equals(id)
            //              select Recruiter;
            var recruiterList = db.Recruiters
                            .Where(r => r.CompanyCompId == id);
            // var recruiterList = db.Recruiters; 
            return View(recruiterList.ToList());
        }
        public ActionResult EditRecruiters(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            return View(recruiter);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRecruiters([Bind(Include = "recId,Department, JobTitle, HasAccess, CompanyCompId, UserName")] Recruiter recruiter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recruiter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }
            return View(recruiter);
        }
        // GET: Recruiters/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Recruiters/Create
        public ActionResult Create()
        {
            ViewData["company"] = GetCompany();

            return View();
        }

        // POST: Recruiters/Create
        [HttpPost]
        public ActionResult Create(FormCollection form)
            //[Bind(Exclude="HasAccess,CompanyCompId,UserName")]  Recruiter recruiter)
        { 
            //if (ModelState.IsValid)
            //{
            //string companyid = form["company"].ToString();
          
            //recruiter.HasAccess = "Y";
            //recruiter.CompanyCompId = 1 ;
            //    db.Recruiters.Add(recruiter);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(recruiter);
            ViewData["company"] = GetCompany();

            string department = form["department"].ToString();
            string jobtitle = form["jobtitle"].ToString();
            string security = form["securityCode"].ToString();
            string company = form["company"].ToString();
            string username = form["username"].ToString();
            string giveAccess = "false";
            int companyid = (from Company in db.Companies
                             where Company.CompName.Contains(company)
                             select Company.CompId).SingleOrDefault();
            string securityCode = (from Company in db.Companies
                                   where Company.CompId.Equals(companyid)
                                   select Company.CompCode).SingleOrDefault();
            if (security.Contains(securityCode))
            {
                giveAccess = "true";
            }
            Recruiter recuiter = new Recruiter()
            {
                Department = department,
                JobTitle = jobtitle,
                HasAccess = giveAccess,
                CompanyCompId = companyid,
                UserName = username
            };
            db.Recruiters.Add(recuiter);
            db.SaveChanges();
            return View();
        }

        // GET: Recruiters/Edit/5
        public ActionResult Edit(int? id)
        {
          if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            return View(recruiter);
        
        }

        // POST: Recruiters/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "recId,Department, JobTitle, HasAccess, CompanyCompId, UserName")] Recruiter recruiter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recruiter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recruiter);
        }
        // GET: Recruiters/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recruiters/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public SelectList GetCompany()
        {
            var company = from Company in db.Companies
                          group Company by Company.CompName into unique
                          select unique.FirstOrDefault();
            return new SelectList(company, "CompName", "CompName", "CompId");

        }
        public SelectList GetRecruiter()
        {
            var recruiter = from Recruiter in db.Recruiters
                          group Recruiter by Recruiter.UserName into unique
                          select unique.FirstOrDefault();
            return new SelectList(recruiter, "UserName", "UserName", "RecId");

        }
        public SelectList GetCountries()
        {
            var country = from Country in db.Countries
                          group Country by Country.CountryName into unique
                          select unique.FirstOrDefault();
            return new SelectList(country, "CountryName", "CountryName", "Id");
        }

        public SelectList GetProvince()
        {
            var country = from Country in db.Countries
                          where Country.CountryName.Contains("CA")
                          group Country by Country.Province into unique
                          select unique.FirstOrDefault();
            return new SelectList(country, "Province", "Province", "Id");
        }
        public SelectList GetCities()
        {
            var country = from Country in db.Countries
                          where Country.Province.Contains("Ontario")
                          group Country by Country.City into unique
                          select unique.FirstOrDefault();
            return new SelectList(country, "City", "City", "Id");
        }

        public SelectList GetCategories()
        {
            var jobcategories = from JobCategory in db.JobCategories
                                group JobCategory by JobCategory.CatName into uniqueCategories
                                select uniqueCategories.FirstOrDefault();

            return new SelectList(jobcategories, "CatName", "CatName", "Id");
        }

        public SelectList GetSubcategories()
        {
            var jobcategories = from JobCategory in db.JobCategories
                                where JobCategory.CatName.Contains("Software Development")
                                group JobCategory by JobCategory.SubCategory into uniqueCategories
                                select uniqueCategories.FirstOrDefault();

            return new SelectList(jobcategories, "SubCategory", "SubCategory", "Id");
        }
    
}
}

