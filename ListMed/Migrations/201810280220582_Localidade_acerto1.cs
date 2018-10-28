namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Localidade_acerto1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Localidade", "UF", c => c.String(maxLength: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Localidade", "UF");
        }
    }
}
