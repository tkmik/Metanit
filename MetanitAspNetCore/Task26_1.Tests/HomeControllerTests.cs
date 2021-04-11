using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task26_1.Controllers;
using Task26_1.Models;
using Xunit;

namespace Task26_1.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewDataMessage()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.Equal("Hello!", result.ViewData["Message"]);
        }
        [Fact]
        public void IndexViewResultNull()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.Equal("Index", result?.ViewName);
        }
        [Fact]
        public void IndexReturnsAViewResultWithAListOfUsers()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestUsers());
            var controller = new HomeController(mock.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Model);
            Assert.Equal(GetTestUsers().Count, model.Count());
        }
        private List<User> GetTestUsers()
        {
            var users = new List<User>
            {
                new User { Id=1, Name="Tom", Age=35},
                new User { Id=2, Name="Alice", Age=29},
                new User { Id=3, Name="Sam", Age=32},
                new User { Id=4, Name="Kate", Age=30}
            };
            return users;
        }       
        [Fact]
        public void AddUserReturnsViewResultWithUserModel()
        {
            var mock = new Mock<IRepository>();
            var controller = new HomeController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            User newUser = new User();

            var result = controller.AddUser(newUser);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newUser, viewResult?.Model);
        }

        [Fact]
        public void AddUserReturnsARedirectAndAddsUser()
        {
            var mock = new Mock<IRepository>();
            var controller = new HomeController(mock.Object);
            var newUser = new User()
            {
                Name = "Ben"
            };

            var result = controller.AddUser(newUser);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(r => r.Create(newUser));
        }

        [Fact]
        public void GetUserReturnsBadRequestResultWhenIdIsNull()
        {
            var mock = new Mock<IRepository>();
            var controller = new HomeController(mock.Object);

            var result = controller.GetUser(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetUserReturnsNotFoundResultWhenUserNotFound()
        {
            int testUserId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.Get(testUserId))
                .Returns(null as User);
            var controller = new HomeController(mock.Object);

            var result = controller.GetUser(testUserId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetUserReturnsViewResultWithUser()
        {
            int testUserId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.Get(testUserId))
                .Returns(GetTestUsers().FirstOrDefault(p => p.Id == testUserId));
            var controller = new HomeController(mock.Object);

            var result = controller.GetUser(testUserId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<User>(viewResult.ViewData.Model);
            Assert.Equal("Tom", model.Name);
            Assert.Equal(35, model.Age);
            Assert.Equal(testUserId, model.Id);
        }
    }
}
