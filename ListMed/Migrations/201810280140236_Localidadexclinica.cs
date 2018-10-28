namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Localidadexclinica : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocalidadeClinicas",
                c => new
                    {
                        Localidade_Id = c.Int(nullable: false),
                        Clinica_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Localidade_Id, t.Clinica_Id })
                .ForeignKey("dbo.Localidade", t => t.Localidade_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clinica", t => t.Clinica_Id, cascadeDelete: true)
                .Index(t => t.Localidade_Id)
                .Index(t => t.Clinica_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocalidadeClinicas", "Clinica_Id", "dbo.Clinica");
            DropForeignKey("dbo.LocalidadeClinicas", "Localidade_Id", "dbo.Localidade");
            DropIndex("dbo.LocalidadeClinicas", new[] { "Clinica_Id" });
            DropIndex("dbo.LocalidadeClinicas", new[] { "Localidade_Id" });
            DropTable("dbo.LocalidadeClinicas");
        }
    }
}
