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
    public class MediaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Media
        public ActionResult Index()
        {
            var model = new MediaGalleryModel();
            model.MediaList = db.MediaGalleryModels.Where(x => x.MediaType == MediaType.Picture).ToList();
            return View(model);
        }

        public ActionResult VideoGallery()
        {
            var model = new MediaGalleryModel();
            model.MediaList = db.MediaGalleryModels.Where(x=>x.MediaType == MediaType.Video).ToList();
            return View(model);
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
        public ActionResult Create([Bind(Include = "MediaId,MediaType,MediaName,MediaLocation,Caption,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] MediaGalleryModel mediaGalleryModel, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(files.FileName);
                    if(mediaGalleryModel.MediaType == MediaType.Picture)
                    {
                        var path = Path.Combine(Server.MapPath("~/Images/Gallery/Pictures"), fileName);
                        files.SaveAs(path);
                        var relativePath = "/Images/Gallery/Pictures/" + fileName;
                        mediaGalleryModel.MediaLocation = relativePath;
                    }
                    else if(mediaGalleryModel.MediaType == MediaType.Video)
                    {
                        var path = Path.Combine(Server.MapPath("~/Images/Gallery/Videos"), fileName);
                        files.SaveAs(path);
                        var relativePath = "/Images/Gallery/Videos/" + fileName;
                        mediaGalleryModel.MediaLocation = relativePath;
                    }
                    // store the file inside ~/App_Data/uploads folder
                    mediaGalleryModel.CreatedBy = User.Identity.Name;
                    mediaGalleryModel.CreatedDate = DateTime.Now;
                    db.MediaGalleryModels.Add(mediaGalleryModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.Error = "Error uploading your picture. Please try again.";
                    return View(mediaGalleryModel);
                }
                // redirect back to the index action to show the form once again

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
                mediaGalleryModel.UpdatedBy = User.Identity.Name;
                mediaGalleryModel.UpdatedDate = DateTime.Now;
                db.Entry(mediaGalleryModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mediaGalleryModel);
        }


        //// GET: Media/Delete/5
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
            var fullPath = Server.MapPath(mediaGalleryModel.MediaLocation);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
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
