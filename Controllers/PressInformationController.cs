using Mahamesh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mahamesh.Controllers
{
    public class PressInformationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PressInformation
        public ActionResult Index()
        {
            return View(db.PressInformationModels.ToList());
        }

        public ActionResult PressIndex()
        {
            var model = new PressInformationModel();
            model.PressList = new List<PressInformationModel>();
            model.PressList = db.PressInformationModels.ToList();
            return PartialView(model);
        }

        // GET: PressInformation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PressInformationModel pressInformationModel = db.PressInformationModels.Find(id);
            if (pressInformationModel == null)
            {
                return HttpNotFound();
            }
            return View(pressInformationModel);
        }

        // GET: PressInformation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PressInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( PressInformationModel pressInformationModel, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(files.FileName);

                    var path = Path.Combine(Server.MapPath("~/Documents/Press"), fileName);
                    files.SaveAs(path);
                    var relativePath = "/Documents/Press/" + fileName;
                    pressInformationModel.DocumentName = fileName;
                    pressInformationModel.InformationDocument = relativePath;
                    pressInformationModel.CreatedBy = User.Identity.Name;
                    pressInformationModel.CreatedDate = DateTime.Now;
                    db.PressInformationModels.Add(pressInformationModel);
                    db.SaveChanges();
                    return RedirectToAction("AdminPanel", "Menu");
                }


            }

            return View(pressInformationModel);
        }

        // GET: PressInformation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PressInformationModel pressInformationModel = db.PressInformationModels.Find(id);
            if (pressInformationModel == null)
            {
                return HttpNotFound();
            }
            return View(pressInformationModel);
        }

        // POST: PressInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InformationId,InformationTitle,InformationDescription,InformationDate,InformationDocument,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] PressInformationModel pressInformationModel, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(files.FileName);

                    var path = Path.Combine(Server.MapPath("~/Documents/Press"), fileName);
                    files.SaveAs(path);
                    var relativePath = "/Documents/Press/" + fileName;
                    pressInformationModel.DocumentName = fileName;
                    pressInformationModel.InformationDocument = relativePath;
                    pressInformationModel.UpdatedBy = User.Identity.Name;
                    pressInformationModel.UpdatedDate = DateTime.Now;
                    db.Entry(pressInformationModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AdminPanel", "Menu");
                }
                else
                {
                    pressInformationModel.UpdatedBy = User.Identity.Name;
                    pressInformationModel.UpdatedDate = DateTime.Now;
                    db.Entry(pressInformationModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AdminPanel", "Menu");
                }
            }
            return View(pressInformationModel);
        }

        // GET: PressInformation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PressInformationModel pressInformationModel = db.PressInformationModels.Find(id);
            if (pressInformationModel == null)
            {
                return HttpNotFound();
            }
            return View(pressInformationModel);
        }

        // POST: PressInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PressInformationModel pressInformationModel = db.PressInformationModels.Find(id);
            db.PressInformationModels.Remove(pressInformationModel);
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
