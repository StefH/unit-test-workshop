using System;

namespace TaxService
{
    public class TaxCalculator : ITaxCalculator
    {
        private const double TaxPercentageNormal = 0.21;
        private const double TaxPercentageFood = 0.06;

        public double Calculate(double value, TaxType taxType)
        {
            switch (taxType)
            {
                case TaxType.Food:
                    return value * (1.0 + TaxPercentageFood);

                case TaxType.Normal:
                    return value * (1.0 + TaxPercentageNormal);

                default:
                    throw new NotSupportedException($"The value {taxType} is not a valid TaxType.");
            }
        }
    }
}