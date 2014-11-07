﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATOMv0.Models
{
    public class FFSite
    {
        private FFCubeEntities db = new FFCubeEntities();

        public int id { get; set; }
        public DimRegion SelectedRegion { get; set; }
        public DimCountry SelectedCountry { get; set; }
        public DimFacility SelectedFacility { get; set; }
        public DimBuilding SelectedBuilding { get; set; }
        public DimModule SelectedModule { get; set; }
        public DimFFInstance SelectedFFInstance { get; set; }
        public DimStationType SelectedStationType { get; set; }

        public virtual ICollection<DimRegion> ListRegions { get; set; }
        public virtual ICollection<DimCountry> ListCountries { get; set; }
        public virtual ICollection<DimFacility> ListFacilities { get; set; }
        public virtual ICollection<DimBuilding> ListBuildings { get; set; }
        public virtual ICollection<DimModule> ListModules { get; set; }
        public virtual ICollection<DimFFInstance> ListFFInstances { get; set; }
        public virtual ICollection<DimStationType> ListStationTypes { get; set; }

        public bool IsRegionSelected()
        {
            if (!SelectedRegion.RegionName.Equals(null)) return true;

            else return false;
        }

        public bool IsCountrySelected()
        {
            if (!SelectedCountry.CountryName.Equals(null)) return true;

            else return false;
        }

        public bool IsFacilitySelected()
        {
            if (!SelectedFacility.SiteName.Equals(null)) return true;

            else return false;
        }

        public bool IsBuildingSelected()
        {
            if (!SelectedBuilding.BuildingName.Equals(null)) return true;

            else return false;
        }

        public bool IsModuleSelected()
        {
            if (!SelectedModule.ModuleName.Equals(null)) return true;

            else return false;
        }

        public bool IsFFInstanceSelected()
        {
            if (!SelectedFFInstance.DatabaseName.Equals(null)) return true;

            else return false;
        }
        public bool IsStationTypeSelected()
        {
            if (!SelectedStationType.StationTypeName.Equals(null)) return true;

            else return false;
        }


    }
}