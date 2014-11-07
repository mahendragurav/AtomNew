using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ATOMv0.Models
{
    public class DimFinancialCompaniesController : Controller
    {
        private FFCubeEntities db = new FFCubeEntities();

        // GET: DimFinancialCompanies
        public ActionResult Index()
        {
            return View(db.DimFinancialCompanies.ToList());
        }

        // GET: DimFinancialCompanies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFinancialCompany dimFinancialCompany = db.DimFinancialCompanies.Find(id);
            if (dimFinancialCompany == null)
            {
                return HttpNotFound();
            }
            return View(dimFinancialCompany);
        }

        // GET: DimFinancialCompanies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DimFinancialCompanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,FinancialCompanyName")] DimFinancialCompany dimFinancialCompany)
        {
            if (ModelState.IsValid)
            {
                db.DimFinancialCompanies.Add(dimFinancialCompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dimFinancialCompany);
        }

        // GET: DimFinancialCompanies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFinancialCompany dimFinancialCompany = db.DimFinancialCompanies.Find(id);
            if (dimFinancialCompany == null)
            {
                return HttpNotFound();
            }
            return View(dimFinancialCompany);
        }

        // POST: DimFinancialCompanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FinancialCompanyName")] DimFinancialCompany dimFinancialCompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimFinancialCompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dimFinancialCompany);
        }

        // GET: DimFinancialCompanies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFinancialCompany dimFinancialCompany = db.DimFinancialCompanies.Find(id);
            if (dimFinancialCompany == null)
            {
                return HttpNotFound();
            }
            return View(dimFinancialCompany);
        }

        // POST: DimFinancialCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimFinancialCompany dimFinancialCompany = db.DimFinancialCompanies.Find(id);
            db.DimFinancialCompanies.Remove(dimFinancialCompany);
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
