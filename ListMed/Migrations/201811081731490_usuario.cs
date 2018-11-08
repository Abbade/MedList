namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false, maxLength: 50),
                        nick = c.String(nullable: false, maxLength: 50),
                        senha = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsuarioClinicas",
                c => new
                    {
                        Usuario_Id = c.Int(nullable: false),
                        Clinica_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Usuario_Id, t.Clinica_Id })
                .ForeignKey("dbo.Usuario", t => t.Usuario_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clinica", t => t.Clinica_Id, cascadeDelete: true)
                .Index(t => t.Usuario_Id)
                .Index(t => t.Clinica_Id);
            
            AddColumn("dbo.Avaliacao", "IdUsuario", c => c.Int(nullable: false));
            CreateIndex("dbo.Avaliacao", "IdUsuario");
            AddForeignKey("dbo.Avaliacao", "IdUsuario", "dbo.Usuario", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioClinicas", "Clinica_Id", "dbo.Clinica");
            DropForeignKey("dbo.UsuarioClinicas", "Usuario_Id", "dbo.Usuario");
            DropForeignKey("dbo.Avaliacao", "IdUsuario", "dbo.Usuario");
            DropIndex("dbo.UsuarioClinicas", new[] { "Clinica_Id" });
            DropIndex("dbo.UsuarioClinicas", new[] { "Usuario_Id" });
            DropIndex("dbo.Avaliacao", new[] { "IdUsuario" });
            DropColumn("dbo.Avaliacao", "IdUsuario");
            DropTable("dbo.UsuarioClinicas");
            DropTable("dbo.Usuario");
        }
    }
}
