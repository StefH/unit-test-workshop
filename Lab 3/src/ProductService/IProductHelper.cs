using TaxService;

namespace ProductService
{
    public interface IProductHelper
    {
        double GetTotalValue(int numProducts, double price, TaxType taxType);
    }
}
