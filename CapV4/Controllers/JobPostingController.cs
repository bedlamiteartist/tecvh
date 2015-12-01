
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using CapModel;

using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;

namespace CapV4.Controllers
{
    public class JobPostingController : Controller
    {
        private Model1Container db = new Model1Container();
        // GET: JobPosting
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["country"] = GetCountries();
            ViewData["province"] = GetProvince();
            ViewData["city"] = GetCities();
            ViewData["category"] = GetCategories();
            ViewData["subcategory"] = GetSubcategories();
            var jobPostings = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                                .Where(j => j.JobVisible.Contains("Yes"));
            return View(jobPostings.ToList());
        }
       
        public ActionResult Index(int? id)
        {
            ViewData["country"] = GetCountries();
            ViewData["province"] = GetProvince();
            ViewData["city"] = GetCities();
            ViewData["category"] = GetCategories();
            ViewData["subcategory"] = GetSubcategories();
            var jobPostings = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                                .Where(j => j.CompanyCompId == id && j.JobVisible.Contains("Yes"));
                                        
            return View(jobPostings.ToList());
        }
        public ActionResult JobPosts(int? id)
        {
            var jobPostings = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                                .Where(j => j.RecruiterRecId == id);
                                
            return View(jobPostings.ToList());
        }
        
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            //to puplate data in drop downs
            ViewData["country"] = GetCountries();
            ViewData["province"] = GetProvince();
            ViewData["city"] = GetCities();
            ViewData["category"] = GetCategories();
            ViewData["subcategory"] = GetSubcategories();

            //to get data submitted
            string textsearch = form["textserach"].ToString();
            string levelSearch = form["joblevel"];
            string typeSearch = form["jobtype"];
            string countrySearch = form["country"].ToString();
            string provinceSearch = form["province"].ToString();
            string citySearch = form["city"].ToString();
            string catSearch = form["category"].ToString();
            string subcatSearch = form["subcategory"].ToString();

            string salaryrange1 = form["salRange1"].ToString();
            string salaryrange4 = form["salRange4"].ToString();
            string salaryrange5 = form["salRange5"].ToString();
            string salaryrange6 = form["salRange6"].ToString();
            string salaryrange7 = form["salRange7"].ToString();
            string salaryrange8 = form["salRange8"].ToString();
            string salaryrange9 = form["salRange9"].ToString();
            string salaryrange10 = form["salRange10"].ToString();

            string companysearch = form["companySearch"].ToString();
            string titlesearch = form["titleSearch"].ToString();
            string postingdateSearch = form["postingdate"].ToString();

            //populate search criteria 
            string searchData = "The Search criteria you entered is\n";

            //if no search criteria is specified show all jobs
          
         //  string queryCriteria =  "JobVisible.Contains(" + "Yes" +")";
            Expression<Func<JobPosting, bool>> queryCriteria = a => a.JobVisible.Contains("Yes");

            //check if feilds are not null, then enter those in search criteria display
            if (textsearch.Length != 0) {
              // queryCriteria = 
                searchData += "Keywords Entered: " + textsearch;
            }
            if (levelSearch != null) {
                searchData += ", Job Level: " + levelSearch; 
            }
            if (typeSearch != null) { 
                searchData += ", Job Type: " + typeSearch; }
            if (countrySearch.Length != 0) { searchData += ", Preferred Country: " + countrySearch; }
            if (provinceSearch.Length != 0) { searchData += ", Preferred Province: " + provinceSearch; }
            if (citySearch.Length != 0) { searchData += ", Preferred City: " + citySearch; }
            if (catSearch.Length != 0) { searchData += ", Job Category: " + catSearch; }
            if (subcatSearch.Length != 0) { searchData += ", Subcategory: " + subcatSearch; }
            if (companysearch.Length != 0) { searchData += ", Desired Company: " + companysearch; }
            if (titlesearch.Length != 0) { searchData += ", Title Contains: " + titlesearch; }

            float minSalary, maxSalary;
           
