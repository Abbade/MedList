using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.DTO
{
    [Table("location")]
    public class location
    {
        [Key]
        public int Id { get; set; }

        public string lat { get; set; }
        public string lng { get; set; }
    }
}