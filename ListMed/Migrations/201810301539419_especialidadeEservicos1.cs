namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class especialidadeEservicos1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clinica", "PrecoConsulta", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Clinica", "PrecoExame", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Clinica", "Preco");
            DropColumn("dbo.Servico", "preco");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Servico", "preco", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Clinica", "Preco", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Clinica", "PrecoExame");
            DropColumn("dbo.Clinica", "PrecoConsulta");
        }
    }
}