            // if text search is entered 
            if (salaryrange1.Contains("true") && salaryrange4.Contains("true") && salaryrange6.Contains("true") && salaryrange7.Contains("true") && salaryrange8.Contains("true") && salaryrange9.Contains("true") && salaryrange10.Contains("true"))
            {
                minSalary = 1; maxSalary = 599999;
                searchData += ", Expected salary range: $1 - $5,99,999 per annum";

            }
            else if (salaryrange1.Contains("true") && salaryrange4.Contains("true") && salaryrange6.Contains("true") && salaryrange7.Contains("true") && salaryrange8.Contains("true") && salaryrange9.Contains("true"))
            {
                minSalary = 1; maxSalary = 99999;
                searchData += ", Expected salary range: $1 - $99,999 per annum";
            }
            else if (salaryrange1.Contains("true") && salaryrange4.Contains("true") && salaryrange6.Contains("true") && salaryrange7.Contains("true") && salaryrange8.Contains("true"))
            {
                minSalary = 1; maxSalary = 89999;
                searchData += ", Expected salary range: $1 - $89,999 per annum";
            }
            else if (salaryrange1.Contains("true") && salaryrange4.Contains("true") && salaryrange6.Contains("true") && salaryrange7.Contains("true"))
            {
                minSalary = 1; maxSalary = 79999;
                searchData += ", Expected salary range: $1 - $79,999 per annum";
            }
            else if (salaryrange1.Contains("true") && salaryrange4.Contains("true") && salaryrange6.Contains("true"))
            {
                minSalary = 1; maxSalary = 69999;
                searchData += ", Expected salary range: $1 - $69,999 per annum";
            }
            else if (salaryrange1.Contains("true") && salaryrange4.Contains("true"))
            {
                minSalary = 1; maxSalary = 59999;
                searchData += ", Expected salary range: $1 - $59,999 per annum";
            }
            else if (salaryrange4.Contains("true") && salaryrange6.Contains("true") && salaryrange7.Contains("true") && salaryrange8.Contains("true") && salaryrange9.Contains("true") && salaryrange10.Contains("true"))
            {
                minSalary = 40000; maxSalary = 599999;
                searchData += ", Expected salary range: $40,000 - $5,99,999 per annum";
            }
            else if (salaryrange6.Contains("true") && salaryrange7.Contains("true") && salaryrange8.Contains("true") && salaryrange9.Contains("true") && salaryrange10.Contains("true"))
            {
                minSalary = 60000; maxSalary = 599999;
                searchData += ", Expected salary range: $60,000 - $5,99,999 per annum";
            }
            else if (salaryrange7.Contains("true") && salaryrange8.Contains("true") && salaryrange9.Contains("true") && salaryrange10.Contains("true"))
            {
                minSalary = 70000; maxSalary = 599999;
                searchData += ", Expected salary range: $70,000 - $5,99,999 per annum";
            }
            else if (salaryrange8.Contains("true") && salaryrange9.Contains("true") && salaryrange10.Contains("true"))
            {
                minSalary = 80000; maxSalary = 599999;
                searchData += ", Expected salary range: $80,000 - $5,99,999 per annum";
            }
            else if (salaryrange9.Contains("true") && salaryrange10.Contains("true"))
            {
                minSalary = 90000; maxSalary = 599999;
                searchData += ", Expected salary range: $90,000 - $5,99,999 per annum";
            }
            else if (salaryrange8.Contains("true") && salaryrange9.Contains("true"))
            {
                minSalary = 80000; maxSalary = 99999;
                searchData += ", Expected salary range: $80,000 - $99,999 per annum";
            }
            else if (salaryrange7.Contains("true") && salaryrange8.Contains("true"))
            {
                minSalary = 70000; maxSalary = 89999;
                searchData += ", Expected salary range: $70,000 - $89,999 per annum";
            }
            else if (salaryrange5.Contains("true") && salaryrange6.Contains("true"))
            {
                minSalary = 50000; maxSalary = 69999;
                searchData += ", Expected salary range: $50,000 - $69,999 per annum";
            }
            else if (salaryrange4.Contains("true") && salaryrange5.Contains("true"))
            {
                minSalary = 40000; maxSalary = 59999;
                searchData += ", Expected salary range: $40,000 - $59,999 per annum";
            }
            else if (salaryrange1.Contains("true"))
            {
                minSalary = 1; maxSalary = 39999;
                searchData += ", Expected salary range: $1 - $39,999 per annum";
            }
            else if (salaryrange4.Contains("true"))
            {
                minSalary = 40000; maxSalary = 49999;
                searchData += ", Expected salary range: $40,000 - $49,999 per annum";
            }
            else if (salaryrange5.Contains("true"))
            {
                minSalary = 50000; maxSalary = 59999;
                searchData += ", Expected salary range: $50,000 - $59,999 per annum";
            }
            else if (salaryrange6.Contains("true"))
            {
                minSalary = 60000; maxSalary = 69999;
                searchData += ", Expected salary range: $60,000 - $69,999 per annum";
            }
            else if (salaryrange7.Contains("true"))
            {
                minSalary = 70000; maxSalary = 79999;
                searchData += ", Expected salary range: $1 - $89,999 per annum";
            }
            else if (salaryrange8.Contains("true"))
            {
                minSalary = 80000; maxSalary = 89999;
                searchData += ", Expected salary range: $80,000 - $89,999 per annum";
            }
            else if (salaryrange9.Contains("true"))
            {
                minSalary = 90000; maxSalary = 99999;
                searchData += ", Expected salary range: $90,000 - $99,999 per annum";
            }
            else if (salaryrange10.Contains("true"))
            {
                minSalary = 100000; maxSalary = 599999;
                searchData += ", Expected Salary Range is greater than $100,000 per annum";
            }
            else
            {
                minSalary = 1;
                maxSalary = 599999;
            }
            DateTime oldestDate = DateTime.UtcNow.Date.AddDays(-30);
            if (postingdateSearch == "1")
            {
                searchData += ", Job is Posted today";
                oldestDate = DateTime.UtcNow.Date.AddDays(1);
            }
            else if (postingdateSearch == "2")
            {
                searchData += ", Posting date <= 2 days";
                oldestDate = DateTime.UtcNow.Date.AddDays(-1);
            }
            else if (postingdateSearch == "3")
            {
                searchData += ", Posting date <= 3 days";
                oldestDate = DateTime.UtcNow.Date.AddDays(-3);
            }
            else if (postingdateSearch == "7")
            {
                searchData += ", Posting date <= 7 days";
                oldestDate = DateTime.UtcNow.Date.AddDays(-7);
            }
            else if (postingdateSearch == "14")
            {
                searchData += ", Posting date <= 14 days";
                oldestDate = DateTime.UtcNow.Date.AddDays(-14);
            }
            else if (postingdateSearch == "30")
            {
                searchData += ", Posting date <= 30 days";
                oldestDate = DateTime.UtcNow.Date.AddDays(-30);
            }

