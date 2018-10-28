namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Localidade_acerto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Localidade", "Tipo", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Localidade", "Tipo");
        }
    }
}
