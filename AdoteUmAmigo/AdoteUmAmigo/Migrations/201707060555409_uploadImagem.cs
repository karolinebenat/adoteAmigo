namespace AdoteUmAmigo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uploadImagem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "Foto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pets", "Foto");
        }
    }
}
