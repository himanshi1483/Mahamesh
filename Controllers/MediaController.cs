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
            var folders = new MediaFolders();
            model.FolderList = db.MediaFolders.ToList();
           // model.MediaList = db.MediaGalleryModels.Where(x => x.MediaType == MediaType.Picture).ToList();
            return View(model);
        }

        public ActionResult VideoGallery(string folder)
        {
            var model = new MediaGalleryModel();
            model.MediaList = db.MediaGalleryModels.Where(x=>x.MediaType == MediaType.Videos && x.MediaFolder == folder).ToList();
            return View(model);
        }

        public ActionResult PictureGallery(string folder)
        {
            var model = new MediaGalleryModel();
            model.MediaList = db.MediaGalleryModels.Where(x => x.MediaType == MediaType.Pictures && x.MediaFolder == folder).ToList();
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
            var folders = db.MediaFolders.ToList();
            ViewBag.Folders = new SelectList(folders, "FolderName", "FolderName");
            return View();
        }

        // POST: Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MediaGalleryModel mediaGalleryModel, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                if ((!db.MediaFolders.Select(x=>x.FolderName).ToList().Contains(mediaGalleryModel.MediaFolderNew)) && mediaGalleryModel.MediaFolder == null)
                {
                    MediaFolders folders = new MediaFolders();
                    folders.FolderName = mediaGalleryModel.MediaFolderNew;
                    folders.MediaType = mediaGalleryModel.MediaType.ToString();
                    db.MediaFolders.Add(folders);
                    db.SaveChanges();
                    mediaGalleryModel.MediaFolder = mediaGalleryModel.MediaFolderNew;

                }
                if (files != null && files.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(files.FileName);
                    if(mediaGalleryModel.MediaType == MediaType.Pictures)
                    {
                        var path = Path.Combine(Server.MapPath("~/Images/Gallery/Pictures"), fileName);
                        files.SaveAs(path);
                        var relativePath = "/Images/Gallery/Pictures/" + fileName;
                        mediaGalleryModel.MediaLocation = relativePath;
                    }
                    else if(mediaGalleryModel.MediaType == MediaType.Videos)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew(AdminPanelViewModel model, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                if ((!db.MediaFolders.Select(x => x.FolderName).ToList().Contains(model.MediaGallery.MediaFolderNew)))
                {
                    MediaFolders folders = new MediaFolders();
                    folders.FolderName = model.MediaGallery.MediaFolderNew;
                    folders.MediaType = model.MediaGallery.MediaType.ToString();
                    db.MediaFolders.Add(folders);
                    db.SaveChanges();

                }
                if (files != null && files.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(files.FileName);
                    if (model.MediaGallery.MediaType == MediaType.Pictures)
                    {
                        var path = Path.Combine(Server.MapPath("~/Images/Gallery/Pictures"), fileName);
                        files.SaveAs(path);
                        var relativePath = "/Images/Gallery/Pictures/" + fileName;
                        model.MediaGallery.MediaLocation = relativePath;
                    }
                    else if (model.MediaGallery.MediaType == MediaType.Videos)
                    {
                        var path = Path.Combine(Server.MapPath("~/Images/Gallery/Videos"), fileName);
                        files.SaveAs(path);
                        var relativePath = "/Images/Gallery/Videos/" + fileName;
                        model.MediaGallery.MediaLocation = relativePath;
                    }
                    // store the file inside ~/App_Data/uploads folder
                    model.MediaGallery.CreatedBy = User.Identity.Name;
                    model.MediaGallery.CreatedDate = DateTime.Now;
                    db.MediaGalleryModels.Add(model.MediaGallery);
                    db.SaveChanges();
                    return RedirectToAction("AdminPanel", "Menu");

                }
                else
                {
                    ViewBag.Error = "Error uploading your picture. Please try again.";
                    return View(model.MediaGallery);
                }
                // redirect back to the index action to show the form once again

            }
            return View(model.MediaGallery);

        }


        // GET: Media/Edit/5
        public ActionResult Edit(int? id)
        {
            var folders = db.MediaFolders.ToList();
            ViewBag.Folders = new SelectList(folders, "FolderName", "FolderName");

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
