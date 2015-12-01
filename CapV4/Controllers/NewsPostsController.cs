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
    public class NewsPostsController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: NewsPosts
        public ActionResult Index(int? id)
        {
            var newsPosts = db.NewsPosts.Include(n => n.Company)
                                .Where(j => j.CompanyCompId == id);
                          
            return View(newsPosts.ToList());
        }

        public ActionResult ShowNews(int? id)
        {
            var newsPosts = db.NewsPosts.Include(n => n.Company)
                                .Where(j => j.CompanyCompId == id);

            return View(newsPosts.ToList());
        }

        // GET: NewsPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsPost newsPost = db.NewsPosts.Find(id);
            if (newsPost == null)
            {
                return HttpNotFound();
            }
            return View(newsPost);
        }

        // GET: NewsPosts/Create
        public ActionResult Create(int? id)
        {
           int compid = Convert.ToInt16(id);
           ViewData["company"] = GetCompany(compid);
            return View();
        }

        // POST: NewsPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "NPId,Title,NewsDesc,CompanyCompId", Exclude = " IsVisible, NewsDate")] NewsPost newsPost)
        public ActionResult Create(FormCollection form)
        {
            ViewData["company"] = GetCompany();
            ViewData["postingdate"] = DateTime.Now.Date;
            string newstitle = form["newstitle"].ToString();
            string postnews = form["shownews"].ToString();
            string company = form["company"].ToString();
            DateTime postingdate = Convert.ToDateTime(form["postingdate"]);
            string description = form["newsdesciption"].ToString();
            string verifyDisplay = "false";
            int companyid = (from Company in db.Companies
                             where Company.CompName.Contains(company)
                             select Company.CompId).SingleOrDefault();
            if (postnews.Contains("true"))
            {
                verifyDisplay = "true";
            }
            NewsPost news = new NewsPost()
            {
                Title = newstitle,
                NewsDate = postingdate.ToString(),
                NewsDesc = description,
                IsVisible = verifyDisplay,
                CompanyCompId = companyid
            };
            db.NewsPosts.Add(news);
            db.SaveChanges();
            return View();
        }
         public SelectList GetCompany()
        {
            var company = from Company in db.Companies
                          group Company by Company.CompName into unique
                          select unique.FirstOrDefault();
            return new SelectList(company, "CompName", "CompName", "CompId");
        }
        public SelectList GetCompany(int id)
        {
            var company = from Company in db.Companies
                            where Company.CompId == id
                          group Company by Company.CompName into unique
                          select unique.FirstOrDefault();
            return new SelectList(company, "CompName", "CompName", "CompId");
        }
        // GET: NewsPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsPost newsPost = db.NewsPosts.Find(id);
            if (newsPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyCompId = new SelectList(db.Companies, "CompId", "CompName", newsPost.CompanyCompId);
            return View(newsPost);
        }

        // POST: NewsPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NPId,Title,NewsDesc,NewsDate,IsVisible,CompanyCompId")] NewsPost newsPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyCompId = new SelectList(db.Companies, "CompId", "CompName", newsPost.CompanyCompId);
            return View(newsPost);
        }

        // GET: NewsPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsPost newsPost = db.NewsPosts.Find(id);
            if (newsPost == null)
            {
                return HttpNotFound();
            }
            return View(newsPost);
        }

        // POST: NewsPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsPost newsPost = db.NewsPosts.Find(id);
            db.NewsPosts.Remove(newsPost);
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
