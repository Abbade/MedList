namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
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
                        avaliacao = c.Double(),
                        PrecoConsulta = c.Decimal(precision: 18, scale: 2),
                        PrecoExame = c.Decimal(precision: 18, scale: 2),
                        HoraAbertura = c.Time(precision: 7),
                        HoraFechamento = c.Time(precision: 7),
                        IdEstado = c.Int(nullable: false),
                        Pontos = c.Int(nullable: false),
                        IdCidade = c.Int(),
                        IdBairro = c.Int(),
                        IdUsuario = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Bairro", t => t.IdBairro)
                .ForeignKey("dbo.Municipio", t => t.IdCidade)
                .ForeignKey("dbo.Estado", t => t.IdEstado, cascadeDelete: true)
                .Index(t => t.IdEstado)
                .Index(t => t.IdCidade)
                .Index(t => t.IdBairro)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.Bairro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 255),
                        Codigo = c.String(maxLength: 20),
                        Uf = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clinica",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaceID = c.String(),
                        NomeFantasia = c.String(maxLength: 180),
                        LinkSite = c.String(maxLength: 80),
                        Lt = c.String(),
                        Lg = c.String(),
                        EnderecoFormatado = c.String(),
                        avaliacao = c.Double(nullable: false),
                        PrecoConsulta = c.Decimal(precision: 18, scale: 2),
                        PrecoExame = c.Decimal(precision: 18, scale: 2),
                        HoraAbertura = c.Time(nullable: false, precision: 7),
                        HoraFechamento = c.Time(nullable: false, precision: 7),
                        IdEstado = c.Int(nullable: false),
                        IdCidade = c.Int(),
                        IdBairro = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bairro", t => t.IdBairro)
                .ForeignKey("dbo.Municipio", t => t.IdCidade)
                .ForeignKey("dbo.Estado", t => t.IdEstado, cascadeDelete: true)
                .Index(t => t.IdEstado)
                .Index(t => t.IdCidade)
                .Index(t => t.IdBairro);
            
            CreateTable(
                "dbo.Avaliacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nota = c.Int(),
                        comentario = c.String(maxLength: 275),
                        IdClinica = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                        DataHora = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinica", t => t.IdClinica, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdClinica)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false, maxLength: 50),
                        nick = c.String(nullable: false, maxLength: 50),
                        senha = c.String(nullable: false),
                        Foto = c.Binary(),
                        Tipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Municipio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Nome = c.String(maxLength: 200),
                        Uf = c.String(maxLength: 2),
                        Estado_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estado", t => t.Estado_Id)
                .Index(t => t.Estado_Id);
            
            CreateTable(
                "dbo.Especialidade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        descricao = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 50),
                        CodigoUf = c.String(maxLength: 2),
                        Regiao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regiao", t => t.Regiao, cascadeDelete: true)
                .Index(t => t.Regiao);
            
            CreateTable(
                "dbo.Regiao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Foto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(maxLength: 120),
                        URL = c.String(),
                        IdClinica = c.Int(),
                        IdAmostraClinica = c.Int(),
                        Imagem = c.Binary(),
                        tipoImg = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AmostraClinica", t => t.IdAmostraClinica)
                .ForeignKey("dbo.Clinica", t => t.IdClinica)
                .Index(t => t.IdClinica)
                .Index(t => t.IdAmostraClinica);
            
            CreateTable(
                "dbo.Servico",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TelefonesClinica",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.String(maxLength: 17),
                        IdClinica = c.Int(),
                        IdAmostraClinica = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AmostraClinica", t => t.IdAmostraClinica)
                .ForeignKey("dbo.Clinica", t => t.IdClinica)
                .Index(t => t.IdClinica)
                .Index(t => t.IdAmostraClinica);
            
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
            DropForeignKey("dbo.AmostraClinica", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.AmostraClinica", "IdCidade", "dbo.Municipio");
            DropForeignKey("dbo.AmostraClinica", "IdBairro", "dbo.Bairro");
            DropForeignKey("dbo.TelefonesClinica", "IdClinica", "dbo.Clinica");
            DropForeignKey("dbo.TelefonesClinica", "IdAmostraClinica", "dbo.AmostraClinica");
            DropForeignKey("dbo.ServicoClinicas", "Clinica_Id", "dbo.Clinica");
            DropForeignKey("dbo.ServicoClinicas", "Servico_Id", "dbo.Servico");
            DropForeignKey("dbo.ServicoAmostraClinicas", "AmostraClinica_Id", "dbo.AmostraClinica");
            DropForeignKey("dbo.ServicoAmostraClinicas", "Servico_Id", "dbo.Servico");
            DropForeignKey("dbo.Foto", "IdClinica", "dbo.Clinica");
            DropForeignKey("dbo.Foto", "IdAmostraClinica", "dbo.AmostraClinica");
            DropForeignKey("dbo.Estado", "Regiao", "dbo.Regiao");
            DropForeignKey("dbo.Clinica", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.Municipio", "Estado_Id", "dbo.Estado");
            DropForeignKey("dbo.EspecialidadeClinicas", "Clinica_Id", "dbo.Clinica");
            DropForeignKey("dbo.EspecialidadeClinicas", "Especialidade_Id", "dbo.Especialidade");
            DropForeignKey("dbo.EspecialidadeAmostraClinicas", "AmostraClinica_Id", "dbo.AmostraClinica");
            DropForeignKey("dbo.EspecialidadeAmostraClinicas", "Especialidade_Id", "dbo.Especialidade");
            DropForeignKey("dbo.Clinica", "IdCidade", "dbo.Municipio");
            DropForeignKey("dbo.Clinica", "IdBairro", "dbo.Bairro");
            DropForeignKey("dbo.UsuarioClinicas", "Clinica_Id", "dbo.Clinica");
            DropForeignKey("dbo.UsuarioClinicas", "Usuario_Id", "dbo.Usuario");
            DropForeignKey("dbo.Avaliacao", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.AmostraClinica", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Avaliacao", "IdClinica", "dbo.Clinica");
            DropIndex("dbo.ServicoClinicas", new[] { "Clinica_Id" });
            DropIndex("dbo.ServicoClinicas", new[] { "Servico_Id" });
            DropIndex("dbo.ServicoAmostraClinicas", new[] { "AmostraClinica_Id" });
            DropIndex("dbo.ServicoAmostraClinicas", new[] { "Servico_Id" });
            DropIndex("dbo.EspecialidadeClinicas", new[] { "Clinica_Id" });
            DropIndex("dbo.EspecialidadeClinicas", new[] { "Especialidade_Id" });
            DropIndex("dbo.EspecialidadeAmostraClinicas", new[] { "AmostraClinica_Id" });
            DropIndex("dbo.EspecialidadeAmostraClinicas", new[] { "Especialidade_Id" });
            DropIndex("dbo.UsuarioClinicas", new[] { "Clinica_Id" });
            DropIndex("dbo.UsuarioClinicas", new[] { "Usuario_Id" });
            DropIndex("dbo.TelefonesClinica", new[] { "IdAmostraClinica" });
            DropIndex("dbo.TelefonesClinica", new[] { "IdClinica" });
            DropIndex("dbo.Foto", new[] { "IdAmostraClinica" });
            DropIndex("dbo.Foto", new[] { "IdClinica" });
            DropIndex("dbo.Estado", new[] { "Regiao" });
            DropIndex("dbo.Municipio", new[] { "Estado_Id" });
            DropIndex("dbo.Avaliacao", new[] { "IdUsuario" });
            DropIndex("dbo.Avaliacao", new[] { "IdClinica" });
            DropIndex("dbo.Clinica", new[] { "IdBairro" });
            DropIndex("dbo.Clinica", new[] { "IdCidade" });
            DropIndex("dbo.Clinica", new[] { "IdEstado" });
            DropIndex("dbo.AmostraClinica", new[] { "IdUsuario" });
            DropIndex("dbo.AmostraClinica", new[] { "IdBairro" });
            DropIndex("dbo.AmostraClinica", new[] { "IdCidade" });
            DropIndex("dbo.AmostraClinica", new[] { "IdEstado" });
            DropTable("dbo.ServicoClinicas");
            DropTable("dbo.ServicoAmostraClinicas");
            DropTable("dbo.EspecialidadeClinicas");
            DropTable("dbo.EspecialidadeAmostraClinicas");
            DropTable("dbo.UsuarioClinicas");
            DropTable("dbo.TelefonesClinica");
            DropTable("dbo.Servico");
            DropTable("dbo.Foto");
            DropTable("dbo.Regiao");
            DropTable("dbo.Estado");
            DropTable("dbo.Especialidade");
            DropTable("dbo.Municipio");
            DropTable("dbo.Usuario");
            DropTable("dbo.Avaliacao");
            DropTable("dbo.Clinica");
            DropTable("dbo.Bairro");
            DropTable("dbo.AmostraClinica");
        }
    }
}
