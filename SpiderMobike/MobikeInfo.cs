#region copyright
// <copyright file="MobikeInfo"  company="CIS"> 
// Copyright (c) CIS. All Right Reserved
// </copyright>
// <author>ddfm</author>
// <datecreated>2017/11/17 10:18:52</datecreated>
#endregion
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderMobike
{
    [Serializable]
    public class MobikeInfo
    {
        [JsonProperty("distId")]
        public string DistId { get; set; }
        [JsonProperty("distX")]
        public string DistX { get; set; }
        [JsonProperty("distY")]
        public string DistY { get; set; }
        [JsonProperty("distNum")]
        public string DistNum { get; set; }
        [JsonProperty("distance")]
        public string Distance { get; set; }
        [JsonProperty("bikeIds")]
        public string BikeIds { get; set; }
        [JsonProperty("biktype")]
        public string BikeType { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("boundary")]
        public string Boundary { get; set; }

        public override bool Equals(object obj)
        {
            var mobikeInfo = obj as MobikeInfo;
            if (mobikeInfo == null)
            {
                return false;
            }
            if (mobikeInfo.BikeIds == this.BikeIds)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.BikeIds.GetHashCode();
        }

    }
}
