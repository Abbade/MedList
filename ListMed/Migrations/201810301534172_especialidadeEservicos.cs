namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class especialidadeEservicos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Especialidade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        descricao = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Servico",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 200),
                        preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EspecialidadeClinicas",
                c => new
                    {
                        Especialidade_Id = c.Int(nullable: false),
                        Clinica_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Especialidade_Id, t.Clinica_Id })
                .ForeignKey("dbo.Especialidade", t => t.Especialidade_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clinica", t => t.Clinica_Id, cascadeDelete: true)
                .Index(t => t.Especialidade_Id)
                .Index(t => t.Clinica_Id);
            
            CreateTable(
                "dbo.ServicoClinicas",
                c => new
                    {
                        Servico_Id = c.Int(nullable: false),
                        Clinica_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Servico_Id, t.Clinica_Id })
                .ForeignKey("dbo.Servico", t => t.Servico_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clinica", t => t.Clinica_Id, cascadeDelete: true)
                .Index(t => t.Servico_Id)
                .Index(t => t.Clinica_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServicoClinicas", "Clinica_Id", "dbo.Clinica");
            DropForeignKey("dbo.ServicoClinicas", "Servico_Id", "dbo.Servico");
            DropForeignKey("dbo.EspecialidadeClinicas", "Clinica_Id", "dbo.Clinica");
            DropForeignKey("dbo.EspecialidadeClinicas", "Especialidade_Id", "dbo.Especialidade");
            DropIndex("dbo.ServicoClinicas", new[] { "Clinica_Id" });
            DropIndex("dbo.ServicoClinicas", new[] { "Servico_Id" });
            DropIndex("dbo.EspecialidadeClinicas", new[] { "Clinica_Id" });
            DropIndex("dbo.EspecialidadeClinicas", new[] { "Especialidade_Id" });
            DropTable("dbo.ServicoClinicas");
            DropTable("dbo.EspecialidadeClinicas");
            DropTable("dbo.Servico");
            DropTable("dbo.Especialidade");
        }
    }
}
