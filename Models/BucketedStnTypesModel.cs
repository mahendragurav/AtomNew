using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATOMv0.Models
{
    public class BucketedStnTypesModel
    {
        public string bucketname { get; set; }
        public List<String> orderedstationtypes { get; set; }
    }
}