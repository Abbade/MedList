namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clinic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clinica", "HoraAbertura", c => c.DateTime());
            AddColumn("dbo.Clinica", "HoraFechamento", c => c.DateTime());
            AlterColumn("dbo.Clinica", "NomeFantasia", c => c.String(maxLength: 180));
            AlterColumn("dbo.Clinica", "TituloSite", c => c.String(maxLength: 180));
            AlterColumn("dbo.Clinica", "LinkSite", c => c.String(maxLength: 80));
            AlterColumn("dbo.Clinica", "IdCidade", c => c.Int());
            AlterColumn("dbo.Clinica", "IdBairro", c => c.Int());
            AlterColumn("dbo.Clinica", "IdEstado", c => c.Int());
            AlterColumn("dbo.Clinica", "Preco", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clinica", "Preco", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Clinica", "IdEstado", c => c.Int(nullable: false));
            AlterColumn("dbo.Clinica", "IdBairro", c => c.Int(nullable: false));
            AlterColumn("dbo.Clinica", "IdCidade", c => c.Int(nullable: false));
            AlterColumn("dbo.Clinica", "LinkSite", c => c.String());
            AlterColumn("dbo.Clinica", "TituloSite", c => c.String());
            AlterColumn("dbo.Clinica", "NomeFantasia", c => c.String());
            DropColumn("dbo.Clinica", "HoraFechamento");
            DropColumn("dbo.Clinica", "HoraAbertura");
        }
    }
}
