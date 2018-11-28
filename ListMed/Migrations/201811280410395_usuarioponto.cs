namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuarioponto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Pontos", c => c.Int(nullable: false));
            AlterColumn("dbo.Clinica", "HoraAbertura", c => c.Time(precision: 7));
            AlterColumn("dbo.Clinica", "HoraFechamento", c => c.Time(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clinica", "HoraFechamento", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Clinica", "HoraAbertura", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Usuario", "Pontos");
        }
    }
}
