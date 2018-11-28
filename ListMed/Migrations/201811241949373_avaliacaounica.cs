namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class avaliacaounica : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Avaliacao");
            AddPrimaryKey("dbo.Avaliacao", new[] { "IdClinica", "IdUsuario" });
            DropColumn("dbo.Avaliacao", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Avaliacao", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Avaliacao");
            AddPrimaryKey("dbo.Avaliacao", "Id");
        }
    }
}
