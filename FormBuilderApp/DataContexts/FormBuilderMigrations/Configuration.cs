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
            context.Flow.AddOrUpdate(f => f.FormId,

                new Workflow
                {
                    FlowId = 1,
                    FormId = 4,
                    Positions = new List<Positions>
                        {
                            new Positions
                            {
                                Id = 1,
                            },

                            new Positions
                            {
                                Id = 2,
                            }
                        }
                },

                new Workflow
                {
                    FlowId = 2,
                    FormId = 1,
                    Positions = new List<Positions>
                        {
                           new Positions
                           {
                               Id = 2,
                           },

                           new Positions
                           {
                              Id = 3,
                           }
                       }
                }
           );
            context.Position.AddOrUpdate(f => f.Id,

                new Positions
                {
                    Id = 1,
                     Position = "President"

                 },

                 new Positions
                 {
                     Id = 2,
                     Position = "Manager"

                 },

                 new Positions
                 {
                     Id=3,
                     Position = "Dean"

                 },

                 new Positions
                 {
                     Id=4,
                     Position = "Chairman"

                 },

                 new Positions
                 {
                     Id=5,
                     Position = "Director"

                 }
             );

<<<<<<< HEAD
             
              context.Forms.AddOrUpdate(f => f.Id,

                new Form
                {
                    Name = "Student",
                    ParentId = 1,
                    FormData = "<h1>Student Page</h1><br />random stuff",
                    Status = Form.FormStatus.Completed,
                    flowId = 2
                },
=======
             ); */


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
            /*
              context.Forms.AddOrUpdate(f => f.Id,

                /*  new Form
                  {
                      Name = "Student",
                      ParentId = 40000,
                      FormData = "<h1>Status</h1>",   
                      Status = Form.FormStatus.Completed,
                      WorkflowId = 20000
                  }, 



            new Form
            {
                Name = "Survey",
                ParentId = 40001,
                FormData = "<h1>Status Update</h1>",
                Status = Form.FormStatus.Completed,
                WorkflowId = 20000
            }

                /*  new Form
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

>>>>>>> 0f4982b57b7253696dc3e1732ffccb28e127927a



                 new Form
                 {
                     Name = "Survey",
                     ParentId = 2,
                     FormData = "<h1>Survey Page</h1><br />random stuff",
                     Status = Form.FormStatus.Completed

                 },

                new Form
                {
                    Name = "Clubs",
                    ParentId = 3,
                    FormData = "<h1>Club Page</h1><br />random stuff",
                    Status = Form.FormStatus.Draft
                },



                 new Form
                 {
                     Name = "Hobbies",
                     ParentId = 4,
                     FormData = "<h1>Hobby Page</h1><br />random stuff",
                     Status = Form.FormStatus.Draft,
                     flowId = 2

                 },

              new Form
              {
                  Id = 1,
                  Name = "Student",
                  ParentId = null,
                  FormData = "<h1>Student Page</h1><br />random stuff",
                  Status = Form.FormStatus.Template,
                  flowId = 1
              },



              new Form
              {
                  Id=2,
                  Name = "Survey",
                  ParentId = null,
                  FormData = "<h1>Survey Page</h1><br />random stuff",
                  Status = Form.FormStatus.Template,
              },


              new Form
              {
                  Id=3,
                  Name = "Clubs",
                  ParentId = null,
                  FormData = "<h1>Club Page</h1><br />random stuff",
                  Status = Form.FormStatus.Template,
              },


              new Form
              {
                      Id=4,
                      Name = "Hobbies",
                      ParentId = null,
                      FormData = "<h1>Hobby Page</h1><br />random stuff",
                      Status = Form.FormStatus.Template,
                      flowId = 2
               },

              new Form
              {
                  Name = "Student",
                  ParentId = 1,
                  FormData = "<h1>Student Page</h1><br />random stuff",
                  Status = Form.FormStatus.Completed
              },


<<<<<<< HEAD
              new Form
                 {
                     Name = "Survey",
                     ParentId = 2,
                     FormData = "<h1>Survey Page</h1><br />random stuff",
                     Status = Form.FormStatus.Completed

                 }
          ); 

=======
                new Positions
                {
                    Id = 30004,
                    Position = "President"
                } 
            ); */
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Workflows', RESEED, 20000)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Positions', RESEED, 30000)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Forms', RESEED, 40000)");
>>>>>>> 0f4982b57b7253696dc3e1732ffccb28e127927a
        }
    }
}

