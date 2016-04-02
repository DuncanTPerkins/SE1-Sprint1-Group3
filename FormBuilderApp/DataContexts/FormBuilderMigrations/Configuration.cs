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

            context.Forms.AddOrUpdate(f => f.Id,

                new Form
                {
                    Name = "Student",
                    ParentId = 1,
                    FormData = "<h1>Status</h1>",   
                    Status = Form.FormStatus.Completed             
                },

                

                new Form
                {
                    Name = "Student",
                    ParentId = 1,
                    FormData = "<h1>Status Update</h1>",
                    Status = Form.FormStatus.Completed
                    
                }
            );
        }
    }
}
