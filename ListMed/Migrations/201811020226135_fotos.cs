namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fotos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Foto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(maxLength: 120),
                        URL = c.String(),
                        IdCliente = c.Int(nullable: false),
                        Clinica_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinica", t => t.Clinica_Id)
                .Index(t => t.Clinica_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Foto", "Clinica_Id", "dbo.Clinica");
            DropIndex("dbo.Foto", new[] { "Clinica_Id" });
            DropTable("dbo.Foto");
        }
    }
}
