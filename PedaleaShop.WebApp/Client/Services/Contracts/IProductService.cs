using PedaleaShop.Models.Dtos;

namespace PedaleaShop.WebApp.Client.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
        Task<ProductDto> GetItem(int id);
        Task<IEnumerable<ProductCategoryDto>> GetProductCategories();
        Task<IEnumerable<ProductColorDto>> GetProductsColors();
        Task<IEnumerable<ProductSizeDto>> GetProductsSizes();
        Task<IEnumerable<ProductDto>> GetItemsByCategory(int categoryId);
        Task<IEnumerable<ProductDto>> GetItemsByCategory(List<int> categoryId);

    }
}
