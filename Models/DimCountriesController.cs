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
    public class DimCountriesController : Controller
    {
        private FFCubeEntities db = new FFCubeEntities();

        // GET: DimCountries
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

            var dimcountries = db.DimCountries.Include(d => d.DimRegion);

            ViewBag.RegionList = db.DimCountries.Select(b => b.DimRegion.RegionName).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                dimcountries = dimcountries.Where(s => s.CountryName.ToUpper().Contains(searchString.ToUpper()) || s.DimRegion.RegionName.ToUpper().Contains(searchString.ToUpper()));

            }

            if (!String.IsNullOrEmpty(columnFilter) && !columnFilter.Equals("ALL"))
            {

                dimcountries = dimcountries.Where(s => s.DimRegion.RegionName.ToUpper().Contains(columnFilter.ToUpper()));

            }

            switch (sortOrder)
            {

                case "Country_Desc":
                    dimcountries = dimcountries.OrderByDescending(s => s.CountryName);
                    break;

                case "Region_Desc":
                    dimcountries = dimcountries.OrderByDescending(s => s.DimRegion.RegionName);
                    break;

                case "Region":
                    dimcountries = dimcountries.OrderBy(s => s.DimRegion.RegionName);
                    break;

                default:
                    dimcountries = dimcountries.OrderBy(s => s.CountryName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dimcountries.ToPagedList(pageNumber, pageSize));
        }

        // GET: DimCountries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimCountry dimCountry = db.DimCountries.Find(id);
            if (dimCountry == null)
            {
                return HttpNotFound();
            }
            return View(dimCountry);
        }

        // GET: DimCountries/Create
        public ActionResult Create()
        {
            ViewBag.KeyRegion = new SelectList(db.DimRegions, "id", "RegionName");
            return View();
        }

        // POST: DimCountries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CountryName,KeyRegion,IsActive")] DimCountry dimCountry)
        {
            if (ModelState.IsValid)
            {
                db.DimCountries.Add(dimCountry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyRegion = new SelectList(db.DimRegions, "id", "RegionName", dimCountry.KeyRegion);
            return View(dimCountry);
        }

        // GET: DimCountries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimCountry dimCountry = db.DimCountries.Find(id);
            if (dimCountry == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyRegion = new SelectList(db.DimRegions, "id", "RegionName", dimCountry.KeyRegion);
            return View(dimCountry);
        }

        // POST: DimCountries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CountryName,KeyRegion,IsActive")] DimCountry dimCountry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimCountry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyRegion = new SelectList(db.DimRegions, "id", "RegionName", dimCountry.KeyRegion);
            return View(dimCountry);
        }

        // GET: DimCountries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimCountry dimCountry = db.DimCountries.Find(id);
            if (dimCountry == null)
            {
                return HttpNotFound();
            }
            return View(dimCountry);
        }

        // POST: DimCountries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimCountry dimCountry = db.DimCountries.Find(id);
            db.DimCountries.Remove(dimCountry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: DimCountry/CountryList
        public ActionResult CountryList(String id)
        {
            String RegionName = id;

            var RegionID = from r in db.DimRegions
                           where r.RegionName.Equals(RegionName)
                           select r.id;

            var countries = from r in db.DimCountries
                            where r.KeyRegion == RegionID.FirstOrDefault()
                            select r.CountryName;

            if (HttpContext.Request.IsAjaxRequest())

                return Json(new SelectList(
                                countries.ToList())
                           , JsonRequestBehavior.AllowGet);

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
