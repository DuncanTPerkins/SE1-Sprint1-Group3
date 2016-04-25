using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormBuilderApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Web.Mvc;
using FormBuilderApp.Models;
using System.ComponentModel;
using System.Security.Principal;
using System.Web;
using System.Web.Routing;

namespace FormBuilderApp.Models.Tests
{

    /***************************************
                INTEGRATION TESTS
              testing the form class
           with the following classes
          workflow, positions, account
    ***************************************/

    [TestClass()]
    public class IntegrationTests
    {

        /***************************************
                    Position Class
        ***************************************/
        [TestMethod()]
        public void IntegrationTest_Position_Valid()
        {
            Form form = new Form()
            {
                flow = new Workflow()
                {
                    Input = new Positions()
                    {
                        Id = 3,
                        Position = "CEO"
                    }
                }
            };

            Assert.IsNotNull(form.flow.Input);
        }

        [TestMethod()]
        public void IntegrationTest_Position_Invalid_PositionIdZero()
        {
            Form form = new Form()
            {
                flow = new Workflow()
                {
                    Input = new Positions()
                    {
                        Id = 0,
                        Position = "CEO"
                    }
                }
            };

            Assert.IsNotNull(form.flow.Input);
            Assert.AreEqual(0, form.flow.Input.Id);

        }

        [TestMethod()]
        public void IntegrationTest_Position_Invalid_TwoPositionsSameId()
        {
            Form form = new Form();
            Workflow flow = new Workflow();
            Positions positions = new Positions();
            Positions positions1 = new Positions();

            var pos1 = positions.ToString();
            var pos2 = positions1.ToString();

            bool stat = pos1.Equals(pos2);

            Assert.IsTrue(stat);

        }

        /***************************************
                    Workflow Class
        ***************************************/

        [TestMethod()]
        public void IntegrationTest_Workflow_Valid()
        {
            Form form = new Form()
            {
                flow = new Workflow()
                {
                    FlowId = 3,
                    FormId = 5,
                    Input = new Positions()
                    {
                        Id = 5,
                        Position = "CEO"
                    }
                }
            };

            Assert.IsNotNull(form.flow);
            Assert.IsNotNull(form.flow.Input);
        }

        [TestMethod()]
        public void IntegrationTest_Workflow_Invalid_PositionsNull()
        {
            Form form = new Form()
            {
                flow = new Workflow()
                {
                    FlowId = 3,
                    FormId = 5,
                }
            };

            Assert.IsNotNull(form.flow);
            Assert.AreEqual(null, form.flow.Input);
        }

        [TestMethod()]
        public void IntegrationTest_Workflow_Invalid_WorkflowsAreTheSame()
        {
            Workflow work = new Workflow();
            Workflow work1 = new Workflow();

            var w1 = work.ToString();
            var w2 = work1.ToString();

            bool stat = w1.Equals(w2);
            Assert.IsTrue(stat);
        }


        /***************************************
                    Form Class
        ***************************************/
        [TestMethod()]
        public void IntegrationTest_Form_Valid_WorkflowIncluded()
        {
            Form form = new Form()
            {
                Id = 3,
                Name = "Survey",
                Status = Form.FormStatus.Template,
                ParentId = null,
                UserId = null,
                WorkflowId = 7,
                FormData = "",
                FormObjectRepresentation = null,
                flow = new Workflow()
                {
                    FormId = 3,
                    FlowId = 7,
                    Input = new Positions()
                    {
                        Id = 3,
                        Position = "CEO"
                    }
                }
            };
            Assert.IsNotNull(form);
            Assert.IsNotNull(form.flow);
            Assert.IsNotNull(form.flow.Input);
        }

        [TestMethod()]
        public void IntegrationTest_Form_Valid_WorkflowNotIncluded()
        {
            Form form = new Form()
            {
                Id = 3,
                Name = "Survey",
                Status = Form.FormStatus.Template,
                ParentId = null,
                UserId = null,
                WorkflowId = 7,
                FormData = null,
                FormObjectRepresentation = "<h1>Survey</h1>",
                flow = null
            };

            Assert.IsNotNull(form);
            Assert.IsNull(form.flow);
        }

        [TestMethod()]
        public void IntegrationTest_Form_Invalid_FormsContainSameInfo()
        {
            Form form = new Form();
            Form form1 = new Form();

            var f1 = form.ToString();
            var f2 = form1.ToString();

            bool stat = f1.Equals(f2);
            Assert.IsTrue(stat);
        }



        /***************************************
                AccountViewModel Class
        ***************************************/

            //ExternalLoginConfirmationModel
        [TestMethod()]
        public void IntegrationTest_ExternalLoginConfirmationModel_Valid()
        {
            ExternalLoginConfirmationViewModel ex = new ExternalLoginConfirmationViewModel();

            ex.Email = "test@test.com";

            Assert.AreEqual(ex.Email, "test@test.com");
        }

        [TestMethod()]
        public void IntegrationTest_ExternalLoginConfirmationModel_Invalid_WrongEmail()
        {
            ExternalLoginConfirmationViewModel ex = new ExternalLoginConfirmationViewModel();

            ex.Email = "admin@test.com";

            Assert.AreNotEqual(ex.Email, "test@test.com");
        }

            // LoginViewModel
        [TestMethod()]
        public void IntegrationTest_LogInViewModel_Valid()
        {
            LoginViewModel login = new LoginViewModel();

            login.Email = "test@test.com";
            login.Password = "Pass1!";
            login.RememberMe = true;

            Assert.AreEqual(login.Email, "test@test.com");
            Assert.AreEqual(login.Password, "Pass1!");
            Assert.IsTrue(login.RememberMe);
        }

