namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Foto", "Clinica_Id", "dbo.Clinica");
            DropIndex("dbo.Foto", new[] { "Clinica_Id" });
            AddColumn("dbo.Foto", "Clinica_Id1", c => c.Int());
            AlterColumn("dbo.Foto", "altura", c => c.Double());
            AlterColumn("dbo.Foto", "largura", c => c.Double());
            AlterColumn("dbo.Foto", "Clinica_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Foto", "Clinica_Id1");
            AddForeignKey("dbo.Foto", "Clinica_Id1", "dbo.Clinica", "Id");
            DropColumn("dbo.Foto", "IdCliente");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foto", "IdCliente", c => c.Int(nullable: false));
            DropForeignKey("dbo.Foto", "Clinica_Id1", "dbo.Clinica");
            DropIndex("dbo.Foto", new[] { "Clinica_Id1" });
            AlterColumn("dbo.Foto", "Clinica_Id", c => c.Int());
            AlterColumn("dbo.Foto", "largura", c => c.Double(nullable: false));
            AlterColumn("dbo.Foto", "altura", c => c.Double(nullable: false));
            DropColumn("dbo.Foto", "Clinica_Id1");
            CreateIndex("dbo.Foto", "Clinica_Id");
            AddForeignKey("dbo.Foto", "Clinica_Id", "dbo.Clinica", "Id");
        }
    }
}