            //query to get jobs as per job search
            var jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                                 .Where(j => j.JobVisible.Contains("Yes") &&
                                          j.JobCompensation >= minSalary &&
                                         j.JobCompensation <= maxSalary &&
                                         (j.PostStartDate >= oldestDate));

            if (typeSearch == null && levelSearch == null && textsearch.Length != 0 && countrySearch.Length != 0 && provinceSearch.Length != 0 && citySearch.Length != 0 && companysearch.Length != 0 && catSearch.Length != 0 && subcatSearch.Length != 0 && titlesearch.Length != 0)
            {
                 jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                                 .Where(j => j.JobTitle.Contains(textsearch) &&
                                             j.Description.Contains(textsearch) &&
                                             j.Location.Country.Contains(countrySearch) &&
                                             j.Location.Province.Contains(provinceSearch) &&
                                             j.Location.City.Contains(citySearch) &&
                                             j.Company.CompName.Contains(companysearch) &&
                                             (j.JobCompensation >= minSalary &&
                                             j.JobCompensation <= maxSalary) &&
                                             j.JobCategory.Contains(catSearch) &&
                                             j.JobSubcategory.Contains(subcatSearch) &&
                                             j.JobTitle.Contains(titlesearch) &&
                                             (j.PostStartDate >= oldestDate) &&
                                             j.JobVisible.Contains("Yes")
                                             );

            }
            else if (typeSearch == null && levelSearch != null && textsearch.Length != 0 && countrySearch.Length != 0 && provinceSearch.Length != 0 && citySearch.Length != 0 && companysearch.Length != 0 && catSearch.Length != 0 && subcatSearch.Length != 0 && titlesearch.Length != 0)
            {
                jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                            .Where(j => j.JobTitle.Contains(textsearch) &&
                                        j.Description.Contains(textsearch) &&
                                        j.Location.Country.Contains(countrySearch) &&
                                        j.Location.Province.Contains(provinceSearch) &&
                                        j.Location.City.Contains(citySearch) &&
                                        j.Company.CompName.Contains(companysearch) &&
                                        (j.JobCompensation >= minSalary &&
                                        j.JobCompensation <= maxSalary) &&
                                        j.JobCategory.Contains(catSearch) &&
                                        j.JobSubcategory.Contains(subcatSearch) &&
                                        j.JobTitle.Contains(titlesearch) &&
                                        (j.PostStartDate >= oldestDate)
                                      && j.JobLevel.Contains(levelSearch)
                                      && j.JobVisible.Contains("Yes")
                                        );

            }
            else if (typeSearch != null && levelSearch == null && textsearch.Length != 0 && countrySearch.Length != 0 && provinceSearch.Length != 0 && citySearch.Length != 0 && companysearch.Length != 0 && catSearch.Length != 0 && subcatSearch.Length != 0 && titlesearch.Length != 0)
            {
                jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                            .Where(j => j.JobTitle.Contains(textsearch) &&
                                        j.Description.Contains(textsearch) &&
                                        j.Location.Country.Contains(countrySearch) &&
                                        j.Location.Province.Contains(provinceSearch) &&
                                        j.Location.City.Contains(citySearch) &&
                                        j.Company.CompName.Contains(companysearch) &&
                                        (j.JobCompensation >= minSalary &&
                                        j.JobCompensation <= maxSalary) &&
                                        j.JobCategory.Contains(catSearch) &&
                                        j.JobSubcategory.Contains(subcatSearch) &&
                                        j.JobTitle.Contains(titlesearch) &&
                                        (j.PostStartDate >= oldestDate)
                                        && j.JobType.Contains(typeSearch)
                                        && j.JobVisible.Contains("Yes")
                                        );

            }
            else if (countrySearch.Length != 0 && provinceSearch.Length != 0 && citySearch.Length != 0)
            {
                jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                            .Where(j => j.Location.Country.Contains(countrySearch) &&
                                        j.Location.Province.Contains(provinceSearch) &&
                                        j.Location.City.Contains(citySearch) &&
                                        (j.JobCompensation <= maxSalary) &&
                                        (j.PostStartDate >= oldestDate) 
                                        && j.JobVisible.Contains("Yes")
                                       );
            }
            else if (countrySearch.Length != 0 && provinceSearch.Length != 0)
            {
                jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                            .Where(j => j.Location.Country.Contains(countrySearch) &&
                                        j.Location.Province.Contains(provinceSearch) &&
                                        (j.JobCompensation <= maxSalary) &&
                                        (j.PostStartDate >= oldestDate)
                                        && j.JobVisible.Contains("Yes")
                                       );
            }
            else if (catSearch.Length != 0 && subcatSearch.Length != 0 )
            {
                jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                            .Where(j => j.JobCategory.Contains(catSearch) &&
                                        j.JobSubcategory.Contains(subcatSearch) &&
                                       (j.JobCompensation <= maxSalary) &&
                                        (j.PostStartDate >= oldestDate)
                                        && j.JobVisible.Contains("Yes")
                                        );

            }

