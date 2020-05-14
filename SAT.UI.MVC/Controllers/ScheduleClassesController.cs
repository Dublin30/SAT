using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.DATA;

namespace SAT.UI.MVC.Controllers
{
    public class ScheduleClassesController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: ScheduleClasses
        public ActionResult Index()
        {
            var scheduleClasses = db.ScheduleClasses.Include(s => s.Cours).Include(s => s.ScheduledClassStatus);
            return View(scheduleClasses.ToList());
        }

        // GET: ScheduleClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleClass scheduleClass = db.ScheduleClasses.Find(id);
            if (scheduleClass == null)
            {
                return HttpNotFound();
            }
            return View(scheduleClass);
        }

        // GET: ScheduleClasses/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.SCSID = new SelectList(db.ScheduledClassStatuses, "SCSID", "StatusName");
            return View();
        }

        // POST: ScheduleClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ScheduleClassesID,CourseID,StarteDate,EndDate,InstructorName,SCSID,Location")] ScheduleClass scheduleClass)
        {
            if (ModelState.IsValid)
            {
                db.ScheduleClasses.Add(scheduleClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", scheduleClass.CourseID);
            ViewBag.SCSID = new SelectList(db.ScheduledClassStatuses, "SCSID", "StatusName", scheduleClass.SCSID);
            return View(scheduleClass);
        }

        // GET: ScheduleClasses/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleClass scheduleClass = db.ScheduleClasses.Find(id);
            if (scheduleClass == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", scheduleClass.CourseID);
            ViewBag.SCSID = new SelectList(db.ScheduledClassStatuses, "SCSID", "StatusName", scheduleClass.SCSID);
            return View(scheduleClass);
        }

        // POST: ScheduleClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ScheduleClassesID,CourseID,StarteDate,EndDate,InstructorName,SCSID,Location")] ScheduleClass scheduleClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduleClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", scheduleClass.CourseID);
            ViewBag.SCSID = new SelectList(db.ScheduledClassStatuses, "SCSID", "StatusName", scheduleClass.SCSID);
            return View(scheduleClass);
        }

        // GET: ScheduleClasses/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleClass scheduleClass = db.ScheduleClasses.Find(id);
            if (scheduleClass == null)
            {
                return HttpNotFound();
            }
            return View(scheduleClass);
        }

        // POST: ScheduleClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            ScheduleClass scheduleClass = db.ScheduleClasses.Find(id);
            db.ScheduleClasses.Remove(scheduleClass);
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
