using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lab2.Models;

namespace lab2.Controllers
{
    public class studentModulesController : Controller
    {
        private LabaratorinisDBEntities1 db = new LabaratorinisDBEntities1();

        // GET: studentModules
        public ActionResult Index()
        {
            var studentModules = db.studentModules.Include(s => s.module).Include(s => s.student);
            return View(studentModules.ToList());
        }

        // GET: studentModules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentModule studentModule = db.studentModules.Find(id);
            if (studentModule == null)
            {
                return HttpNotFound();
            }
            return View(studentModule);
        }

        // GET: studentModules/Create
        public ActionResult Create()
        {
            ViewBag.ModuleID = new SelectList(db.modules, "ModuleID", "ModuleTitle");
            ViewBag.StudentID = new SelectList(db.students, "StudentID", "StudentSurname");
            return View();
        }

        // POST: studentModules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConnectionID,StudentID,ModuleID")] studentModule studentModule)
        {
            if (ModelState.IsValid)
            {
                if (db.studentModules.Any(s => s.ModuleID.Equals(studentModule.ModuleID) && s.StudentID.Equals(studentModule.StudentID)))
                {
                    ViewBag.ModuleID = new SelectList(db.modules, "ModuleID", "ModuleTitle", studentModule.ModuleID);
                    ViewBag.StudentID = new SelectList(db.students, "StudentID", "StudentSurname", studentModule.StudentID);
                    ViewBag.Error = "Toks modulis šiam studentui jau yra pridėtas.";
                    return View(studentModule);
                }
                else
                {
                    db.studentModules.Add(studentModule);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ModuleID = new SelectList(db.modules, "ModuleID", "ModuleTitle", studentModule.ModuleID);
            ViewBag.StudentID = new SelectList(db.students, "StudentID", "StudentSurname", studentModule.StudentID);

            return View(studentModule);
        }

        // GET: studentModules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentModule studentModule = db.studentModules.Find(id);
            if (studentModule == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleID = new SelectList(db.modules, "ModuleID", "ModuleTitle", studentModule.ModuleID);
            ViewBag.StudentID = new SelectList(db.students, "StudentID", "StudentSurname", studentModule.StudentID);
            return View(studentModule);
        }

        // POST: studentModules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConnectionID,StudentID,ModuleID")] studentModule studentModule)
        {
            if (ModelState.IsValid)
            {
                if (db.studentModules.Any(s => s.ModuleID.Equals(studentModule.ModuleID) && s.StudentID.Equals(studentModule.StudentID)))
                {
                    ViewBag.ModuleID = new SelectList(db.modules, "ModuleID", "ModuleTitle", studentModule.ModuleID);
                    ViewBag.StudentID = new SelectList(db.students, "StudentID", "StudentSurname", studentModule.StudentID);
                    ViewBag.Error = "Toks modulis šiam studentui jau yra pridėtas.";
                    return View(studentModule);
                }
                else
                {
                    db.Entry(studentModule).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ModuleID = new SelectList(db.modules, "ModuleID", "ModuleTitle", studentModule.ModuleID);
            ViewBag.StudentID = new SelectList(db.students, "StudentID", "StudentSurname", studentModule.StudentID);
            return View(studentModule);
        }

        // GET: studentModules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentModule studentModule = db.studentModules.Find(id);
            if (studentModule == null)
            {
                return HttpNotFound();
            }
            return View(studentModule);
        }

        // POST: studentModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            studentModule studentModule = db.studentModules.Find(id);
            db.studentModules.Remove(studentModule);
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
