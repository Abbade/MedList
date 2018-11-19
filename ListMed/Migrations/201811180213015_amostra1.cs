namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amostra1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AmostraClinica", "Pontos", c => c.Int(nullable: false));
            AddColumn("dbo.AmostraClinica", "IdUsuario", c => c.Int(nullable: false));
            CreateIndex("dbo.AmostraClinica", "IdUsuario");
            AddForeignKey("dbo.AmostraClinica", "IdUsuario", "dbo.Usuario", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AmostraClinica", "IdUsuario", "dbo.Usuario");
            DropIndex("dbo.AmostraClinica", new[] { "IdUsuario" });
            DropColumn("dbo.AmostraClinica", "IdUsuario");
            DropColumn("dbo.AmostraClinica", "Pontos");
        }
    }
}
