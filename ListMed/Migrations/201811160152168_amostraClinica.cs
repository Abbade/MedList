namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amostraClinica : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TelefonesClinica", "IdClinica", "dbo.Clinica");
            DropIndex("dbo.TelefonesClinica", new[] { "IdClinica" });
            CreateTable(
                "dbo.AmostraClinica",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaceID = c.String(),
                        NomeFantasia = c.String(maxLength: 180),
                        LinkSite = c.String(maxLength: 80),
                        Lt = c.String(),
                        Lg = c.String(),
                        EnderecoFormatado = c.String(),
                        avaliacao = c.Double(nullable: true),
                        PrecoConsulta = c.Decimal(precision: 18, scale: 2),
                        PrecoExame = c.Decimal(precision: 18, scale: 2),
                        HoraAbertura = c.Time(nullable: false, precision: 7),
                        HoraFechamento = c.Time(nullable: false, precision: 7),
                        IdEstado = c.Int(),
                        IdCidade = c.Int(),
                        IdBairro = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bairro", t => t.IdBairro)
                .ForeignKey("dbo.Cidade", t => t.IdCidade)
                .ForeignKey("dbo.Estado", t => t.IdEstado)
                .Index(t => t.IdEstado)
                .Index(t => t.IdCidade)
                .Index(t => t.IdBairro);
            
            CreateTable(
                "dbo.EspecialidadeAmostraClinicas",
                c => new
                    {
                        Especialidade_Id = c.Int(nullable: false),
                        AmostraClinica_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Especialidade_Id, t.AmostraClinica_Id })
                .ForeignKey("dbo.Especialidade", t => t.Especialidade_Id, cascadeDelete: true)
                .ForeignKey("dbo.AmostraClinica", t => t.AmostraClinica_Id, cascadeDelete: true)
                .Index(t => t.Especialidade_Id)
                .Index(t => t.AmostraClinica_Id);
            
            CreateTable(
                "dbo.ServicoAmostraClinicas",
                c => new
                    {
                        Servico_Id = c.Int(nullable: false),
                        AmostraClinica_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Servico_Id, t.AmostraClinica_Id })
                .ForeignKey("dbo.Servico", t => t.Servico_Id, cascadeDelete: true)
                .ForeignKey("dbo.AmostraClinica", t => t.AmostraClinica_Id, cascadeDelete: true)
                .Index(t => t.Servico_Id)
                .Index(t => t.AmostraClinica_Id);
            
            AddColumn("dbo.Foto", "IdAmostraClinica", c => c.Int());
            AddColumn("dbo.Foto", "Imagem", c => c.Binary());
            AddColumn("dbo.Foto", "tipoImg", c => c.String());
            AddColumn("dbo.TelefonesClinica", "IdAmostraClinica", c => c.Int());
            AlterColumn("dbo.TelefonesClinica", "IdClinica", c => c.Int());
            CreateIndex("dbo.Foto", "IdAmostraClinica");
            CreateIndex("dbo.TelefonesClinica", "IdClinica");
            CreateIndex("dbo.TelefonesClinica", "IdAmostraClinica");
            AddForeignKey("dbo.Foto", "IdAmostraClinica", "dbo.AmostraClinica", "Id");
            AddForeignKey("dbo.TelefonesClinica", "IdAmostraClinica", "dbo.AmostraClinica", "Id");
            AddForeignKey("dbo.TelefonesClinica", "IdClinica", "dbo.Clinica", "Id");
            DropColumn("dbo.Clinica", "TituloSite");
            DropColumn("dbo.Foto", "altura");
            DropColumn("dbo.Foto", "largura");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foto", "largura", c => c.Double());
            AddColumn("dbo.Foto", "altura", c => c.Double());
            AddColumn("dbo.Clinica", "TituloSite", c => c.String(maxLength: 180));
            DropForeignKey("dbo.TelefonesClinica", "IdClinica", "dbo.Clinica");
            DropForeignKey("dbo.AmostraClinica", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.AmostraClinica", "IdCidade", "dbo.Cidade");
            DropForeignKey("dbo.AmostraClinica", "IdBairro", "dbo.Bairro");
            DropForeignKey("dbo.TelefonesClinica", "IdAmostraClinica", "dbo.AmostraClinica");
            DropForeignKey("dbo.ServicoAmostraClinicas", "AmostraClinica_Id", "dbo.AmostraClinica");
            DropForeignKey("dbo.ServicoAmostraClinicas", "Servico_Id", "dbo.Servico");
            DropForeignKey("dbo.Foto", "IdAmostraClinica", "dbo.AmostraClinica");
            DropForeignKey("dbo.EspecialidadeAmostraClinicas", "AmostraClinica_Id", "dbo.AmostraClinica");
            DropForeignKey("dbo.EspecialidadeAmostraClinicas", "Especialidade_Id", "dbo.Especialidade");
            DropIndex("dbo.ServicoAmostraClinicas", new[] { "AmostraClinica_Id" });
            DropIndex("dbo.ServicoAmostraClinicas", new[] { "Servico_Id" });
            DropIndex("dbo.EspecialidadeAmostraClinicas", new[] { "AmostraClinica_Id" });
            DropIndex("dbo.EspecialidadeAmostraClinicas", new[] { "Especialidade_Id" });
            DropIndex("dbo.TelefonesClinica", new[] { "IdAmostraClinica" });
            DropIndex("dbo.TelefonesClinica", new[] { "IdClinica" });
            DropIndex("dbo.Foto", new[] { "IdAmostraClinica" });
            DropIndex("dbo.AmostraClinica", new[] { "IdBairro" });
            DropIndex("dbo.AmostraClinica", new[] { "IdCidade" });
            DropIndex("dbo.AmostraClinica", new[] { "IdEstado" });
            AlterColumn("dbo.TelefonesClinica", "IdClinica", c => c.Int(nullable: false));
            DropColumn("dbo.TelefonesClinica", "IdAmostraClinica");
            DropColumn("dbo.Foto", "tipoImg");
            DropColumn("dbo.Foto", "Imagem");
            DropColumn("dbo.Foto", "IdAmostraClinica");
            DropTable("dbo.ServicoAmostraClinicas");
            DropTable("dbo.EspecialidadeAmostraClinicas");
            DropTable("dbo.AmostraClinica");
            CreateIndex("dbo.TelefonesClinica", "IdClinica");
            AddForeignKey("dbo.TelefonesClinica", "IdClinica", "dbo.Clinica", "Id", cascadeDelete: true);
        }
    }
}
