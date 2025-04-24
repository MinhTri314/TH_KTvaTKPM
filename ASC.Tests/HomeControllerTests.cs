using System;
using ASC_Web.Controllers;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ASC_Web.Configuration;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ASC.Tests.TestUtilities;
using ASC.Utilities;

namespace ASC.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<IOptions<ApplicationSetting>> optionsMock;
        private readonly Mock<HttpContext> mockHttpContext;
        public HomeControllerTests()
        {
            optionsMock = new Mock<IOptions<ApplicationSetting>>();
            mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(p => p.Session).Returns(new FakeSession());
            optionsMock.Setup(ap => ap.Value).Returns(new ApplicationSetting
            {
                ApplicationTitle = "ASC"
            });
        }
        [Fact]
        public void HomeController_Index_View_Test()
        {
            var controller = new HomeController(optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;
            controller.Index();
            Assert.NotNull(controller.HttpContext.Session.GetSession<ApplicationSetting>("Test"));
        }
        [Fact]
        public void HomeController_Index_NoModel_Test()
        {
            var controller = new HomeController(optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;
            Assert.Null((controller.Index() as ViewResult).ViewData.Model);
        }
        [Fact]
        public void HomeController_Index_Validation_Test()
        {
            var controller = new HomeController(optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;
            Assert.Equal(0, (controller.Index() as ViewResult).ViewData.ModelState.ErrorCount);
        }
    }
}
