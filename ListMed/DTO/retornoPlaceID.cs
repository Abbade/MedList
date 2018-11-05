using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListMed.DTO
{
    public class retornoPlaceID
    {
        public List<object> html_attributions { get; set; }
        public Result result { get; set; }
        public string status { get; set; }
    }
}