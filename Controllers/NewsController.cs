using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mahamesh.Models;

namespace Mahamesh.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        public ActionResult Index()
        {
            return View(db.NewsModels.ToList());
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsModel newsModel = db.NewsModels.Find(id);
            if (newsModel == null)
            {
                return HttpNotFound();
            }
            return View(newsModel);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsId,NewsTitle,NewsDescription,NewsDate,ImageLocation,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] NewsModel newsModel, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(files.FileName);

                    var path = Path.Combine(Server.MapPath("~/Documents/News"), fileName);
                    files.SaveAs(path);
                    var relativePath = "/Documents/News/" + fileName;
                    newsModel.DocumentName = fileName;
                    newsModel.NewsDocument = relativePath;
                    newsModel.CreatedBy = User.Identity.Name;
                    newsModel.CreatedDate = DateTime.Now;

                    db.NewsModels.Add(newsModel);
                    db.SaveChanges();
                    return RedirectToAction("AdminPanel", "Menu");
                }
              
            }

            return View(newsModel);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsModel newsModel = db.NewsModels.Find(id);
            if (newsModel == null)
            {
                return HttpNotFound();
            }
            return View(newsModel);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,NewsTitle,NewsDescription,NewsDate,ImageLocation,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] NewsModel newsModel, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(files.FileName);

                    var path = Path.Combine(Server.MapPath("~/Documents/News"), fileName);
                    files.SaveAs(path);
                    var relativePath = "/Documents/News/" + fileName;
                    newsModel.DocumentName = fileName;
                    newsModel.NewsDocument = relativePath;
                    newsModel.UpdatedBy = User.Identity.Name;
                    newsModel.UpdatedDate = DateTime.Now;
                    db.Entry(newsModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AdminPanel", "Menu");
                }
                else
                {
                    newsModel.UpdatedBy = User.Identity.Name;
                    newsModel.UpdatedDate = DateTime.Now;
                    db.Entry(newsModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AdminPanel", "Menu");
                }
            }
            return View(newsModel);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsModel newsModel = db.NewsModels.Find(id);
            if (newsModel == null)
            {
                return HttpNotFound();
            }
            return View(newsModel);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsModel newsModel = db.NewsModels.Find(id);
            db.NewsModels.Remove(newsModel);
            db.SaveChanges();
            return RedirectToAction("AdminPanel", "Menu");
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
