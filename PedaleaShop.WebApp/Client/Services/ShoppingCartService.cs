using Newtonsoft.Json;
using PedaleaShop.Entities.Dtos;
using PedaleaShop.WebApp.Client.Services.Contracts;
using System.Net.Http.Json;
using System.Text;

namespace PedaleaShop.WebApp.Client.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient httpClient;
        
        public event Action<int> OnShoppingCartChanged;

        public ShoppingCartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ShoppingCartItemDto> AddItem(ShoppingCartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<ShoppingCartItemToAddDto>("api/ShoppingCartsItems",cartItemToAddDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ShoppingCartItemDto);
                    }

                    return await response.Content.ReadFromJsonAsync<ShoppingCartItemDto>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ShoppingCartItemDto> DeleteItem(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/ShoppingCartsItems/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ShoppingCartItemDto>();
                }
                return default(ShoppingCartItemDto);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<List<ShoppingCartItemDto>> GetItems(int userId)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/ShoppingCartsItems/{userId}/GetItems");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {   
                        return Enumerable.Empty<ShoppingCartItemDto>().ToList();
                    }
                    return await response.Content.ReadFromJsonAsync<List<ShoppingCartItemDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void RaiseEventOnShoppingCartChanged(int totalQuantity)
        {
            if (OnShoppingCartChanged != null)
            {
                OnShoppingCartChanged.Invoke(totalQuantity);
            }
        }

        public async Task<ShoppingCartItemDto> UpdateQuantity(ShoppingCartItemQuantityUpdateDto cartItemQuantityUpdateDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(cartItemQuantityUpdateDto);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

                var response = await httpClient.PatchAsync($"api/ShoppingCartsItems/UpdateQuantity/{cartItemQuantityUpdateDto.CartItemId}", content);

                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ShoppingCartItemDto>();
                }
                return null;

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<ShoppingCartItemDto> UpdateIsSeparated(ShoppingCartItemIsSeparatedUpdateDto cartItemIsSeparatedUpdateDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(cartItemIsSeparatedUpdateDto);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

                var response = await httpClient.PatchAsync($"api/ShoppingCartsItems/UpdateIsSeparated/{cartItemIsSeparatedUpdateDto.CartItemId}", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ShoppingCartItemDto>();
                }
                return null;

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}