        [TestMethod()]
        public void IntegrationTest_LoginViewModel_Invalid_WrongUser()
        {
            LoginViewModel login = new LoginViewModel();
            login.Email = "admin@test.com";
            login.Password = "Pass1!";
            login.RememberMe = true;

            Assert.AreNotEqual(login.Email, "test@test.com");
            Assert.AreEqual(login.Password, "Pass1!");
            Assert.IsTrue(login.RememberMe);
        }

        [TestMethod()]
        public void IntegrationTest_LoginViewModel_Invalid_WrongPassword()
        {
            LoginViewModel login = new LoginViewModel();
            login.Email = "test@test.com";
            login.Password = "Pass!";
            login.RememberMe = true;

            Assert.AreEqual(login.Email, "test@test.com");
            Assert.AreNotEqual(login.Password, "Pass1!");
            Assert.IsTrue(login.RememberMe);
        }

            // ResetPasswordViewModel
        [TestMethod()]
        public void IntegrationTest_ResetPasswordViewModel_Valid()
        {
            ResetPasswordViewModel reset = new ResetPasswordViewModel();
            reset.Password = "Pass1!";
            reset.ConfirmPassword = "Pass1!";
            reset.Email = "test@test.com";
            reset.Code = "1234";

            Assert.AreEqual(reset.Email, "test@test.com");
            Assert.AreSame(reset.Password, reset.ConfirmPassword);
            Assert.AreEqual(reset.Code, "1234");
        }

        [TestMethod()]
        public void IntegrationTest_ResetPasswordViewModel_Invalid_WrongEmail()
        {
            ResetPasswordViewModel reset = new ResetPasswordViewModel();
            reset.Password = "Pass1!";
            reset.ConfirmPassword = "Pass1!";
            reset.Email = "admin@test.com";
            reset.Code = "1234";

            Assert.AreNotEqual(reset.Email, "test@test.com");
            Assert.AreSame(reset.Password, reset.ConfirmPassword);
            Assert.AreEqual(reset.Code, "1234");
        }

        [TestMethod()]
        public void IntegrationTest_ResetPasswordViewModel_Invalid_DifferentPasswords()
        {
            ResetPasswordViewModel reset = new ResetPasswordViewModel();
            reset.Password = "Pass1!";
            reset.ConfirmPassword = "Pass!";
            reset.Email = "test@test.com";
            reset.Code = "1234";

            Assert.AreEqual(reset.Email, "test@test.com");
            Assert.AreNotSame(reset.Password, reset.ConfirmPassword);
            Assert.AreEqual(reset.Code, "1234");
        }

        [TestMethod()]
        public void IntegrationTest_ResetPasswordViewModel_Invalid_WrongCode()
        {
            ResetPasswordViewModel reset = new ResetPasswordViewModel();
            reset.Password = "Pass1!";
            reset.ConfirmPassword = "Pass1!";
            reset.Email = "test@test.com";
            reset.Code = "1234";

            Assert.AreEqual(reset.Email, "test@test.com");
            Assert.AreSame(reset.Password, reset.ConfirmPassword);
            Assert.AreNotEqual(reset.Code, "12345");
        }

            // RegisterViewModel
        [TestMethod()]
        public void IntegrationTest_RegisterViewModel_Valid()
        {
            RegisterViewModel register = new RegisterViewModel();
            register.Password = "Pass1!";
            register.ConfirmPassword = "Pass1!";
            register.Email = "test@test.com";

            string[] regEmails = { "superuser@test.com", "admin@test.com", "user@test.com" };

            CollectionAssert.DoesNotContain(regEmails, register.Email);
            Assert.AreSame(register.Password, register.ConfirmPassword);
        }

        [TestMethod()]
        public void IntegrationTest_RegisterViewModel_Invalid_DifferentPasswords()
        {
            RegisterViewModel register = new RegisterViewModel();
            register.Password = "Pass1!";
            register.ConfirmPassword = "Pass!";
            register.Email = "test@test.com";

            Assert.AreEqual(register.Email, "test@test.com");
            Assert.AreNotSame(register.Password, register.ConfirmPassword);
        }

        [TestMethod()]
        public void IntegrationTest_RegisterViewModel_Invalid_TakenUsername()
        {
            RegisterViewModel register = new RegisterViewModel();
            register.Password = "Pass1!";
            register.ConfirmPassword = "Pass1!";
            register.Email = "test@test.com";

            string[] regEmails = {"test@test.com","admin@test.com","user@test.com" };

            CollectionAssert.Contains(regEmails, register.Email);
            Assert.AreSame(register.Password, register.ConfirmPassword);
        }

        // ForgotPasswordViewModel
        [TestMethod()]
        public void IntegrationTest_ForgotPasswordViewModel_Valid()
        {
            ForgotPasswordViewModel pass = new ForgotPasswordViewModel();

            pass.Email = "test@test.com";

            Assert.AreEqual("test@test.com", pass.Email);
        }

        [TestMethod()]
        public void IntegrationTest_ForgotPasswordViewModel_Invalid_WrongEmail()
        {
            ForgotPasswordViewModel pass = new ForgotPasswordViewModel();

            pass.Email = "admin@test.com";

            Assert.AreNotEqual("test@test.com", pass.Email);
        }

    }

}