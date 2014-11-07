using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ATOMv0.Models
{
    public class DimStationTypesController : Controller
    {
        private FFCubeEntities db = new FFCubeEntities();

        // GET: DimStationTypes
        public ActionResult Index()
        {
            return View(db.DimStationTypes.ToList());
        }

        // GET: DimStationTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimStationType dimStationType = db.DimStationTypes.Find(id);
            if (dimStationType == null)
            {
                return HttpNotFound();
            }
            return View(dimStationType);
        }

        // GET: DimStationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DimStationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,StationTypeName,KeyBucket,Status,FFInstanceID,Sequence")] DimStationType dimStationType)
        {
            if (ModelState.IsValid)
            {
                db.DimStationTypes.Add(dimStationType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dimStationType);
        }

        // GET: DimStationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimStationType dimStationType = db.DimStationTypes.Find(id);
            if (dimStationType == null)
            {
                return HttpNotFound();
            }
            return View(dimStationType);
        }

        // POST: DimStationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,StationTypeName,KeyBucket,Status,FFInstanceID,Sequence")] DimStationType dimStationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimStationType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dimStationType);
        }

        // GET: DimStationTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimStationType dimStationType = db.DimStationTypes.Find(id);
            if (dimStationType == null)
            {
                return HttpNotFound();
            }
            return View(dimStationType);
        }

        // POST: DimStationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimStationType dimStationType = db.DimStationTypes.Find(id);
            db.DimStationTypes.Remove(dimStationType);
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

        //POST: DimStationTypes/Update
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(BucketedStnTypesModel bucketstationtypes)
        {
            var Bucket = db.DimBuckets.Where(x => x.BucketName.Equals(bucketstationtypes.bucketname)).First();


            if (bucketstationtypes.orderedstationtypes != null)
            {
                var sequence = 0;

                foreach (string stationtype in bucketstationtypes.orderedstationtypes)
                {
                    System.Diagnostics.Debug.Write(stationtype + '\n');

                    var stnTypeRow = db.DimStationTypes.Where(x => x.StationTypeName.Equals(stationtype)).First();


                    stnTypeRow.KeyBucket = Bucket.id;

                    stnTypeRow.Sequence = sequence;

                    //System.Diagnostics.Debug.Write(ffRow.KeyModule);


                    db.SaveChanges();

                    System.Diagnostics.Debug.Write("Ordered Bucket List Saved" + '\n');

                    sequence++;

                }

                sequence = 0;



            }


            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
