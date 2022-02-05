using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Controllers;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTests
	{
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

