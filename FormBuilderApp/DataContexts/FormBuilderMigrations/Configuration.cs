namespace FormBuilderApp.DataContexts.FormBuilderMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FormBuilderApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FormBuilderDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\FormBuilderMigrations";
        }

        protected override void Seed(FormBuilderDb context)
        {
            context.form.AddOrUpdate(f => f.Id,
                new Form
                {
                    Id = 1,
                    Name = "Survey",
                    Status = Form.FormStatus.Template,
                    FormData = "<form action='action_page.php'>First name:<br><input type='text' name='firstname' value='Mickey'><br>Last name:<br><input type='text' name='lastname' value='Mouse'><br><br><input type='submit' value='Submit'></form>",
                    WorkflowId = 3,
                },

                new Form
                {
                    Id = 2,
                    ParentId = 1,
                    Name = "Survey",
                    Status = Form.FormStatus.Draft,
                    FormData = "<form action='action_page.php'>First name:<br><input type='text' name='firstname' value='Ellen'><br>Last name:<br><input type='text' name='lastname' value=''><br><br><input type='submit' value='Submit'></form>",
                    WorkflowId = 3,
                },

                new Form
                {
                    Id = 3,
                    ParentId = 1,
                    Name = "Survey",
                    Status = Form.FormStatus.Completed,
                    FormData = "<form action='action_page.php'>First name:<br><input type='text' name='firstname' value='Ellen'><br>Last name:<br><input type='text' name='lastname' value=''><br><br><input type='submit' value='Submit'></form>",
                    WorkflowId = 3,
                },

                new Form
                {
                    Id = 4,
                    ParentId = 1,
                    Name = "Survey",
                    Status = Form.FormStatus.Accepted,
                    FormData = "<form action='action_page.php'>First name:<br><input type='text' name='firstname' value='Ellen'><br>Last name:<br><input type='text' name='lastname' value=''><br><br><input type='submit' value='Submit'></form>",
                    WorkflowId = null,
                },

                new Form
                {
                    Id = 5,
                    ParentId = 1,
                    Name = "Survey",
                    Status = Form.FormStatus.Denied,
                    FormData = "<form action='action_page.php'>First name:<br><input type='text' name='firstname' value='Ellen'><br>Last name:<br><input type='text' name='lastname' value=''><br><br><input type='submit' value='Submit'></form>",
                    WorkflowId = 3,
                }


            );

            context.flow.AddOrUpdate(f => f.FlowId,
                new Workflow
                {
                    FlowId = 3,
                    FormId = 1,
                    Input = new Positions()
                    {
                        Id = 1,
                        Position = "CEO"
                    }
                }
            );


        }
    }
}
