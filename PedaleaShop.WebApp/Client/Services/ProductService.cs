using PedaleaShop.Entities.Dtos;
using PedaleaShop.WebApp.Client.Services.Contracts;
using System.Net.Http.Json;

namespace PedaleaShop.WebApp.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ProductsDto> GetItem(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Products/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductsDto);
                    }

                    return await response.Content.ReadFromJsonAsync<ProductsDto>();
                }
                else 
                { 
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<ProductsDto>> GetItems()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/Products");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductsDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductsDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
      
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

            public async Task<IEnumerable<ProductsDto>> GetItemsByCategory(int categoryId)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Products/{categoryId}/GetItemsByCategory");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductsDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductsDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public Task<IEnumerable<ProductsDto>> GetItemsByCategory(List<int> categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetProductCategories()
        {
            try
            {
                var response = await httpClient.GetAsync("api/ProductsCategories");

                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductCategoryDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductCategoryDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
        public async Task<IEnumerable<ProductsColorDto>> GetProductsColors()
        {
            try
            {
                var response = await httpClient.GetAsync("api/ProductsColors");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductsColorDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductsColorDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
        public async Task<IEnumerable<ProductsSizeDto>> GetProductsSizes()
        {
            try
            {
                var response = await httpClient.GetAsync("api/ProductsSizes");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductsSizeDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductsSizeDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

    }
}
