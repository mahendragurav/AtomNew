using System;
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
    public class DimRegionsController : Controller
    {
        private FFCubeEntities db = new FFCubeEntities();

        // GET: DimRegions
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

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

            var dimRegions = db.DimRegions.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                dimRegions = dimRegions.Where(s => s.RegionName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Region_Desc":
                    dimRegions = dimRegions.OrderByDescending(s => s.RegionName);
                    break;

                default:
                    dimRegions = dimRegions.OrderBy(s => s.RegionName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dimRegions.ToPagedList(pageNumber, pageSize));
        }

        // GET: DimRegions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimRegion dimRegion = db.DimRegions.Find(id);
            if (dimRegion == null)
            {
                return HttpNotFound();
            }
            return View(dimRegion);
        }

        // GET: DimRegions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DimRegions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,RegionName")] DimRegion dimRegion)
        {
            if (ModelState.IsValid)
            {
                db.DimRegions.Add(dimRegion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dimRegion);
        }

        // GET: DimRegions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimRegion dimRegion = db.DimRegions.Find(id);
            if (dimRegion == null)
            {
                return HttpNotFound();
            }
            return View(dimRegion);
        }

        // POST: DimRegions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,RegionName")] DimRegion dimRegion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimRegion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dimRegion);
        }

        // GET: DimRegions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimRegion dimRegion = db.DimRegions.Find(id);
            if (dimRegion == null)
            {
                return HttpNotFound();
            }
            return View(dimRegion);
        }

        // POST: DimRegions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimRegion dimRegion = db.DimRegions.Find(id);
            db.DimRegions.Remove(dimRegion);
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
