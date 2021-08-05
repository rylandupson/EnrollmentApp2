using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnrollmentApp.DATA.EF;
using EnrollmentApp.UI.Utilities;

namespace EnrollmentApp.UI.Controllers
{
    public class StudentsController : Controller
    {
        private EnrollmentEntities db = new EnrollmentEntities();

        // GET: Students
        [Authorize(Roles = "Admin, Scheduling")]
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        [Authorize(Roles = "Admin, Scheduling")]
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
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,Zip,Phone,Email,PhotoURL,SSID")] Student student, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "default.png";

                if (photo != null)
                {
                    file = photo.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));

                    string[] goodExts = { ".jpeg", ".jfif", ".jpg", ".png", ".gif" };

                    //Check that the uploaded file is in our list of acceptable exts & file sizw <= 4mb max from ASP.NET
                    if (goodExts.Contains(ext.ToLower()) && photo.ContentLength <= 4194304)
                    {
                        //Create a new file name (using a GUID)
                        file = Guid.NewGuid() + ext;

                        #region Resize Image
                        string savePath = Server.MapPath("~/Content/images/StudentImages/");

                        Image convertedImage = Image.FromStream(photo.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion
                    }
                    student.PhotoURL = file;
                }
                #endregion

                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,Zip,Phone,Email,PhotoURL,SSID")] Student student, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                //Check to see if a new file has been uploaded. If not the HiddenFor() in the view will maintain the original value
                string file = "default.png";

                if (photo != null)
                {
                    //get the name
                    file = photo.FileName;

                    //capture the extension
                    string ext = file.Substring(file.LastIndexOf('.'));

                    //Create a "whitelisted" array of acceptable exts
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //Check the ext and file size
                    if (goodExts.Contains(ext.ToLower()) && photo.ContentLength < 4194304)
                    {
                        //Create a new file name
                        file = Guid.NewGuid() + ext;

                        //Resize the image
                        string savePath = Server.MapPath("~/Content/images/StudentImages/");

                        Image convertedImage = Image.FromStream(photo.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        if (student.PhotoURL != null && student.PhotoURL != "default.png")
                        {
                            string path = Server.MapPath("~/Content/images/StudentImages/");
                            ImageUtility.Delete(path, student.PhotoURL);
                        }
                        //update the property of the book object
                        student.PhotoURL = file;
                    }
                }
                #endregion

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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

            string path = Server.MapPath("~/Content/images/StudentImages/");
            ImageUtility.Delete(path, student.PhotoURL);

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
    }
}
