using Microsoft.AspNetCore.Components;
using PedaleaShop.Entities.Dtos;
using PedaleaShop.Entities.Dtos;
using PedaleaShop.WebApp.Client.Services.Contracts;

namespace PedaleaShop.WebApp.Client.Pages
{
    public class ProductsByCategoryBase:ComponentBase
    {
        [Parameter]
        public string ProductsColorsIds { get; set; }
        public List<int> LastProductsColorsIds { get; set; } //= new List<int>();
        [Parameter]
        public string ProductsSizesIds { get; set; }
        public List<int> lastProductsSizesIds { get; set; } //= new List<int>();
        [Parameter]
        public string CategoriesIds { get; set; }
        [Parameter]
        public int CategoryId { get; set; }
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

        public IEnumerable<ProductsDto> Products { get; set; }
        public string CategoryName { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                var tempColorIds= ProductsColorsIds?.Split(',')?.Select(Int32.Parse)?.ToList();
                var tempSizeIds = ProductsSizesIds?.Split(',')?.Select(Int32.Parse)?.ToList();
                //var tempCategoriesIds = CategoriesIds?.Split(',')?.Select(Int32.Parse)?.ToList();
                //CategoryId = Convert.ToInt32(CategoryIdString);
                if (tempColorIds!=null && tempSizeIds != null)
                {

                    LastProductsColorsIds = tempColorIds;
                    lastProductsSizesIds = tempSizeIds;

                    Products = await GetProductCollection(CategoryId , tempColorIds,tempSizeIds);

                    if (Products != null && Products.Count() > 0)
                    {
                        var productDto = Products.FirstOrDefault(p => (CategoryId == 0 ? true : p.CategoryId == CategoryId) && tempColorIds.Contains(p.ColorId) && tempSizeIds.Contains(p.SizeId));

                        if (productDto != null)
                        {
                            CategoryName = productDto.CategoryName;
                        }

                    }
                }
                else
                {
                    Products = await GetProductCollection(CategoryId, LastProductsColorsIds, lastProductsSizesIds);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        protected string GetCategoryName(IGrouping<int, ProductsDto> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
        }
        protected IOrderedEnumerable<IGrouping<int, ProductsDto>> GetGroupedProductsByCategory()
        {
            return from product in Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }
        private async Task<IEnumerable<ProductsDto>> GetProductCollectionByCategoryId(int categoryId)
        {
            var productCollection = await ManageProductsLocalStorageService.GetCollection();

            if(productCollection != null)
            {
                return productCollection.Where(p => p.CategoryId == categoryId);
            }
            else
            {
                return await ProductService.GetItemsByCategory(categoryId);
            }

        }
        private async Task<IEnumerable<ProductsDto>> GetProductCollection(int categoriesIds, List<int>? colorsId, List<int>? sizesId)
        {
            var productCollection = await ManageProductsLocalStorageService.GetCollection();

            if (productCollection != null)
            {
                return productCollection.Where(p => (categoriesIds==0 ?true:p.CategoryId==categoriesIds) && (colorsId == null? true : colorsId.Contains(p.ColorId)) && (sizesId == null ? true : sizesId.Contains(p.SizeId)));
            }
            else
            {
                return await ProductService.GetItemsByCategory(categoriesIds);
            }

        }
    }
}
