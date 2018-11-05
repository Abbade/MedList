namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cidadebairro : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LocalidadeClinicas", "Localidade_Id", "dbo.Localidade");
            DropForeignKey("dbo.LocalidadeClinicas", "Clinica_Id", "dbo.Clinica");
            DropIndex("dbo.LocalidadeClinicas", new[] { "Localidade_Id" });
            DropIndex("dbo.LocalidadeClinicas", new[] { "Clinica_Id" });
            CreateTable(
                "dbo.Bairro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 200),
                        IdCidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cidade", t => t.IdCidade, cascadeDelete: false)
                .Index(t => t.IdCidade);
            
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 200),
                        IdEstado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estado", t => t.IdEstado, cascadeDelete: false)
                .Index(t => t.IdEstado);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 200),
                        UF = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clinica", "IdEstado", c => c.Int(nullable: false));
            AddColumn("dbo.Clinica", "IdCidade", c => c.Int(nullable: false));
            AddColumn("dbo.Clinica", "IdBairro", c => c.Int(nullable: false));
            CreateIndex("dbo.Clinica", "IdEstado");
            CreateIndex("dbo.Clinica", "IdCidade");
            CreateIndex("dbo.Clinica", "IdBairro");
            AddForeignKey("dbo.Clinica", "IdCidade", "dbo.Cidade", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Clinica", "IdEstado", "dbo.Estado", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Clinica", "IdBairro", "dbo.Bairro", "Id", cascadeDelete: true);
            DropTable("dbo.Localidade");
            DropTable("dbo.LocalidadeClinicas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LocalidadeClinicas",
                c => new
                    {
                        Localidade_Id = c.Int(nullable: false),
                        Clinica_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Localidade_Id, t.Clinica_Id });
            
            CreateTable(
                "dbo.Localidade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 200),
                        Tipo = c.String(maxLength: 1),
                        UF = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Clinica", "IdBairro", "dbo.Bairro");
            DropForeignKey("dbo.Bairro", "IdCidade", "dbo.Cidade");
            DropForeignKey("dbo.Clinica", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.Cidade", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.Clinica", "IdCidade", "dbo.Cidade");
            DropIndex("dbo.Cidade", new[] { "IdEstado" });
            DropIndex("dbo.Bairro", new[] { "IdCidade" });
            DropIndex("dbo.Clinica", new[] { "IdBairro" });
            DropIndex("dbo.Clinica", new[] { "IdCidade" });
            DropIndex("dbo.Clinica", new[] { "IdEstado" });
            DropColumn("dbo.Clinica", "IdBairro");
            DropColumn("dbo.Clinica", "IdCidade");
            DropColumn("dbo.Clinica", "IdEstado");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
            DropTable("dbo.Bairro");
            CreateIndex("dbo.LocalidadeClinicas", "Clinica_Id");
            CreateIndex("dbo.LocalidadeClinicas", "Localidade_Id");
            AddForeignKey("dbo.LocalidadeClinicas", "Clinica_Id", "dbo.Clinica", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LocalidadeClinicas", "Localidade_Id", "dbo.Localidade", "Id", cascadeDelete: true);
        }
    }
}
