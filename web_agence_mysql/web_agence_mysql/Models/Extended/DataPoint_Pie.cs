using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace web_agence_mysql.Models
{
    [DataContract]
    public class DataPoint_Pie
    {
        public DataPoint_Pie(double y, string indexLabel)
        {
            this.Y = y;
            this.IndexLabel = indexLabel;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;

        [DataMember(Name = "indexLabel")]
        public string IndexLabel = null;
    }
}