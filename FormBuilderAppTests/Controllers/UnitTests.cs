using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormBuilderApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FormBuilderApp.Models.Tests
{
    /***************************************
                 UNIT TESTS
            testing the Form class
    ***************************************/

    [TestClass()]
    public class UnitTests
    {

        // Create Forms
        [TestMethod()]
        public void UnitTests_CreateForm_Invalid_EmptyForm()
        {
            Form form = new Form();

            Assert.IsNotNull(form);
            Assert.AreEqual(null, form.WorkflowId);
            Assert.AreEqual(null, form.UserId);
            Assert.AreEqual(null, form.ParentId);
            Assert.AreEqual(null, form.Name);
            Assert.AreEqual(0, form.Id);
            Assert.AreEqual(null, form.FormObjectRepresentation);
            Assert.AreEqual(null, form.FormData);
            Assert.AreEqual(null, form.flow);
            Assert.AreEqual(0, Convert.ToInt32(form.Status));
        }


        [TestMethod()]
        public void UnitTests_CreateForm_Valid()
        {
            Form form = new Form()
            {
                flow = null,
                FormData = null,
                FormObjectRepresentation = null,
                Name = "Test",
                Id = 5,
                ParentId = null,
                Status = Form.FormStatus.Template,
                UserId = null,
                WorkflowId = null
            };

            Assert.IsNotNull(form);
        }

        [TestMethod()]
        public void UnitTests_AddWorkflow_Valid()
        {
            Form form = new Form();
            form.flow = new Workflow
            {
                FlowId = 2,
                FormId = 12,
                Input = new Positions
                {
                    Id = 2,
                    Position = "Manager"
                }
            };
            Assert.IsNotNull(form.flow);
            Assert.IsNotNull(form.flow.Input);
        }

        [TestMethod()]
        public void UnitTests_AddWorkflow_Invalid_NoPositionsAdded()
        {
            Form form = new Form();
            form.flow = new Workflow
            {
                FlowId = 2,
                FormId = 12
            };
            Assert.IsNotNull(form.flow);
            Assert.IsNull(form.flow.Input);
        }

        // View Forms
        [TestMethod()]
        public void UnitTests_ViewForms_Valid()
        {
            var stat = FormBuilderApp.Models.Form.FormStatus.Completed;
            Assert.AreEqual(FormBuilderApp.Models.Form.FormStatus.Completed, stat);
        }


        [TestMethod()]
        public void UnitTests_ViewForms_Invalid_WrongFormType()
        {
            var stat = FormBuilderApp.Models.Form.FormStatus.Template;
            Assert.AreNotEqual(FormBuilderApp.Models.Form.FormStatus.Completed, stat);
        }

        // Deny Forms
        [TestMethod()]
        public void UnitTests_DenyForms_Valid()
        {
            var role = "Super User";
            Form forms = new Form()
            {
                Status = Form.FormStatus.Completed
            };

            var expected = (Form.FormStatus.Denied).ToString();

            Assert.AreEqual(role, "Super User");
            Assert.AreEqual(forms.Status, Form.FormStatus.Completed);
            Assert.AreEqual(expected, "Denied");

        }

        [TestMethod()]
        public void UnitTests_DenyForms_Invalid_FormNotCompleted()
        {
            var role = "Super User";
            Form forms = new Form()
            {
                Status = Form.FormStatus.Draft
            };

            Assert.AreEqual(role, "Super User");
            Assert.AreNotEqual(forms.Status, "Completed");

        }

        //Accept Forms
        [TestMethod()]
        public void UnitTests_DenyForms_Invalid_WrongUserRole()
        {
            var role = "User";
            Form forms = new Form()
            {
                Status = Form.FormStatus.Completed
            };

            Assert.AreNotEqual(role, "Super User");
            Assert.AreEqual(forms.Status, Form.FormStatus.Completed);
        }

        [TestMethod()]
        public void UnitTests_AcceptForms_Valid()
        {
            var role = "Super User";
            Form forms = new Form()
            {
                Status = Form.FormStatus.Completed
            };

            var expected = (Form.FormStatus.Accepted).ToString();

            Assert.AreEqual(role, "Super User");
            Assert.AreEqual(forms.Status, Form.FormStatus.Completed);
            Assert.AreEqual(expected, "Accepted");

        }

        [TestMethod()]
        public void UnitTests_AcceptForms_Invalid_FormNotCompleted()
        {
            var role = "Super User";
            Form forms = new Form()
            {
                Status = Form.FormStatus.Draft
            };

            var expected = (Form.FormStatus.Accepted).ToString();

            Assert.AreEqual(role, "Super User");
            Assert.AreNotEqual(forms.Status, "Completed");
            Assert.AreEqual(expected, "Accepted");

        }

        [TestMethod()]
        public void UnitTests_AcceptForms_Invalid_WrongUserRole()
        {
            var role = "User";
            Form forms = new Form()
            {
                Status = Form.FormStatus.Completed
            };

            var expected = (Form.FormStatus.Accepted).ToString();

            Assert.AreNotEqual(role, "Super User");
            Assert.AreEqual(forms.Status, Form.FormStatus.Completed);
            Assert.AreEqual(expected, "Accepted");

        }


    }
}

