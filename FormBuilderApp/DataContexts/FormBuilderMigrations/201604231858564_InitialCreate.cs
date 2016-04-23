namespace FormBuilderApp.DataContexts.FormBuilderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workflows",
                c => new
                    {
                        FlowId = c.Int(nullable: false, identity: true),
                        FormId = c.Int(nullable: false),
                        Input_Id = c.Int(),
                    })
                .PrimaryKey(t => t.FlowId)
                .ForeignKey("dbo.Positions", t => t.Input_Id)
                .Index(t => t.Input_Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.String(),
                        Workflow_FlowId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workflows", t => t.Workflow_FlowId)
                .Index(t => t.Workflow_FlowId);
            
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ParentId = c.Int(),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        FormData = c.String(),
                        FormObjectRepresentation = c.String(),
                        WorkflowId = c.Int(),
                        flow_FlowId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workflows", t => t.flow_FlowId)
                .Index(t => t.flow_FlowId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Forms", "flow_FlowId", "dbo.Workflows");
            DropForeignKey("dbo.Positions", "Workflow_FlowId", "dbo.Workflows");
            DropForeignKey("dbo.Workflows", "Input_Id", "dbo.Positions");
            DropIndex("dbo.Forms", new[] { "flow_FlowId" });
            DropIndex("dbo.Positions", new[] { "Workflow_FlowId" });
            DropIndex("dbo.Workflows", new[] { "Input_Id" });
            DropTable("dbo.Forms");
            DropTable("dbo.Positions");
            DropTable("dbo.Workflows");
        }
    }
}
