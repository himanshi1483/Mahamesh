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
    public class TenderInfoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TenderInfo
        public ActionResult Index()
        {
            return View(db.TenderModels.ToList());
        }

        public PartialViewResult _FileIndex()
        {
            var model = new AdminPanelViewModel();
            model.TenderList = new List<TenderModel>();
            model.TenderList = db.TenderModels.ToList();
            return PartialView("_FileIndex",model);
        }

        // GET: TenderInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TenderModel tenderModel = db.TenderModels.Find(id);
            if (tenderModel == null)
            {
                return HttpNotFound();
            }
            return View(tenderModel);
        }

        // GET: TenderInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TenderInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenderId,TenderTitle,TenderDescription,TenderDate,TenderDocument,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] TenderModel tenderModel, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(files.FileName);
                  
                    var path = Path.Combine(Server.MapPath("~/Documents/Tenders"), fileName);
                    files.SaveAs(path);
                    var relativePath = "/Documents/Tenders/" + fileName;
                    tenderModel.TenderDocument = relativePath;
                    tenderModel.DocumentName = fileName;
                    tenderModel.CreatedBy = User.Identity.Name;
                    tenderModel.CreatedDate = DateTime.Now;
                    db.TenderModels.Add(tenderModel);
                    db.SaveChanges();
                    return RedirectToAction("AdminPanel", "Menu");
                }
             
            }

            return View(tenderModel);
        }

        // GET: TenderInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TenderModel tenderModel = db.TenderModels.Find(id);
            if (tenderModel == null)
            {
                return HttpNotFound();
            }
            return View(tenderModel);
        }

        // POST: TenderInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TenderId,TenderTitle,TenderDescription,TenderDate,TenderDocument,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] TenderModel tenderModel, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(files.FileName);

                    var path = Path.Combine(Server.MapPath("~/Documents/Tenders"), fileName);
                    files.SaveAs(path);
                    var relativePath = "/Documents/Tenders/" + fileName;
                    tenderModel.TenderDocument = relativePath;
                    tenderModel.DocumentName = fileName;
                    tenderModel.UpdatedBy = User.Identity.Name;
                    tenderModel.UpdatedDate = DateTime.Now;

                    db.Entry(tenderModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AdminPanel", "Menu");
                }
                else
                {
                    tenderModel.UpdatedBy = User.Identity.Name;
                    tenderModel.UpdatedDate = DateTime.Now;

                    db.Entry(tenderModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AdminPanel", "Menu");
                }
            }
            return View(tenderModel);
        }

        // GET: TenderInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TenderModel tenderModel = db.TenderModels.Find(id);
            if (tenderModel == null)
            {
                return HttpNotFound();
            }
            return View(tenderModel);
        }

        // POST: TenderInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TenderModel tenderModel = db.TenderModels.Find(id);
            db.TenderModels.Remove(tenderModel);
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
