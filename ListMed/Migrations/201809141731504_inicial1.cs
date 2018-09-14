namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clinica",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeFantasia = c.String(),
                        TituloSite = c.String(),
                        Snippet = c.String(),
                        LinkSite = c.String(),
                        IdCidade = c.Int(nullable: false),
                        DescricaoCidade = c.String(),
                        IdBairro = c.Int(nullable: false),
                        DescricaoBairro = c.String(),
                        IdEstado = c.Int(nullable: false),
                        Uf = c.String(),
                        Lt = c.String(),
                        Lg = c.String(),
                        EnderecoFormatado = c.String(),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Clinica");
        }
    }
}
