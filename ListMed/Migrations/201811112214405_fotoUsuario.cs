namespace ListMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fotoUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Foto", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Foto");
        }
    }
}
