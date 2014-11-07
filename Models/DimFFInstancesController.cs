using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net.Http;

namespace ATOMv0.Models
{
    public class DimFFInstancesController : Controller
    {
        private FFCubeEntities db = new FFCubeEntities();

        // GET: DimFFInstances
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var dimffinstances = db.DimFFInstances.Include(d => d.DimModule);

            if (!String.IsNullOrEmpty(searchString))
            {

                dimffinstances = dimffinstances.Where(s => s.HostName.ToUpper().Contains(searchString.ToUpper()) || s.DatabaseName.ToUpper().Contains(searchString.ToUpper()) || s.DataSourceName.ToUpper().Contains(searchString.ToUpper()) || s.ProjectName.ToUpper().Contains(searchString.ToUpper()) || s.DataFilePrefix.ToUpper().Contains(searchString.ToUpper()) || s.DimModule.ModuleName.ToUpper().Contains(searchString.ToUpper()) || s.SiteName.ToUpper().Contains(searchString.ToUpper()) || s.QAContactName.ToUpper().Contains(searchString.ToUpper()) || s.ITContactName.ToUpper().Contains(searchString.ToUpper()) || s.UserName.ToUpper().Contains(searchString.ToUpper()) || s.BaanCoNo.ToUpper().Contains(searchString.ToUpper()) || s.DimModule.ModuleName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Hostname": dimffinstances = dimffinstances.OrderBy(s => s.HostName); break;
                case "Hostname_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.HostName); break;
                case "Database": dimffinstances = dimffinstances.OrderBy(s => s.DatabaseName); break;
                case "Database_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.DatabaseName); break;
                case "DataSourceName": dimffinstances = dimffinstances.OrderBy(s => s.DataSourceName); break;
                case "DataSourceName_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.DataSourceName); break;
                case "ProjectName": dimffinstances = dimffinstances.OrderBy(s => s.ProjectName); break;
                case "ProjectName_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.ProjectName); break;
                case "SiteName": dimffinstances = dimffinstances.OrderBy(s => s.SiteName); break;
                case "SiteName_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.SiteName); break;
                case "QAContactName": dimffinstances = dimffinstances.OrderBy(s => s.QAContactName); break;
                case "QAContactName_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.QAContactName); break;
                case "ITContactName": dimffinstances = dimffinstances.OrderBy(s => s.ITContactName); break;
                case "ITContactName_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.ITContactName); break;
                case "UserName": dimffinstances = dimffinstances.OrderBy(s => s.UserName); break;
                case "UserName_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.UserName); break;
                case "BaanCoNo": dimffinstances = dimffinstances.OrderBy(s => s.BaanCoNo); break;
                case "BaanCoNo_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.BaanCoNo); break;
                case "Module": dimffinstances = dimffinstances.OrderBy(s => s.DimModule.ModuleName); break;
                case "Module_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.DimModule.ModuleName); break;

                default:
                    dimffinstances = dimffinstances.OrderBy(s => s.HostName);
                    break;
            }


            //return View(dimffinstances.ToList());
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dimffinstances.ToPagedList(pageNumber, pageSize));

        }

        // GET: DimFFInstances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFFInstance dimFFInstance = db.DimFFInstances.Find(id);
            if (dimFFInstance == null)
            {
                return HttpNotFound();
            }
            return View(dimFFInstance);
        }

        // GET: DimFFInstances/Create
        public ActionResult Create()
        {
            ViewBag.KeyModule = new SelectList(db.DimModules, "id", "ModuleName");
            return View();
        }

        // POST: DimFFInstances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,DataSourceName,HostName,DatabaseName,UserName,Password,ProjectName,DataFilePrefix,ReplicationDelayMinute,IsActive,KeyModule,ITContactName,ITPhone,ITEmail,QAContactName,QAPhone,QAEmail,SiteName,BaanCoNo")] DimFFInstance dimFFInstance)
        {
            if (ModelState.IsValid)
            {
                db.DimFFInstances.Add(dimFFInstance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyModule = new SelectList(db.DimModules, "id", "ModuleName", dimFFInstance.KeyModule);
            return View(dimFFInstance);
        }

        // GET: DimFFInstances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFFInstance dimFFInstance = db.DimFFInstances.Find(id);
            if (dimFFInstance == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyModule = new SelectList(db.DimModules, "id", "ModuleName", dimFFInstance.KeyModule);
            return View(dimFFInstance);
        }

        // POST: DimFFInstances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,DataSourceName,HostName,DatabaseName,UserName,Password,ProjectName,DataFilePrefix,ReplicationDelayMinute,IsActive,KeyModule,ITContactName,ITPhone,ITEmail,QAContactName,QAPhone,QAEmail,SiteName,BaanCoNo")] DimFFInstance dimFFInstance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimFFInstance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyModule = new SelectList(db.DimModules, "id", "ModuleName", dimFFInstance.KeyModule);
            return View(dimFFInstance);
        }

        // GET: DimFFInstances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFFInstance dimFFInstance = db.DimFFInstances.Find(id);
            if (dimFFInstance == null)
            {
                return HttpNotFound();
            }
            return View(dimFFInstance);
        }

        // POST: DimFFInstances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimFFInstance dimFFInstance = db.DimFFInstances.Find(id);
            db.DimFFInstances.Remove(dimFFInstance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // POST: /DimFFInstance/Update/
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(DimFFInstanceAJAXModel ffInstanceAJAXModel)
        {
            var Module = db.DimModules.Where(x => x.DimBuilding.BuildingName.Equals(ffInstanceAJAXModel.buildingname));

            if (ffInstanceAJAXModel.assignedcustomers != null)
            {
                foreach (string ffinstance in ffInstanceAJAXModel.assignedcustomers)
                {
                    System.Diagnostics.Debug.Write(ffinstance + '\n');

                    var ffRow = db.DimFFInstances.Where(x => x.ProjectName.Equals(ffinstance)).First();

                    if (!ffRow.KeyModule.HasValue)
                    {
                        ffRow.KeyModule = Module.FirstOrDefault().id;

                        System.Diagnostics.Debug.Write(ffRow.KeyModule);

                    }
                    db.SaveChanges();

                    System.Diagnostics.Debug.Write("Assigned List Saved" + '\n');

                }
            }

            if (ffInstanceAJAXModel.availablecustomers != null)
            {
                foreach (string ffinstance in ffInstanceAJAXModel.availablecustomers)
                {
                    var ffRow = db.DimFFInstances.Where(x => x.ProjectName.Equals(ffinstance)).First();

                    if (ffRow.KeyModule.HasValue)
                    {
                        ffRow.KeyModule = null;

                        System.Diagnostics.Debug.Write(ffRow.KeyModule);

                    }

                    if (ModelState.IsValid)
                    {
                        db.Entry(ffRow).Property(x => x.KeyModule).IsModified = true;

                        db.SaveChanges();

                        System.Diagnostics.Debug.Write("Available List Saved" + '\n');
                    }
                }
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
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
