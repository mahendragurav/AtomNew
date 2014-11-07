using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Configuration;

using System.Web.Script.Serialization;
using ATOMv0.Models;
using System.Web.Security;

namespace AtomConfiguratorModel.Models
{
   [AuthorizeEnum(RolesEnum.Roles.SiteAdministrator)]
  public class FFSiteController : Controller
  {

    private FFCubeEntities db = new FFCubeEntities();

    // GET: FFSite

   
    public ActionResult Index()
    {
      // bool isRole = User.IsInRole(RolesEnum.Roles.AtomAdministrator.ToString());
      // string[] roles = Roles.GetRolesForUser();
      // string userName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
      // string[] roles = Roles.GetRolesForUser(userName);

      // if (roles.Contains(RolesEnum.Roles.SiteAdministrator.ToString()))
      // {
      ViewBag.KeyRegion = (from r in db.DimRegions
                           select r.RegionName).Distinct();
      return View();
      //}
      //else
      //{

      /// return RedirectToAction("Login", "Home");
      // }


    }


    // GET: FFSite/GetDropDownData
    public JsonResult GetDropDownData(string typeofData, string filter, string buildingfilter, string bucketfilter)
    {
      switch (typeofData)
      {

        case "RegionList":

          var queryregions = db.DimRegions.ToList();

          IEnumerable<DimRegion> regions = queryregions.Select(x => new DimRegion { RegionName = x.RegionName, id = x.id });

          return Json(new SelectList(regions.ToList()), JsonRequestBehavior.AllowGet);

        case "CountryList":

          var querycountries = db.DimCountries.Where(x => x.DimRegion.RegionName.Equals(filter)).ToList();

          IEnumerable<DimCountry> countries = querycountries.Select(x => new DimCountry { CountryName = x.CountryName, id = x.id });

          return Json(countries, JsonRequestBehavior.AllowGet);

        case "FacilityList":

          var queryfacilities = db.DimFacilities.Where(x => x.DimCountry.CountryName.Equals(filter)).ToList();

          IEnumerable<DimFacility> facilities = queryfacilities.Select(x => new DimFacility { SiteName = x.SiteName, id = x.id });

          return Json(facilities, JsonRequestBehavior.AllowGet);

        case "BuildingList":

          var querybuildings = db.DimBuildings.Where(x => x.DimFacility.SiteName.Equals(filter)).ToList();

          IEnumerable<DimBuilding> buildings = querybuildings.Select(x => new DimBuilding { BuildingName = x.BuildingName, id = x.id });

          return Json(buildings, JsonRequestBehavior.AllowGet);

        case "ModuleList":

          var querymodules = db.DimModules.Where(x => x.DimBuilding.BuildingName.Equals(filter)).ToList();

          IEnumerable<DimModule> modules = querymodules.Select(x => new DimModule { ModuleName = x.ModuleName, id = x.id });

          return Json(modules, JsonRequestBehavior.AllowGet);

        case "FFInstanceList":

          var queryffinstances = db.DimFFInstances.AsEnumerable();

          IEnumerable<DimFFInstance> ffinstances;

          if (buildingfilter == null)
          {
            //int? nullfilter = null;

            queryffinstances = db.DimFFInstances.Where(x => x.DimModule.DimBuilding.DimFacility.SiteName.Equals(filter));

            queryffinstances = queryffinstances.Where(x => !x.KeyModule.HasValue || x.KeyModule == null);


            SqlConnection sc = (SqlConnection)db.Database.Connection; //get the SQLConnection that your entity object would use
            string adoConnStr = sc.ConnectionString;

            SqlConnection vcon = new SqlConnection(adoConnStr);

            List<DimFFInstance> ffinstlist = new List<DimFFInstance>();

            SqlDataAdapter da = new SqlDataAdapter("select id,ProjectName from DimFFInstance where KeyModule IS NULL and SiteName LIKE '" + filter + "'", vcon);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
              DimFFInstance ffinst = new DimFFInstance();
              ffinst.id = int.Parse(row["id"].ToString());
              ffinst.ProjectName = row["ProjectName"].ToString();
              ffinstlist.Add(ffinst);
            }

            return Json(ffinstlist, JsonRequestBehavior.AllowGet);

            System.Diagnostics.Debug.Write(dt.Rows[0]["ProjectName"]);

            //ffinstances = queryffinstances.Select(x => new DimFFInstance { ProjectName = x.ProjectName, id = x.id , KeyModule = x.KeyModule ?? null }).ToList();

            //return Json(ffinstances, JsonRequestBehavior.AllowGet);

          }
          else

            queryffinstances = db.DimFFInstances.Where(x => x.DimModule.DimBuilding.BuildingName.Equals(buildingfilter));

          ffinstances = queryffinstances.Select(x => new DimFFInstance { ProjectName = x.ProjectName, id = x.id }).ToList();

          System.Diagnostics.Debug.Write(Json(ffinstances).Data.ToString());

          return Json(ffinstances, JsonRequestBehavior.AllowGet);

        case "StationTypesList":

          var ffinstance = db.DimFFInstances.Where(x => x.ProjectName.Equals(filter)).First();

          var querystationtypes = db.DimStationTypes.Where(x => x.FFInstanceID == ffinstance.id);

          if (bucketfilter != null)
          {

            var bucket = db.DimBuckets.Where(x => x.BucketName.Equals(bucketfilter)).First();

            querystationtypes = querystationtypes.Where(x => x.KeyBucket == bucket.id).OrderBy(x => x.Sequence);
          }

          var liststationtypes = querystationtypes.ToList();

          IEnumerable<DimStationType> stationtypes = liststationtypes.Select(x => new DimStationType { StationTypeName = x.StationTypeName, id = x.id });

          return Json(stationtypes, JsonRequestBehavior.AllowGet);

        case "BucketsList":

          var querybuckets = db.DimBuckets.ToList();

          IEnumerable<DimBucket> buckets = querybuckets.Select(x => new DimBucket { BucketName = x.BucketName, id = x.id });

          return Json(buckets, JsonRequestBehavior.AllowGet);

        case "CustomersList":

          var querycustomers = db.DimBusinessPartners.AsEnumerable();

          if (buildingfilter != null)
          {
            var building = db.DimBuildings.Where(x => x.BuildingName.Equals(buildingfilter)).First();

            querycustomers = db.DimBusinessPartners.Where(x => x.KeyBuilding == building.id).ToList();
          }
          else
          {
            var site = db.DimFacilities.Where(x => x.SiteName.Equals(filter)).First();

            querycustomers = db.DimBusinessPartners.Where(x => x.KeySite == site.id).ToList();

            querycustomers = querycustomers.Where(x => x.KeyBuilding == null).ToList();
          }

          IEnumerable<DimBusinessPartner> customers = querycustomers.Select(x => new DimBusinessPartner { BPCode = x.BPCode, id = x.id });

          return Json(customers, JsonRequestBehavior.AllowGet);


        default:
          return Json(new SelectList(null));
      }
    }
  }
}