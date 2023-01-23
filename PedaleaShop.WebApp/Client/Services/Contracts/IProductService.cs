using PedaleaShop.Entities.Dtos;

namespace PedaleaShop.WebApp.Client.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsDto>> GetItems();
        Task<ProductsDto> GetItem(int id);
        Task<IEnumerable<ProductCategoryDto>> GetProductCategories();
        Task<IEnumerable<ProductsColorDto>> GetProductsColors();
        Task<IEnumerable<ProductsSizeDto>> GetProductsSizes();
        Task<IEnumerable<ProductsDto>> GetItemsByCategory(int categoryId);
        Task<IEnumerable<ProductsDto>> GetItemsByCategory(List<int> categoryId);

    }
}
