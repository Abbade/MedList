namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fotos1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Foto", "altura", c => c.Double(nullable: false));
            AddColumn("dbo.Foto", "largura", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Foto", "largura");
            DropColumn("dbo.Foto", "altura");
        }
    }
}
