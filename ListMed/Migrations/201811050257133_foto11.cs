namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foto11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Foto", "IdClinica", "dbo.Clinica");
            DropIndex("dbo.Foto", new[] { "IdClinica" });
            AlterColumn("dbo.Foto", "IdClinica", c => c.Int());
            CreateIndex("dbo.Foto", "IdClinica");
            AddForeignKey("dbo.Foto", "IdClinica", "dbo.Clinica", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Foto", "IdClinica", "dbo.Clinica");
            DropIndex("dbo.Foto", new[] { "IdClinica" });
            AlterColumn("dbo.Foto", "IdClinica", c => c.Int(nullable: false));
            CreateIndex("dbo.Foto", "IdClinica");
            AddForeignKey("dbo.Foto", "IdClinica", "dbo.Clinica", "Id", cascadeDelete: true);
        }
    }
}
