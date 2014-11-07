﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ATOMv0.Models
{
    public class DimModulesController : Controller
    {
        private FFCubeEntities db = new FFCubeEntities();

        // GET: DimModules
        public ActionResult Index(string sortOrder, string currentFilter, string columnFilter, string searchString, int? page)
        {

            ViewBag.ColumnFilter = (columnFilter != null) ? columnFilter : "ALL";

            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var dimModules = db.DimModules.Include(d => d.DimBuilding);

            ViewBag.BuildingList = db.DimModules.Select(b => b.DimBuilding.BuildingName).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                dimModules = dimModules.Where(s => s.ModuleName.ToUpper().Contains(searchString.ToUpper()) || s.DimBuilding.BuildingName.ToUpper().Contains(searchString.ToUpper()));

            }

            if (!String.IsNullOrEmpty(columnFilter) && !columnFilter.Equals("ALL"))
            {

                dimModules = dimModules.Where(s => s.DimBuilding.BuildingName.ToUpper().Contains(columnFilter.ToUpper()));

            }

            switch (sortOrder)
            {

                case "Module_Desc":
                    dimModules = dimModules.OrderByDescending(s => s.ModuleName);
                    break;

                case "Building_Desc":
                    dimModules = dimModules.OrderByDescending(s => s.DimBuilding.BuildingName);
                    break;

                case "Building":
                    dimModules = dimModules.OrderBy(s => s.DimBuilding.BuildingName);
                    break;

                default:
                    dimModules = dimModules.OrderBy(s => s.ModuleName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dimModules.ToPagedList(pageNumber, pageSize));
        }

        // GET: DimModules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimModule dimModule = db.DimModules.Find(id);
            if (dimModule == null)
            {
                return HttpNotFound();
            }
            return View(dimModule);
        }

        // GET: DimModules/Create
        public ActionResult Create()
        {
            ViewBag.KeyBuilding = new SelectList(db.DimBuildings, "id", "BuildingName");
            return View();
        }

        // POST: DimModules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ModuleName,KeyBuilding")] DimModule dimModule)
        {
            if (ModelState.IsValid)
            {
                db.DimModules.Add(dimModule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyBuilding = new SelectList(db.DimBuildings, "id", "BuildingName", dimModule.KeyBuilding);
            return View(dimModule);
        }

        // GET: DimModules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimModule dimModule = db.DimModules.Find(id);
            if (dimModule == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyBuilding = new SelectList(db.DimBuildings, "id", "BuildingName", dimModule.KeyBuilding);
            return View(dimModule);
        }

        // POST: DimModules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ModuleName,KeyBuilding")] DimModule dimModule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimModule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyBuilding = new SelectList(db.DimBuildings, "id", "BuildingName", dimModule.KeyBuilding);
            return View(dimModule);
        }

        // GET: DimModules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimModule dimModule = db.DimModules.Find(id);
            if (dimModule == null)
            {
                return HttpNotFound();
            }
            return View(dimModule);
        }

        // POST: DimModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimModule dimModule = db.DimModules.Find(id);
            db.DimModules.Remove(dimModule);
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

        // GET: /Modules/ModulesList
        public ActionResult ModulesList(String id)
        {

            IEnumerable<DimModule> modules = new List<DimModule>();

            String BuildingName = id;

            var BuildingID = from r in db.DimBuildings where r.BuildingName.Equals(BuildingName) select r.id;

            var query = db.DimModules.Where(x => x.KeyBuilding == BuildingID.FirstOrDefault()).ToList();

            modules = query.Select(x =>
                        new DimModule()
                        {
                            ModuleName = x.ModuleName,
                        });

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(modules, JsonRequestBehavior.AllowGet);
            }


            return RedirectToAction("Index");
        }
    }
}
