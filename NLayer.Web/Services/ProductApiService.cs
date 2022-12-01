using NLayer.Core.Dtos;
using NLayer.Core.DTOs;
using System.Net.WebSockets;

namespace NLayer.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<List<ProductsWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            //Burada appsettingjson içerisinde tanımladığımız BaseUrl adresinden yani api den veri aldık.
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductsWithCategoryDto>>>("products/GetProductsWithCategory");
            return response.Data;
        }


        public async Task<ProductDto> SaveAsync(ProductDto productDto)
        {
            var response = await _httpClient.PostAsJsonAsync("products", productDto);
            if (!response.IsSuccessStatusCode) return null;
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();
            return responseBody.Data;
        }


        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"/products/{id}");
            return response.Data;
        }
        public async Task<bool> UpdateAsync(ProductDto productDto)
        {
            var response = await _httpClient.PutAsJsonAsync("products", productDto);

            return response.IsSuccessStatusCode;
        }


        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            return response.IsSuccessStatusCode;

        }


    }
}
