using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using CourseProject.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectFirst.Tests.Controllers
{
    [TestClass]
    public class SubjectsControllerTest
    {
        private SubjectsController controller;
        private ViewResult result;

        [TestInitialize]
        public void SetupContext()
        {
            controller = new SubjectsController();
            result = controller.Index() as ViewResult;
        }

        [TestMethod]
        public void IndexViewResultNotNull()
        {
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
