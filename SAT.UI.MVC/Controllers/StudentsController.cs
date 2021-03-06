﻿using System;
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
    public class StudentsController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Classman).Include(s => s.StudentStatus);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ClassmanID = new SelectList(db.Classmans, "ClassmanID", "ClassmanName");
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,DOB,ClassmanID,GraduationYear,StreetAddress,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase PhotoUrl)
        {
            if (ModelState.IsValid)
            {
                student.PhotoUrl = UploadImage(PhotoUrl);

                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassmanID = new SelectList(db.Classmans, "ClassmanID", "ClassmanName", student.ClassmanID);
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassmanID = new SelectList(db.Classmans, "ClassmanID", "ClassmanName", student.ClassmanID);
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,DOB,ClassmanID,GraduationYear,StreetAddress,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase PhotoUrl)
        {
            if (ModelState.IsValid)
            {
                if (PhotoUrl != null)
                {
                    string oldImage = "~/UploadedImages/" + student.PhotoUrl;
                    student.PhotoUrl = UploadImage(PhotoUrl);
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassmanID = new SelectList(db.Classmans, "ClassmanID", "ClassmanName", student.ClassmanID);
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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

        private string UploadImage(HttpPostedFileBase PhotoUrl)
        {
            string imageName = "NoImage.png";

            if (PhotoUrl != null)
            {
                imageName = PhotoUrl.FileName;
                // imageName is FastAndFurious.png
                //then extension, after substring is ".jpeg"
                string extension = imageName.Substring(imageName.LastIndexOf("."));
                string[] goodExts = new string[] { ".jpeg", ".jpg", ".png", ".gif" };

                if (goodExts.Contains(extension))
                {
                    imageName = Guid.NewGuid() + extension;
                    PhotoUrl.SaveAs(Server.MapPath("~/UploadedImages/" + imageName));
                }
                else
                {
                    imageName = "NoImage.png";

                }
            }
            return imageName;
        }
    }
}
