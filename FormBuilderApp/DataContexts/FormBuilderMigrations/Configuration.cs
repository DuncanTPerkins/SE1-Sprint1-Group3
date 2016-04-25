namespace FormBuilderApp.DataContexts.FormBuilderMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FormBuilderApp.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<FormBuilderDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
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
                    FormData = "<form name='genform'><div id='fields'><div class='form-group'><label for='Club Name'>Club Name</label><input class='form-control' type='text' name='Club Name' placeholder=''></div><div class='form-group'><label>Club Type</label><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Interests'> Interests</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Social'> Social</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Sports'> Sports</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Departmental'> Departmental</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Arts'> Arts</label></div></div><div class='form-group'><label for='Staff Liaison'>Staff Liaison</label><input class='form-control' type='text' name='Staff Liaison' placeholder=''></div><div class='form-group'><label for='Charter'>Charter</label><textarea class='form-control' type='textarea' name='Charter' placeholder='' <='' div=''></textarea></div></div><button id='later' class='btn btn-danger' type='submit' name='submit' value='Draft'>Save Draft</button><button id='completed' class='btn btn-primary' type='submit' name='submit' value='Complete'>Submit</button></form>",
                    WorkflowId = 3,
                },

                new Form
                {
                    Id = 2,
                    ParentId = 1,
                    Name = "Survey",
                    Status = Form.FormStatus.Draft,
                    FormData = "<form name='genform'><div id='fields'><div class='form-group'><label for='Club Name'>Club Name</label><input class='form-control' type='text' name='Club Name' placeholder=''></div><div class='form-group'><label>Club Type</label><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Interests'> Interests</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Social'> Social</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Sports'> Sports</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Departmental'> Departmental</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Arts'> Arts</label></div></div><div class='form-group'><label for='Staff Liaison'>Staff Liaison</label><input class='form-control' type='text' name='Staff Liaison' placeholder=''></div><div class='form-group'><label for='Charter'>Charter</label><textarea class='form-control' type='textarea' name='Charter' placeholder='' <='' div=''></textarea></div></div><button id='later' class='btn btn-danger' type='submit' name='submit' value='Draft'>Save Draft</button><button id='completed' class='btn btn-primary' type='submit' name='submit' value='Complete'>Submit</button></form>",
                    FormObjectRepresentation = "[{'name':'Club Name','value':'Sculpting Club'},{'name':'Club Type','value':'Arts'},{'name':'Staff Liaison','value':'Burt Reynolds'},{'name':'Charter','value':''}]",
                    WorkflowId = 3,
                },

                new Form
                {
                    Id = 3,
                    ParentId = 1,
                    Name = "Survey",
                    Status = Form.FormStatus.Completed,
                    FormData = "<form name='genform'><div id='fields'><div class='form-group'><label for='Club Name'>Club Name</label><input class='form-control' type='text' name='Club Name' placeholder=''></div><div class='form-group'><label>Club Type</label><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Interests'> Interests</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Social'> Social</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Sports'> Sports</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Departmental'> Departmental</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Arts'> Arts</label></div></div><div class='form-group'><label for='Staff Liaison'>Staff Liaison</label><input class='form-control' type='text' name='Staff Liaison' placeholder=''></div><div class='form-group'><label for='Charter'>Charter</label><textarea class='form-control' type='textarea' name='Charter' placeholder='' <='' div=''></textarea></div></div><button id='later' class='btn btn-danger' type='submit' name='submit' value='Draft'>Save Draft</button><button id='completed' class='btn btn-primary' type='submit' name='submit' value='Complete'>Submit</button></form>",
                    FormObjectRepresentation = "[{'name':'Club Name','value':'Sculpting Club'},{'name':'Club Type','value':'Arts'},{'name':'Staff Liaison','value':'Burt Reynolds'},{'name':'Charter','value':'A  bunch of rules and stuff here.'}]",
                    WorkflowId = 3,
                },

                new Form
                {
                    Id = 4,
                    ParentId = 1,
                    Name = "Survey",
                    Status = Form.FormStatus.Accepted,
                    FormData = "<form name='genform'><div id='fields'><div class='form-group'><label for='Club Name'>Club Name</label><input class='form-control' type='text' name='Club Name' placeholder=''></div><div class='form-group'><label>Club Type</label><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Interests'> Interests</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Social'> Social</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Sports'> Sports</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Departmental'> Departmental</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Arts'> Arts</label></div></div><div class='form-group'><label for='Staff Liaison'>Staff Liaison</label><input class='form-control' type='text' name='Staff Liaison' placeholder=''></div><div class='form-group'><label for='Charter'>Charter</label><textarea class='form-control' type='textarea' name='Charter' placeholder='' <='' div=''></textarea></div></div><button id='later' class='btn btn-danger' type='submit' name='submit' value='Draft'>Save Draft</button><button id='completed' class='btn btn-primary' type='submit' name='submit' value='Complete'>Submit</button></form>",
                    FormObjectRepresentation = "[{'name':'Club Name','value':'Sculpting Club'},{'name':'Club Type','value':'Arts'},{'name':'Staff Liaison','value':'Burt Reynolds'},{'name':'Charter','value':'A  bunch of rules and stuff here.'}]",
                    WorkflowId = null,
                },

                new Form
                {
                    Id = 5,
                    ParentId = 1,
                    Name = "Survey",
                    Status = Form.FormStatus.Denied,
                    FormData = "<form name='genform'><div id='fields'><div class='form-group'><label for='Club Name'>Club Name</label><input class='form-control' type='text' name='Club Name' placeholder=''></div><div class='form-group'><label>Club Type</label><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Interests'> Interests</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Social'> Social</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Sports'> Sports</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Departmental'> Departmental</label></div><div class='radio'><label><input class='radio' type='radio' name='Club Type' value='Arts'> Arts</label></div></div><div class='form-group'><label for='Staff Liaison'>Staff Liaison</label><input class='form-control' type='text' name='Staff Liaison' placeholder=''></div><div class='form-group'><label for='Charter'>Charter</label><textarea class='form-control' type='textarea' name='Charter' placeholder='' <='' div=''></textarea></div></div><button id='later' class='btn btn-danger' type='submit' name='submit' value='Draft'>Save Draft</button><button id='completed' class='btn btn-primary' type='submit' name='submit' value='Complete'>Submit</button></form>",
                    FormObjectRepresentation = "[{'name':'Club Name','value':'Sculpting Club'},{'name':'Club Type','value':'Arts'},{'name':'Staff Liaison','value':'Burt Reynolds'},{'name':'Charter','value':'A  bunch of rules and stuff here.'}]",
                    WorkflowId = 3,
                    DenyReason = "Club already exists"
                }
            );


            context.position.AddOrUpdate(f => f.Id,
                        new Positions
                        {
                            Id = 1,
                            Position = "CEO"
                        },

                        new Positions
                        {
                            Id = 2,
                            Position = "Dean"
                        },

                        new Positions
                        {
                            Id = 3,
                            Position = "Chairman"
                        },

                        new Positions
                        {
                            Id = 4,
                            Position = "Manager"
                        },

                        new Positions
                        {
                            Id = 5,
                            Position = "Project Manager"
                        },

                        new Positions
                        {
                            Id = 6,
                            Position = "Team Leader"
                        }
                    
            );

            context.flow.AddOrUpdate(f => f.FlowId,
                new Workflow
                {
                    FlowId = 3,
                    FormId = 1,
                }
            );
        }
    }
}
