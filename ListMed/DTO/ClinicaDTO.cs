using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListMed.DTO
{
    public class ClinicaDTO
    {
        public string formatted_address { get; set; }
        public geometry geometry { get; set; }
        public string name { get; set; }
        public opening_hours opening_hours { get; set; }
        public double rating { get; set; }
        public string place_id { get; set; }

    }
}