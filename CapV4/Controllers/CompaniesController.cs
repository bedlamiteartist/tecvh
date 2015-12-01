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
    public class CompaniesController : Controller
    {
        private Model1Container db = new Model1Container();
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
        // GET: Companies
        public ActionResult Index()
        {
            ViewData["company"] = GetCompany();
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
          
            ViewData["country"] = GetCountries();
            ViewData["province"] = GetProvince();
            ViewData["city"] = GetCities();
            ViewData["company"] = GetCompany();
            string selectCriteria = form["select"].ToString();
            string compName = form["company"].ToString();
            int compId = (from Company in db.Companies
                          where Company.CompName.Contains(compName)
                             select Company.CompId).SingleOrDefault();

            if (selectCriteria.Contains("CreateComp"))
            {
                return View("Create");
            }
            else if (selectCriteria.Contains("EditComp"))
            {
                return RedirectToAction("Edit", "Companies", new { id = compId });
            }
            else if (selectCriteria.Contains("CreateNews"))
            {
                return RedirectToAction("Create", "NewsPosts", new { id = compId });
            }
            else if (selectCriteria.Contains("ShowNews"))
            {
                return RedirectToAction("Index", "NewsPosts", new { id = compId });
            }
            else if (selectCriteria.Contains("SearchUser"))
            {
                return RedirectToAction("Index", "JobSeekers");
            }
            else if (selectCriteria.Contains("ManageRecruiter"))
            {
                return RedirectToAction("ManageRecuiters", "Recruiters", new { id = compId });
            }
            else
            {
                return View();
            }
        }
        public ActionResult CompanySearch()
        {               
            return View(db.Companies.ToList());
        }
         [HttpPost]
        public ActionResult CompanySearch(FormCollection form)
        {
            string compName = form["Compname"].ToString();
            var compList = db.Companies.Where(c => c.CompName.Contains(compName));
            return View(compList.ToList());
        }
        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewData["country"] = GetCountries();
            ViewData["province"] = GetProvince();
            ViewData["city"] = GetCities(); 
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            
            ViewData["country"] = GetCountries();
            ViewData["province"] = GetProvince();
            ViewData["city"] = GetCities(); 

            string companyname = form["compName"].ToString();
            string username = form["username"].ToString();
            string descripition = form["description"].ToString();
            string aptNum = form["aptNum"].ToString();
            string streetAddress = form["streetAdress"].ToString();
            string city = form["city"].ToString();
            string province = form["province"].ToString();
            string country = form["country"].ToString();
            string postalcode = form["postalcode"].ToString();
            Company company = new Company()
            {
                CompName = companyname,
                CompCode = randomCode(8),
                CompDescription = descripition,
                NumFollowers = 0,
                UserName = username
            };
            db.Companies.Add(company);
            Location loc = new Location();
            loc.AptNum = aptNum;
            loc.StreetAdd = streetAddress;
            loc.City = city;
            loc.Province = province;
            loc.Country = country;
            loc.PostalCode = postalcode;
            company.Location = loc;
            
            db.Locations.Add(company.Location);
            db.SaveChanges();
            return View();
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewData["country"] = GetCountries();
            ViewData["province"] = GetProvince();
            ViewData["city"] = GetCities(); 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompId,CompName,CompCode,CompDescription,NumFollowers,UserName")] Company company)
        {
            ViewData["country"] = GetCountries();
            ViewData["province"] = GetProvince();
            ViewData["city"] = GetCities(); 
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
         private static string randomCode(int length)
        {
           
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz~`!@#$%^&*()_+-={}[]:<>?,./|\\";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
         public SelectList GetCompany()
         {
             var company = from Company in db.Companies
                           group Company by Company.CompName into unique
                           select unique.FirstOrDefault();
             return new SelectList(company, "CompName", "CompName", "CompId");

         }
    }
    
}
