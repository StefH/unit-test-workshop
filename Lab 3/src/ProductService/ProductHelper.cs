using JetBrains.Annotations;
using System;
using TaxService;

namespace ProductService
{
    public class ProductHelper : IProductHelper
    {
        private readonly ITaxCalculator _taxCalculator;

        public ProductHelper([NotNull] ITaxCalculator taxCalculator)
        {
            if (taxCalculator == null)
            {
                throw new ArgumentNullException(nameof(taxCalculator));
            }

            _taxCalculator = taxCalculator;
        }

        public double GetTotalValue(int numProducts, double price, TaxType taxType)
        {
            double value = numProducts * price;

            return value * _taxCalculator.Calculate(value, taxType);
        }
    }
}