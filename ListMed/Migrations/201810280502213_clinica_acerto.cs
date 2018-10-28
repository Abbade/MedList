namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clinica_acerto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clinica", "HoraAbertura", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Clinica", "HoraFechamento", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clinica", "HoraFechamento", c => c.String());
            AlterColumn("dbo.Clinica", "HoraAbertura", c => c.String());
        }
    }
}
