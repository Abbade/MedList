namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_avaliacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avaliacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nota = c.Int(),
                        comentario = c.String(maxLength: 275),
                        IdClinica = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinica", t => t.IdClinica, cascadeDelete: true)
                .Index(t => t.IdClinica);
            
            DropColumn("dbo.Clinica", "Snippet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clinica", "Snippet", c => c.String());
            DropForeignKey("dbo.Avaliacao", "IdClinica", "dbo.Clinica");
            DropIndex("dbo.Avaliacao", new[] { "IdClinica" });
            DropTable("dbo.Avaliacao");
        }
    }
}
