namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class avaliacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avaliacao", "DataHora", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Avaliacao", "DataHora");
        }
    }
}
