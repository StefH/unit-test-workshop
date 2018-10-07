using Moq;
using NFluent;
using ProductService;
using System;
using TaxService;
using Xunit;

namespace ProductServiceTests
{
    public class ProductHelperTests
    {
        Mock<ITaxCalculator> _taxCalculatorMock;

        IProductHelper _sut;

        public ProductHelperTests()
        {
            // Create a new mock for the ITaxCalculator
            _taxCalculatorMock = new Mock<ITaxCalculator>();

            // Setup the "Calculate" method to accept a double and a TaxType and always return the double value 5.
            _taxCalculatorMock.Setup(_ => _.Calculate(It.IsAny<double>(), It.IsAny<TaxType>())).Returns(5);

            // Create a new ProductHelper class with the mocked ITaxCalculator
            _sut = new ProductHelper(_taxCalculatorMock.Object);
        }

        [Fact]
        public void ProductHelper_GetTotalValue_TaxType_Normal()
        {
            // Assign
            int numProducts = 4;
            double price = 6.9;
            var taxType = TaxType.Normal;

            // Act
            double result = _sut.GetTotalValue(numProducts, price, taxType);

            // Assert
            Check.That(result).IsCloseTo(138, 1e-10);

            // Verify that the "Calculate" method is called only once with a double and a TaxType argument.
            _taxCalculatorMock.Verify(_ => _.Calculate(It.IsAny<double>(), It.IsAny<TaxType>()), Times.Once);

            // A detailed verify would be to check if the right values are passed to the mock.
            _taxCalculatorMock.Verify(_ => _.Calculate(It.IsAny<double>(), taxType), Times.Once);
        }

        [Fact]
        public void ProductHelper_GetTotalValue_TaxType_Food()
        {
            // Assign
            int numProducts = 4;
            double price = 6.9;
            var taxType = TaxType.Food;

            // As an example:
            // Setup the "Calculate" method to accept a double and a TaxType.Food and always return the double value 7.
            _taxCalculatorMock.Setup(_ => _.Calculate(It.IsAny<double>(), TaxType.Food)).Returns(7);

            // Act
            double result = _sut.GetTotalValue(numProducts, price, taxType);

            // Assert
            Check.That(result).IsCloseTo(193.2, 1e-10);

            // Verify that the "Calculate" method is called only once with a double and a TaxType argument.
            _taxCalculatorMock.Verify(_ => _.Calculate(It.IsAny<double>(), It.IsAny<TaxType>()), Times.Once);

            // A detailed verify would be to check if the right values are passed to the mock.
            _taxCalculatorMock.Verify(_ => _.Calculate(It.IsAny<double>(), taxType), Times.Once);
        }

        [Fact]
        public void ProductHelper_GetTotalValue_ITaxCalculator_Throws_Exception()
        {
            // Assign
            int numProducts = 4;
            double price = 6.9;
            var taxType = TaxType.Food;

            // Setup the "Calculate" method to accept a double and a TaxType.Food and always throws a NotSupportedException.
            _taxCalculatorMock.Setup(_ => _.Calculate(It.IsAny<double>(), It.IsAny<TaxType>())).Throws<NotSupportedException>();

            // Act and Assert
            Check.ThatCode(() => _sut.GetTotalValue(numProducts, price, taxType)).Throws<NotSupportedException>();

            // Verify that the "Calculate" method is called only once with a double and a TaxType argument.
            _taxCalculatorMock.Verify(_ => _.Calculate(It.IsAny<double>(), taxType), Times.Once);
        }
    }
}