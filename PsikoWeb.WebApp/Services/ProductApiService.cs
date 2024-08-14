using Core.DTOs;

namespace PsikoWeb.WebApp.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductWithCategoriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<List<ProductWithCategoryDto>>>("products/GetProductsWithCategory");
            return response.Data;
        }
        public async Task<ProductDTO> SaveAsync(ProductDTO product)
        {
            var response = await _httpClient.PostAsJsonAsync("products",product);
            if(!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDTO<ProductDTO>>();
            return responseBody.Data;
        }
        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<ProductDTO>>($"products/{id}");
            return response.Data;
        }
        public async Task<bool> UpdateAsync(ProductDTO product)
        {
            var response = await _httpClient.PutAsJsonAsync("products", product);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
