using JustBlog.Controllers;
using JustBlog.Providers;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JustBlog.Tests
{
    [TestFixture]
    public class AdminControllerTests
    {
        AdminController _adminController;
        IAuthProvider _authProvider;

        [SetUp]
        public void SetUp()
        {
            _authProvider = MockRepository.GenerateMock<IAuthProvider>();
            _adminController = new AdminController(_authProvider);

            var httpContextMock = MockRepository.GenerateMock<HttpContextBase>();
            _adminController.Url = new UrlHelper(new RequestContext(httpContextMock, new RouteData()));
        }

        [Test]
        public void Login_IsLoggedIn_True_Test()
        {
            //arrange
            _authProvider.Stub(s => s.IsLoggedIn).Return(true);

            //act
            var actual = _adminController.Login("/admin/manage");

            //assert
            Assert.IsInstanceOf<RedirectResult>(actual);
            Assert.AreEqual("/admin/manage", ((RedirectResult)actual).Url);
        }
    }
}
