namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Localidade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Localidade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clinica", "PlaceID", c => c.String());
            AddColumn("dbo.Clinica", "avaliacao", c => c.Double(nullable: false));
            AddColumn("dbo.Clinica", "abertoAgora", c => c.Boolean());
            AddColumn("dbo.Clinica", "Telefone1", c => c.String(maxLength: 17));
            AddColumn("dbo.Clinica", "Telefone2", c => c.String(maxLength: 17));
            DropColumn("dbo.Clinica", "IdCidade");
            DropColumn("dbo.Clinica", "DescricaoCidade");
            DropColumn("dbo.Clinica", "IdBairro");
            DropColumn("dbo.Clinica", "DescricaoBairro");
            DropColumn("dbo.Clinica", "IdEstado");
            DropColumn("dbo.Clinica", "UF");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clinica", "UF", c => c.String());
            AddColumn("dbo.Clinica", "IdEstado", c => c.Int());
            AddColumn("dbo.Clinica", "DescricaoBairro", c => c.String());
            AddColumn("dbo.Clinica", "IdBairro", c => c.Int());
            AddColumn("dbo.Clinica", "DescricaoCidade", c => c.String());
            AddColumn("dbo.Clinica", "IdCidade", c => c.Int());
            DropColumn("dbo.Clinica", "Telefone2");
            DropColumn("dbo.Clinica", "Telefone1");
            DropColumn("dbo.Clinica", "abertoAgora");
            DropColumn("dbo.Clinica", "avaliacao");
            DropColumn("dbo.Clinica", "PlaceID");
            DropTable("dbo.Localidade");
        }
    }
}
