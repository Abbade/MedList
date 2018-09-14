using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    public class MedListContext : DbContext
    {
        public MedListContext() : base("name=MedListContext")
        {

        }
        public DbSet<Clinica> Clinicas { get; set; }
    }
}