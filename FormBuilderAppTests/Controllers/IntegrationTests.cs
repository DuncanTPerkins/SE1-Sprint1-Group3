using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormBuilderApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
//using System.Web.Mvc;
using FormBuilderApp.Models;
using System.ComponentModel;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity;
using System.Security.Principal;
using System.Web;
//using System.Data.Entity;
//using System.Web.Routing;


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

        // Bottom-Up: Test Position Class
        [TestMethod()]
        public void Test_Position_Valid()
        {
            Form form = new Form();
            Workflow flow = new Workflow();
            Positions positions = new Positions()
            {
                Id = 3,
                Position = "CEO"
            };
            Assert.IsNotNull(positions);
        }

        [TestMethod()]
        public void Test_Position_Invalid_PositionIdZero()
        {
            Form form = new Form();
            Workflow flow = new Workflow();
            Positions positions = new Positions()
            {
                Id = 0,
                Position = "CEO"
            };

            Assert.IsNotNull(positions);
            Assert.AreEqual(positions.Id, 0);

        }

        [TestMethod()]
        public void Test_Position_Invalid_TwoPositionsSameId()
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

        // Bottom-Up: Test Workflow Class
        [TestMethod()]
        public void Test_Workflow_Valid()
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
        public void Test_Workflow_Invalid_FormWorkflowNull()
        {
            Form form = new Form();
            Workflow flow = new Workflow()
            {
                FlowId = 7,
                FormId = 3
            };

            Assert.AreEqual(form.flow, null);
        }

        [TestMethod()]
        public void Test_Workflow_Invalid_WorkflowsAreTheSame()
        {
            Workflow work = new Workflow();
            Workflow work1 = new Workflow();

            var w1 = work.ToString();
            var w2 = work1.ToString();

            bool stat = w1.Equals(w2);
            Assert.IsTrue(stat);
        }

        [TestMethod()]
        public void Test_Workflow_Invalid_PositionNotCompleted()
        {
            Workflow work = new Workflow();

            Assert.AreEqual(work.Input, null);
        }

        // Bottom-Up: Test Form Class
        [TestMethod()]
        public void Test_Form_Valid_WorkflowIncluded()
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
        public void Test_Form_Valid_WorkflowNotIncluded()
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
        public void Test_Form_Invalid_FormsContainSameInfo()
        {
            Form form = new Form();
            Form form1 = new Form();

            var f1 = form.ToString();
            var f2 = form1.ToString();

            bool stat = f1.Equals(f2);
            Assert.IsTrue(stat);
        }



        // Bottom-Up: Test AccountLogin
        [TestMethod()]
        public void test_ValidUser()
        {
            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("Admin");
            var principal = new GenericPrincipal(fakeIdentity, null);

            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            var controller = new AdminController();
            controller.ControllerContext = controllerContext.Object;

            var result = controller.CreateForm();

            Assert.IsNotNull(result);
            Assert.AreEqual(fakeIdentity.Name, "Admin");
            Assert.IsInstanceOfType(result, typeof(ViewResult));



        }

        [TestMethod()]
        public void test_InvalidUser()
        {
            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("User");
            var principal = new GenericPrincipal(fakeIdentity, null);

            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            var controller = new AdminController();
            controller.ControllerContext = controllerContext.Object;

            var result = controller.CreateForm();

            Assert.IsNotNull(result);
            Assert.AreEqual(fakeIdentity.Name, "User");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Error: Not an Admin");
        }

        /* [TestMethod()]
        public void test()
        {
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated).Returns(true); // or false

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new TestController();
            controller.ControllerContext =
                   new ControllerContext(context.Object, new RouteData(), controller);

            // test

            ViewResult viewResult = (ViewResult)controller.Login("Index");

            Assert.IsTrue(viewResult.ViewName == "Index");

        } */

    }

}