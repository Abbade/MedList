namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Localidade_acerto2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clinica", "HoraAbertura", c => c.String());
            AlterColumn("dbo.Clinica", "HoraFechamento", c => c.String());
            DropColumn("dbo.Clinica", "abertoAgora");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clinica", "abertoAgora", c => c.Boolean());
            AlterColumn("dbo.Clinica", "HoraFechamento", c => c.DateTime());
            AlterColumn("dbo.Clinica", "HoraAbertura", c => c.DateTime());
        }
    }
}
