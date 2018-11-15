namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tel1n : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TelefonesClinica",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.String(maxLength: 17),
                        IdClinica = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinica", t => t.IdClinica, cascadeDelete: true)
                .Index(t => t.IdClinica);
            
            DropColumn("dbo.Clinica", "Telefone1");
            DropColumn("dbo.Clinica", "Telefone2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clinica", "Telefone2", c => c.String(maxLength: 17));
            AddColumn("dbo.Clinica", "Telefone1", c => c.String(maxLength: 17));
            DropForeignKey("dbo.TelefonesClinica", "IdClinica", "dbo.Clinica");
            DropIndex("dbo.TelefonesClinica", new[] { "IdClinica" });
            DropTable("dbo.TelefonesClinica");
        }
    }
}
