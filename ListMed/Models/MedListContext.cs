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
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TelefonesClinica> TelefonesClinicas { get; set; }
    }
}