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
             context.Position.AddOrUpdate(p => p.Position,
                 new Positions
                 {
                     Id = 30000,
                     Position = "President"

                 },

                 new Positions
                 {
                     Id = 30001,
                     Position = "Manager"

                 },

                 new Positions
                 {
                     Id = 30002,
                     Position = "Dean"

                 },

                 new Positions
                 {
                     Id = 30003,
                     Position = "Chairman"

                 },

                 new Positions
                 {
                     Id = 30004,
                     Position = "Director"

                 }
             );

             
              context.Forms.AddOrUpdate(f => f.Id,

                new Form
                {
                    Name = "Student",
                    ParentId = 40000,
                    FormData = "<h1>Student Page</h1><br />random stuff",
                    Status = Form.FormStatus.Completed
                },



                 new Form
                 {
                     Name = "Survey",
                     ParentId = 40001,
                     FormData = "<h1>Survey Page</h1><br />random stuff",
                     Status = Form.FormStatus.Completed

                 },

                new Form
                {
                    Name = "Clubs",
                    ParentId = 40002,
                    FormData = "<h1>Club Page</h1><br />random stuff",
                    Status = Form.FormStatus.Draft
                },



                 new Form
                 {
                     Name = "Hobbies",
                     ParentId = 40003,
                     FormData = "<h1>Hobby Page</h1><br />random stuff",
                     Status = Form.FormStatus.Draft

                 },

              new Form
              {
                  Name = "Student",
                  ParentId = null,
                  FormData = "<h1>Student Page</h1><br />random stuff",
                  Status = Form.FormStatus.Template,
                  WorkflowId = 20000
              },



              new Form
              {
                  Name = "Survey",
                  ParentId = null,
                  FormData = "<h1>Survey Page</h1><br />random stuff",
                  Status = Form.FormStatus.Template,
                  WorkflowId = 20001
              },


              new Form
              {
                  Name = "Clubs",
                  ParentId = null,
                  FormData = "<h1>Club Page</h1><br />random stuff",
                  Status = Form.FormStatus.Template,
                  WorkflowId = 20002
              },


              new Form
              {
                  Name = "Hobbies",
                  ParentId = null,
                  FormData = "<h1>Hobby Page</h1><br />random stuff",
                  Status = Form.FormStatus.Template,
                  WorkflowId = 20003
              },

              new Form
              {
                  Name = "Student",
                  ParentId = 40000,
                  FormData = "<h1>Student Page</h1><br />random stuff",
                  Status = Form.FormStatus.Completed
              },


              new Form
                 {
                     Name = "Survey",
                     ParentId = 40001,
                     FormData = "<h1>Survey Page</h1><br />random stuff",
                     Status = Form.FormStatus.Completed

                 }
          ); 

        context.Flow.AddOrUpdate(f => f.FlowId,
            new Workflow
            {
                FlowId = 20000,
                Positions = new List<Positions>
                {
                    new Positions
                    {
                        Id = 300000,
                    },

                    new Positions
                    {
                        Id = 300001,
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
                    },

                    new Positions
                    {
                        Id = 300002,
                    }
                }
            }
        );
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Workflows', RESEED, 20000)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Positions', RESEED, 30000)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Forms', RESEED, 40000)");
        }
    }
}

