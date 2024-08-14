using Core.DTOs;

namespace PsikoWeb.WebApp.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<List<CategoryDto>>>("Categories");
            return response.Data;
        }
    }
}
