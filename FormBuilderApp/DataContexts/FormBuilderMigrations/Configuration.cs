namespace FormBuilderApp.DataContexts.FormBuilderMigrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FormBuilderApp.DataContexts.FormBuilderDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"DataContexts\FormBuilderMigrations";
        }

        protected override void Seed(FormBuilderApp.DataContexts.FormBuilderDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Forms.AddOrUpdate(f => f.City, new Form
            //{
            //    City = "Johnson City",
            //    State = "Tennessee",
            //    Name = "Bob"
            //});

          /*  context.Forms.AddOrUpdate(f => f.Id,

                new Form
                {
                    Name = "Student",
                    ParentId = 40000,
                    FormData = "<h1>Status</h1>",   
                    Status = Form.FormStatus.Completed,
                    WorkflowId = 20000
                },

                

                new Form
                {
                    Name = "Student",
                    ParentId = 40000,
                    FormData = "<h1>Status Update</h1>",
                    Status = Form.FormStatus.Completed,
                    WorkflowId = 20000
                },

                new Form
                {
                    Name = "Survey",
                    ParentId = 40001,
                    FormData = "<h1>Status</h1>",
                    Status = Form.FormStatus.Draft,
                    WorkflowId = 20001
                },



                new Form
                {
                    Name = "Survey",
                    ParentId = 40001,
                    FormData = "<h1>Status Update</h1>",
                    Status = Form.FormStatus.Draft,
                    WorkflowId = 20001
                },

                new Form
                {
                    Name = "Student",
                    ParentId = 40000,
                    FormData = "<h1>Student</h1>",
                    Status = Form.FormStatus.Template,
                    WorkflowId = 20000
                },



                new Form
                {
                    Name = "Survey",
                    ParentId = 40001,
                    FormData = "<h1>Survey</h1>",
                    Status = Form.FormStatus.Template,
                    WorkflowId = 20001
                },

                new Form
                {
                    Name = "Student",
                    ParentId = 40000,
                    FormData = "<h1>Status</h1>",
                    Status = Form.FormStatus.Accepted,
                },



                new Form
                {
                    Name = "Survey",
                    ParentId = 40001,
                    FormData = "<h1>Status Update</h1>",
                    Status = Form.FormStatus.Accepted,
                }


            ); */

            context.Flow.AddOrUpdate(f => f.FlowId,
                new Workflow
                {
                    FlowId = 20000,
                    Positions = new List<Positions>
                    {
                        new Positions
                        {
                            Id = 300000,
                            Position = "CEO"
                        },

                        new Positions
                        {
                            Id = 300001,
                            Position = "Manager"
                        }
                    }
                },

                new Workflow
                {
                    FlowId = 20001,
                    Positions = new List<Positions>
                    {
                        new Positions
                        {
                            Id = 300001,
                            Position = "Manager"
                        },

                        new Positions
                        {
                            Id = 300002,
                            Position = "Chairman"
                        }
                    }
                }
            );

            context.Posiition.AddOrUpdate(f => f.Id,
                new Positions
                {
                    Id = 30000,
                    Position = "CEO"
                },

                new Positions
                {
                    Id = 30001,
                    Position = "Manager"
                },

                new Positions
                {
                    Id = 30002,
                    Position = "Chairman"
                },


                new Positions
                {
                    Id = 30003,
                    Position = "Dean"
                },

                new Positions
                {
                    Id = 30004,
                    Position = "President"
                }
            );

context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Workflows', RESEED, 20000)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Positions', RESEED, 30000)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Forms', RESEED, 40000)");
        }
    }
}
