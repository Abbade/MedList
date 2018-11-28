namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estadouf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estado", "Uf", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Estado", "Uf");
        }
    }
}
