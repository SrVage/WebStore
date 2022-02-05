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
			mockProductData.Setup(s => s.GetProduct(It.IsAny<ProductFilter>())).Returns<ProductFilter>(f => Enumerable.Empty<Product>());
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
	}
}

