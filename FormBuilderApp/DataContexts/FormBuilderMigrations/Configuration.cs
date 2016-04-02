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
            /* context.Position.AddOrUpdate(p => p.Position,
                 new Positions
                 {
                     Id = 1,
                     Position = "CEO"

                 },

                 new Positions
                 {
                     Id = 1,
                     Position = "Manager"

                 },

                 new Positions
                 {
                     Id = 1,
                     Position = "Dean"

                 },

                 new Positions
                 {
                     Id = 1,
                     Position = "Chairman"

                 }
             );

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

             ); */

            context.Position.AddOrUpdate(
                  p => p.Position,
                  new Positions { Position = "CEO" }
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
                );

            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Workflows', RESEED, 10000)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Positions', RESEED, 20000)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Form', RESEED, 30000)");




        }
    }
}