                //when only one feild is use to search
            else
            {
                if (countrySearch.Length != 0)
                {
                    jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                                .Where(j => j.Location.Country.Contains(countrySearch) &&
                                            (j.JobCompensation <= maxSalary) &&
                                            (j.PostStartDate >= oldestDate)
                                            && j.JobVisible.Contains("Yes")
                                           );
                }
                 if (textsearch.Length != 0)
                {
                    jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                                .Where(j => j.JobTitle.Contains(textsearch) &&
                                            j.Description.Contains(textsearch) &&
                                            (j.JobCompensation >= minSalary &&
                                            j.JobCompensation <= maxSalary) &&
                                            (j.PostStartDate >= oldestDate)
                                            && j.JobVisible.Contains("Yes")
                                            );
                }
                 if (levelSearch != null)
                 {
                     jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                             .Where(j => j.JobLevel.Contains(levelSearch) &&
                                         (j.JobCompensation >= minSalary &&
                                         j.JobCompensation <= maxSalary) &&
                                         (j.PostStartDate >= oldestDate) 
                                         && j.JobVisible.Contains("Yes")
                                         );
                 }
                 if (typeSearch != null)
                 {
                     jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                             .Where(j => j.JobType.Contains(typeSearch) &&
                                         (j.JobCompensation >= minSalary &&
                                         j.JobCompensation <= maxSalary) &&
                                         (j.PostStartDate >= oldestDate)
                                         && j.JobVisible.Contains("Yes")
                                         );
                 }
                 if (catSearch.Length != 0) {
                     jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                             .Where(j => j.JobCategory.Contains(catSearch) &&
                                         (j.JobCompensation >= minSalary &&
                                         j.JobCompensation <= maxSalary) &&
                                         (j.PostStartDate >= oldestDate)
                                         && j.JobVisible.Contains("Yes")
                                         );
                 }
                 if (companysearch.Length != 0) {
                     jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                              .Where(j => j.Company.CompName.Contains(companysearch) &&
                                          (j.JobCompensation >= minSalary &&
                                          j.JobCompensation <= maxSalary) &&
                                          (j.PostStartDate >= oldestDate)
                                          && j.JobVisible.Contains("Yes")
                                          );
                 }
                 if (titlesearch.Length != 0) {
                     jobQuery = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                             .Where(j => j.JobTitle.Contains(titlesearch) &&
                                         (j.JobCompensation >= minSalary &&
                                         j.JobCompensation <= maxSalary) &&
                                         (j.PostStartDate >= oldestDate)
                                         && j.JobVisible.Contains("Yes")
                                         );
                 }
            }
            //test data entered - only for testing purposes
            var jobPostings = db.JobPostings.Include(j => j.UserMedia).Include(j => j.Company)
                                               .Where(queryCriteria);
            //var jobPostings = from JobPosting in db.JobPostings where () select JobPosting;
           // var jobPostings = jobPostingsstring;           
            ViewData["result"] = searchData;
            return View(jobQuery.ToList());
            //return View(jobPostings.ToList());
        }
        // GET: JobPostings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPosting jobPosting = db.JobPostings.Find(id);
            if (jobPosting == null)
            {
                return HttpNotFound();
            }
            return View(jobPosting);
        }

        // GET: JobPostings/Create
        public ActionResult Create()
        {
            ViewData["company"] = GetCompany();
            ViewData["recruiter"] = GetRecruiter();
            ViewData["level"] = GetJobLevel();
            ViewData["type"] = GetJobType();
            ViewData["category"] = GetCategories();
            ViewData["subcategory"] = GetSubcategories();
            ViewData["country"] = GetCountries();
            ViewData["province"] = GetProvince();
            ViewData["city"] = GetCities();
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            // to populate the drop down
            ViewData["company"] = GetCompany();
            ViewData["recruiter"] = GetRecruiter();
            ViewData["level"] = GetJobLevel();
            ViewData["type"] = GetJobType();
            ViewData["category"] = GetCategories();
            ViewData["subcategory"] = GetSubcategories();
            ViewData["country"] = GetCountries();
            ViewData["province"] = GetProvince();
            ViewData["city"] = GetCities();

            //to get data submmited in form
            string jobtitle = form["jobtitle"].ToString();
            string compname = form["company"].ToString();
            string recname = form["recruiter"].ToString();
            string positions = form["positions"];
            string joblevel = form["level"].ToString();
            string jobtype = form["type"].ToString();
            DateTime postingdate = Convert.ToDateTime(form["postingdate"]);
            DateTime closingdate = Convert.ToDateTime(form["closingdate"]);
            double jobSalary = Convert.ToDouble(form["jobCompensation"]);
            string category = form["category"].ToString();
            string subcategory = form["category"].ToString();
            string aptNum = form["aptNum"].ToString();
            string streetAddress = form["streetAdress"].ToString();
            string city = form["city"].ToString();
            string province = form["province"].ToString();
            string country = form["country"].ToString();
            string jobDescripition = form["jobdesciption"].ToString();
            string jobDuties = form["jobRequirements"].ToString();
            string jobrequirements = form["jobDuties"].ToString();
            string verifyDisplay = form["showJob"].ToString();
            string jobVisible = "false";
            string postalcode = form["postalcode"].ToString();
            int company = (from Company in db.Companies
                           where Company.CompName.Contains(compname)
                           select Company.CompId).SingleOrDefault();
            int recruiter = (from Recruiter in db.Recruiters
                             where Recruiter.UserName.Contains(recname)
                             select Recruiter.RecId).SingleOrDefault();
            if (verifyDisplay.Contains("true"))
            {
                jobVisible = "true";
            }
         //   int compId = Convert.ToInt32(company);

            JobPosting jobposting = new JobPosting()
            {
                JobTitle = jobtitle,
                JobType = jobtype,
                PostStartDate = postingdate,
                PostEndDate = closingdate,
                NumPositions = positions,
                JobLevel = joblevel,
                JobCompensation = jobSalary,
                Description = jobDescripition,
                JobReq = jobrequirements,
                JobDuties = jobrequirements,
                CompanyCompId = company,
                RecruiterRecId = recruiter,
                JobVisible = jobVisible,
                JobCategory = category,
                JobSubcategory = subcategory,
            };
            db.JobPostings.Add(jobposting);
            Location loc = new Location();

            loc.AptNum = aptNum;
            loc.StreetAdd = streetAddress;
            loc.City = city;
            loc.Country = country;
            loc.Province = province;
            jobposting.Location = loc;
            loc.PostalCode = postalcode;
           
            db.Locations.Add(jobposting.Location);

            db.SaveChanges();
            return View();
        }

        // GET: JobPostings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPosting jobPosting = db.JobPostings.Find(id);
            if (jobPosting == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecruiterRecId = new SelectList(db.Recruiters, "RecId", "Department", jobPosting.RecruiterRecId);
            ViewBag.CompanyCompId = new SelectList(db.Companies, "CompId", "CompName", jobPosting.CompanyCompId);
            return View(jobPosting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobPostId,JobTitle,JobType,PostStartDate,PostEndDate,NumPositions,JobLevel,JobCompensation,Description,JobReq,JobDuties,CompanyCompId,RecruiterRecId,JobVisible,JobCategory,JobSubcategory")] JobPosting jobPosting, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobPosting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Recruiters");
            }
            ViewBag.CompanyCompId = new SelectList(db.Companies, "CompId", "CompName", jobPosting.CompanyCompId);
            ViewBag.RecruiterRecId = new SelectList(db.Recruiters, "RecId", "Department", jobPosting.RecruiterRecId);
            return View(jobPosting);
        }

        public SelectList GetCountries()
        {
            var country = from Country in db.Countries
                          group Country by Country.CountryName into unique
                          select unique.FirstOrDefault();
            return new SelectList(country, "CountryName", "CountryName", "Id");
        }

        public SelectList GetProvince( string selection)
        {
            var country = from Country in db.Countries
                          where Country.CountryName.Contains(selection)
                          group Country by Country.Province into unique
                          select unique.FirstOrDefault();
            return new SelectList(country, "Province", "Province", "Id");
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
        public SelectList GetCities(string selection)
        {
            var country = from Country in db.Countries
                          where Country.Province.Contains(selection)
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
        public SelectList GetJobLevel()
        {
            List<string> joblevels = new List<string>();
            joblevels.Add("Student");
            joblevels.Add("Entry Level");
            joblevels.Add("Experienced (Non-manager)");
            joblevels.Add("Manager");
            joblevels.Add("Executive");
            joblevels.Add("Senior - Executive");
            return new SelectList(joblevels);
        }
        public SelectList GetJobType()
        {
            List<string> jobtypes = new List<string>();
            jobtypes.Add("Part-time");
            jobtypes.Add("Full-time");
            jobtypes.Add("Temporary/Contract");
            jobtypes.Add("Intern");
            return new SelectList(jobtypes);
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
            var company = from Recruiter in db.Recruiters
                          group Recruiter by Recruiter.UserName into unique
                          select unique.FirstOrDefault();
            return new SelectList(company, "UserName", "UserName", "RecId");
        }
    }
}
