namespace FormBuilderApp.DataContexts.FormBuilderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "DenyReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forms", "DenyReason");
        }
    }
}
