namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullponto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuario", "Pontos", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "Pontos", c => c.Int(nullable: false));
        }
    }
}
