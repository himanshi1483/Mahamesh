using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mahamesh.Models;

namespace Mahamesh.Controllers
{
    public class MediaFoldersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MediaFolders
        public ActionResult Index()
        {
            return View(db.MediaFolders.ToList());
        }

        // GET: MediaFolders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaFolders mediaFolders = db.MediaFolders.Find(id);
            if (mediaFolders == null)
            {
                return HttpNotFound();
            }
            return View(mediaFolders);
        }

        // GET: MediaFolders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MediaFolders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FolderId,FolderName,MediaType")] MediaFolders mediaFolders)
        {
            if (ModelState.IsValid)
            {
                db.MediaFolders.Add(mediaFolders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mediaFolders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew([Bind(Include = "FolderId,FolderName,MediaType")] AdminPanelViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.MediaFolders.Add(model.Folder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model.Folder);
        }

        // GET: MediaFolders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaFolders mediaFolders = db.MediaFolders.Find(id);
            if (mediaFolders == null)
            {
                return HttpNotFound();
            }
            return View(mediaFolders);
        }

        // POST: MediaFolders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FolderId,FolderName,MediaType")] MediaFolders mediaFolders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mediaFolders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mediaFolders);
        }

        // GET: MediaFolders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaFolders mediaFolders = db.MediaFolders.Find(id);
            if (mediaFolders == null)
            {
                return HttpNotFound();
            }
            return View(mediaFolders);
        }

        // POST: MediaFolders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MediaFolders mediaFolders = db.MediaFolders.Find(id);
            db.MediaFolders.Remove(mediaFolders);
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
