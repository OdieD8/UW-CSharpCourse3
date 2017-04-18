using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelloWorld.Models;
using HelloWorld.Controllers;
using Moq;
using System.Linq;

namespace HelloWorld.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void TestMethodWithFakeClass()
        {
            // Arrange
            var controller = new HomeController(new FakeProductRepository());

            // Act
            var result = controller.Products();

            // Assert
            var products = (Product[])((System.Web.Mvc.ViewResultBase)(result)).Model;
            Assert.AreEqual(5, products.Length, "Length is invalid");

            int greaterThanTen = 0;
            int lessThanTen = 0;

            foreach (Product p in products)
            {
                if (p.Price > 10m)
                {
                    greaterThanTen++;
                }
                if (p.Price < 10m)
                {
                    lessThanTen++;
                }
            }

            Assert.AreEqual(3, greaterThanTen);
            Assert.AreEqual(2, lessThanTen);

            // Using Linq
            Assert.AreEqual(3, products.Where(p => p.Price > 10).Count());
            Assert.AreEqual(2, products.Where(p => p.Price < 10).Count());
        }

        [TestMethod]
        public void TestMethodWithMoq()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .SetupGet(t => t.Products)
                .Returns(() =>
                {
                    return new Product[]{
                new Product{Name="Baseball"},
                new Product{Name="Football"}
                    };
                });

            // Arrange
            var controller = new HomeController(mockProductRepository.Object);

            // Act
            var result = controller.Products();

            // Assert
            var products = (Product[])((System.Web.Mvc.ViewResultBase)(result)).Model;
            Assert.AreEqual(2, products.Length, "Length is invalid");
        }
    }
}