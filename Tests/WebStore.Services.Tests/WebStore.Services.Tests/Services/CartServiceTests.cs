using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Services;
using Assert = Xunit.Assert;
namespace WebStore.Services.Tests.Services
{
    [TestClass]
	public class CartServiceTests
	{
        private Cart _cart;
        private Mock<IProductData> _productDataMock;
        private Mock<ICartStore> _cartStoreMock;
        private ICartService _cartService;

        [TestInitialize]
        public void TestInitialize()
        {
            _cart = new Cart
            {
                Items = new List<CartItem>
            {
                new() { ProductID = 1, Quantity = 1 },
                new() { ProductID = 2, Quantity = 3 },
            }
            };

            _productDataMock = new Mock<IProductData>();
            _productDataMock
               .Setup(c => c.GetProduct(It.IsAny<ProductFilter>()))
               .Returns(new[]
                {
                new Product
                {
                    ID = 1,
                    Name = "Product 1",
                    Price = 1.1m,
                    Order = 1,
                    ImageURL = "img_1.png",
                    Brand = new Brand { ID = 1, Name = "Brand 1", Order = 1},
                    SectionID = 1,
                    Section = new Section{ ID = 1, Name = "Section 1", Order = 1 },
                },
                new Product
                {
                    ID = 2,
                    Name = "Product 2",
                    Price = 2.2m,
                    Order = 2,
                    ImageURL = "img_2.png",
                    Brand = new Brand { ID = 2, Name = "Brand 2", Order = 2},
                    SectionID = 2,
                    Section = new Section{ ID = 2, Name = "Section 2", Order = 2 },
                },
                new Product
                {
                    ID = 3,
                    Name = "Product 3",
                    Price = 3.3m,
                    Order = 3,
                    ImageURL = "img_3.png",
                    Brand = new Brand { ID = 3, Name = "Brand 3", Order = 3},
                    SectionID = 3,
                    Section = new Section{ ID = 3, Name = "Section 3", Order = 3 },
                },
                });

            _cartStoreMock = new Mock<ICartStore>();
            _cartStoreMock.Setup(c => c.Cart).Returns(_cart);

            _cartService = new CartService(_cartStoreMock.Object, _productDataMock.Object);
        }

        [TestMethod]
        public void Cart_Class_ItemsCount_returns_Correct_Quantity()
        {
            var cart = _cart;

            var expected_items_count = cart.Items.Sum(i => i.Quantity);

            var actual_items_count = cart.ItemsCount;

            Assert.Equal(expected_items_count, actual_items_count);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_ItemsCount()
        {
            var cart_view_model = new CartViewModel
            {
                Items = new[]
                {
                ( new ProductViewModel { ID = 1, Name = "Product 1", Price = 0.5m }, 1 ),
                ( new ProductViewModel { ID = 2, Name = "Product 2", Price = 1.5m }, 3 ),
            }
            };

            var expected_items_count = cart_view_model.Items.Sum(i => i.quantity);

            var actual_items_count = cart_view_model.ItemsCount;

            Assert.Equal(expected_items_count, actual_items_count);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_TotalPrice()
        {
            var cart_view_model = new CartViewModel
            {
                Items = new[]
                {
                ( new ProductViewModel { ID = 1, Name = "Product 1", Price = 0.5m }, 1 ),
                ( new ProductViewModel { ID = 2, Name = "Product 2", Price = 1.5m }, 3 ),
            }
            };

            var expected_total_price = cart_view_model.Items.Sum(item => item.quantity * item.product.Price);

            var actual_total_price = cart_view_model.TotalPrice;

            Assert.Equal(expected_total_price, actual_total_price);
        }

        [TestMethod]
        public void CartService_Add_WorkCorrect()
        {
            _cart.Items.Clear();
            const int expected_id = 5;
            const int expected_items_count = 1;
            _cartService.Add(expected_id);
            var actual_items_count = _cart.ItemsCount;
            Assert.Equal(expected_items_count, actual_items_count);
            Assert.Single(_cart.Items);
            Assert.Equal(expected_id, _cart.Items.Single().ProductID);
        }

        [TestMethod]
        public void CartService_Remove_Correct_Item()
        {
            const int item_id = 1;
            const int expected_product_id = 2;
            _cartService.Remove(item_id);
            Assert.Single(_cart.Items);
            Assert.Equal(expected_product_id, _cart.Items.Single().ProductID);
        }

        [TestMethod]
        public void CartService_Clear_ClearCart()
        {
            _cartService.Clear();

            Assert.Empty(_cart.Items);
        }

        [TestMethod]
        public void CartService_Decrement_Correct()
        {
            const int item_id = 2;
            const int expected_quantity = 2;
            const int expectes_items_count = 3;
            const int expected_products_count = 2;
            _cartService.Decrement(item_id);
            Assert.Equal(expectes_items_count, _cart.ItemsCount);
            Assert.Equal(expected_products_count, _cart.Items.Count);
            var items = _cart.Items.ToArray();
            Assert.Equal(item_id, items[1].ProductID);
            Assert.Equal(expected_quantity, items[1].Quantity);
        }

        [TestMethod]
        public void CartService_Remove_Item_When_Decrement_to_0()
        {
            const int item_id = 1;
            const int expected_items_count = 3;
            _cartService.Decrement(item_id);
            Assert.Equal(expected_items_count, _cart.ItemsCount);
            Assert.Single(_cart.Items);
        }

        [TestMethod]
        public void CartService_GetViewModel_WorkCorrect()
        {
            const int expected_items_count = 4;
            const decimal expected_first_product_price = 1.1m;
            var result = _cartService.GetViewModel();
            Assert.Equal(expected_items_count, result.ItemsCount);
            Assert.Equal(expected_first_product_price, result.Items.First().product.Price);
        }
    }
}

