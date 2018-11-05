namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foto1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Foto", "Clinica_Id1", "dbo.Clinica");
            DropIndex("dbo.Foto", new[] { "Clinica_Id1" });
            RenameColumn(table: "dbo.Foto", name: "Clinica_Id1", newName: "IdClinica");
            AlterColumn("dbo.Foto", "IdClinica", c => c.Int(nullable: false));
            CreateIndex("dbo.Foto", "IdClinica");
            AddForeignKey("dbo.Foto", "IdClinica", "dbo.Clinica", "Id", cascadeDelete: true);
            DropColumn("dbo.Foto", "Clinica_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foto", "Clinica_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Foto", "IdClinica", "dbo.Clinica");
            DropIndex("dbo.Foto", new[] { "IdClinica" });
            AlterColumn("dbo.Foto", "IdClinica", c => c.Int());
            RenameColumn(table: "dbo.Foto", name: "IdClinica", newName: "Clinica_Id1");
            CreateIndex("dbo.Foto", "Clinica_Id1");
            AddForeignKey("dbo.Foto", "Clinica_Id1", "dbo.Clinica", "Id");
        }
    }
}
