using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.DTO
{

    public class Lead
    {

        public string formatted_address { get; set; }
        public string name { get; set; }
        public double rating { get; set; }
        public string place_id { get; set; }


        public   opening_hours opening_hours { get; set; }
        public  geometry geometry { get; set; }

    }
}