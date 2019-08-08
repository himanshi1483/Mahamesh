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
    public class MediaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Media
        public ActionResult Index()
        {
            return View(db.MediaGalleryModels.ToList());
        }

        // GET: Media/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaGalleryModel mediaGalleryModel = db.MediaGalleryModels.Find(id);
            if (mediaGalleryModel == null)
            {
                return HttpNotFound();
            }
            return View(mediaGalleryModel);
        }

        // GET: Media/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MediaId,MediaType,MediaName,MediaLocation,Caption,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] MediaGalleryModel mediaGalleryModel)
        {
            if (ModelState.IsValid)
            {
                db.MediaGalleryModels.Add(mediaGalleryModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mediaGalleryModel);
        }

        // GET: Media/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaGalleryModel mediaGalleryModel = db.MediaGalleryModels.Find(id);
            if (mediaGalleryModel == null)
            {
                return HttpNotFound();
            }
            return View(mediaGalleryModel);
        }

        // POST: Media/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MediaId,MediaType,MediaName,MediaLocation,Caption,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] MediaGalleryModel mediaGalleryModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mediaGalleryModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mediaGalleryModel);
        }

        // GET: Media/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaGalleryModel mediaGalleryModel = db.MediaGalleryModels.Find(id);
            if (mediaGalleryModel == null)
            {
                return HttpNotFound();
            }
            return View(mediaGalleryModel);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MediaGalleryModel mediaGalleryModel = db.MediaGalleryModels.Find(id);
            db.MediaGalleryModels.Remove(mediaGalleryModel);
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
