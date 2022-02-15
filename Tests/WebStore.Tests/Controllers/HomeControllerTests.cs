using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTests
	{
		[TestMethod]
		public void IndexReturnsViewResult()
        {
			var mockProductData = new Mock<IProductData>();
			mockProductData.Setup(s => s.GetProduct(It.IsAny<ProductFilter>())).Returns<ProductFilter>(f => new ProductsPage(Enumerable.Empty<Product>(), 0));
			var controller = new HomeController();
			var actualResult = controller.Index(mockProductData.Object);
			Assert.IsType<ViewResult>(actualResult);
		}

		[TestMethod]
		public void ConfiguredActionReturnsStringValue()
        {
			const string id = "12";
			const string value = "sfsdf";
			const string expectedString = $"Hello World! {id} - {value}";
			var controller = new HomeController();
			var actualString = controller.ConfiguredAction(id, value);
			Assert.Equal(expectedString, actualString);
        }

		[TestMethod, ExpectedException(typeof(ApplicationException))]
		public void ThrowThrownApplicationException()
        {
			const string message = "Message";
			var controller = new HomeController();
			controller.Throw(message);
        }

		[TestMethod]
		public void ThrowThrownApplicationExceptionWithMessage()
		{
			const string message = "Message";
			var controller = new HomeController();
			var applicationException = Assert.Throws<ApplicationException>(() => controller.Throw(message));
			var actualExceptionMessage = applicationException.Message;
			Assert.Equal(message, actualExceptionMessage);

		}

		[TestMethod]
		public void StatusWith404ReturnsRedirectToActionError404()
		{
			const string status404 = "404";
			const string expectedActionName = nameof(HomeController.Error);

			var controller = new HomeController();

			var result = controller.Status(status404);

			var redirectActionResult = Assert.IsType<RedirectToActionResult>(result);

			Assert.Null(redirectActionResult.ControllerName);

			var actual_action_name = redirectActionResult.ActionName;
			Assert.Equal(expectedActionName, actual_action_name);
		}
	}
}

