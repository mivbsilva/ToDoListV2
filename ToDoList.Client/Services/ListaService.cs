using System.Net.Http.Json;
using ToDoList.Client.Models;
using System.Text;
using System.Text.Json;

namespace ToDoList.Client.Services
{
    public class ListaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5199/api";

        public ListaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ListaDeTarefas>> GetListasAsync(int usuarioId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ListaDeTarefas>>($"{_baseUrl}/Lista/usuario/{usuarioId}");
                return response ?? new List<ListaDeTarefas>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar listas: {ex.Message}");
                return new List<ListaDeTarefas>();
            }
        }

        public async Task<ListaDeTarefas?> CreateListaAsync(ListaDeTarefas lista)
        {
            try
            {
                var json = JsonSerializer.Serialize(lista);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_baseUrl}/Lista", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ListaDeTarefas>(responseContent, new JsonSerializerOptions 
                    { 
                        PropertyNameCaseInsensitive = true 
                    });
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar lista: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateListaAsync(ListaDeTarefas lista)
        {
            try
            {
                var json = JsonSerializer.Serialize(lista);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"{_baseUrl}/Lista/{lista.Id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar lista: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteListaAsync(int listaId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/Lista/{listaId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar lista: {ex.Message}");
                return false;
            }
        }
    }
}
