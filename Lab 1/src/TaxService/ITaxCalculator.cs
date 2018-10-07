namespace TaxService
{
    public interface ITaxCalculator
    {
        double Calculate(double value, TaxType taxType);
    }
}
