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
    public class FeedbackController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Feedback
        public ActionResult Index()
        {
            return View(db.FeedbackModels.ToList());
        }

        // GET: Feedback/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackModel feedbackModel = db.FeedbackModels.Find(id);
            if (feedbackModel == null)
            {
                return HttpNotFound();
            }
            return View(feedbackModel);
        }

        // GET: Feedback/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feedback/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeedbackId,FeedbackTitle,FeedbackDescription,FeedbackDate,Email,Name,PhoneNumber,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] FeedbackModel feedbackModel)
        {
            if (ModelState.IsValid)
            {
                feedbackModel.CreatedDate = DateTime.Now;
                feedbackModel.CreatedBy = User.Identity.Name;
                db.FeedbackModels.Add(feedbackModel);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(feedbackModel);
        }

        // GET: Feedback/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackModel feedbackModel = db.FeedbackModels.Find(id);
            if (feedbackModel == null)
            {
                return HttpNotFound();
            }
            return View(feedbackModel);
        }

        // POST: Feedback/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeedbackId,FeedbackTitle,FeedbackDescription,FeedbackDate,Email,Name,PhoneNumber,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] FeedbackModel feedbackModel)
        {
            if (ModelState.IsValid)
            {
                feedbackModel.UpdatedDate = DateTime.Now;
                feedbackModel.UpdatedBy = User.Identity.Name;
                db.Entry(feedbackModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feedbackModel);
        }

        // GET: Feedback/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackModel feedbackModel = db.FeedbackModels.Find(id);
            if (feedbackModel == null)
            {
                return HttpNotFound();
            }
            return View(feedbackModel);
        }

        // POST: Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeedbackModel feedbackModel = db.FeedbackModels.Find(id);
            db.FeedbackModels.Remove(feedbackModel);
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
