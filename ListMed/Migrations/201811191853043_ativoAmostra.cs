namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ativoAmostra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AmostraClinica", "Ativo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AmostraClinica", "Ativo");
        }
    }
}
