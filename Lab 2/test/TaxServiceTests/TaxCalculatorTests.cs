using NFluent;
using System;
using TaxService;
using Xunit;

namespace TaxServiceTests
{
    public class TaxCalculatorTests
    {
        private ITaxCalculator _sut;

        /// <summary>
        /// This constructor is executed by xunit each time a test is run.
        /// </summary>
        public TaxCalculatorTests()
        {
            // Create a new instance from the class we want to test
            _sut = new TaxCalculator();
        }

        [Fact]
        public void TaxCalculator_Calculate_TaxType_Normal()
        {
            // Assign
            double value = 10;
            TaxType taxType = TaxType.Normal;

            // Act
            double result = _sut.Calculate(value, taxType);

            // Assert
            Check.That(result).IsEqualTo(12.1);
        }

        [Fact]
        public void TaxCalculator_Calculate_TaxType_Food()
        {
            // Assign
            double value = 10;
            TaxType taxType = TaxType.Food;

            // Act
            double result = _sut.Calculate(value, taxType);

            // Assert

            // We do not use "Check.That(result).IsEqualTo(10.6);", this throws an exception like `The checked value is different from the expected one, with a difference of 1,8E-15. You may consider using IsCloseTo() for comparison.`
            // Instead we use "IsCloseTo".
            Check.That(result).IsCloseTo(10.6, 1e-10);
        }

        [Fact]
        public void TaxCalculator_Calculate_TaxType_Invalid_ThrowsException()
        {
            // Assign
            double value = 10;
            TaxType taxType = (TaxType)42;

            // Act and Assert
            Check.ThatCode(() => _sut.Calculate(value, taxType)).Throws<NotSupportedException>();
        }
    }
}