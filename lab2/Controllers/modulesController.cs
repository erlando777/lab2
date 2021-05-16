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
    public class modulesController : Controller
    {
        private LabaratorinisDBEntities1 db = new LabaratorinisDBEntities1();

        // GET: modules
        public ActionResult Index()
        {
            return View(db.modules.ToList());
        }

        // GET: modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            module module = db.modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // GET: modules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModuleID,ModuleCode,ModuleTitle")] module module)
        {
            if (ModelState.IsValid)
            {
                if (db.modules.Any(s => s.ModuleCode.Equals(module.ModuleCode)))
                {
                    ViewBag.Error = "Modulis su tokiu kodu jau yra pridėtas.";
                    return View(module); 
                }
                else
                {
                    db.modules.Add(module);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }   
            }
            return View(module);
        }

        // GET: modules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            module module = db.modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModuleID,ModuleCode,ModuleTitle")] module module)
        {
            if (ModelState.IsValid)
            {
                if (db.modules.Any(s => s.ModuleCode.Equals(module.ModuleCode)))
                {
                    ViewBag.Error = "Modulis su tokiu kodu jau yra pridėtas.";
                    return View(module);
                }
                else
                {
                    db.Entry(module).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            return View(module);
        }

        // GET: modules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            module module = db.modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //pirma istrinam moduliu sarysius su studentais
            while (true)
            {
                studentModule studentModule = db.studentModules.Where(s => s.ModuleID == id).FirstOrDefault();
                if(studentModule != null)
                {
                    db.studentModules.Remove(studentModule);
                    db.SaveChanges();
                }
                else
                {
                    break;
                }
            }
            //tada istrinam modulio paskaitas
            while (true)
            {
                lecture lecture = db.lectures.Where(s => s.ModuleID == id).FirstOrDefault();
                if(lecture != null)
                {
                    db.lectures.Remove(lecture);
                    db.SaveChanges();
                }
                else
                {
                    break;
                }
            } 
            module module = db.modules.Find(id);        
            db.modules.Remove(module);
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
