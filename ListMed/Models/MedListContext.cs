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
        public DbSet<Localidade> Localidades { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
    }
}