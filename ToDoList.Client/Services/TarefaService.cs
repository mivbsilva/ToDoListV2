using System.Net.Http.Json;
using ToDoList.Client.Models;
using System.Text;
using System.Text.Json;

namespace ToDoList.Client.Services
{
    public class TarefaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5199/api";

        public TarefaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Tarefa>> GetTarefasAsync(int listaId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Tarefa>>($"{_baseUrl}/Tarefa/lista/{listaId}");
                return response ?? new List<Tarefa>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar tarefas: {ex.Message}");
                return new List<Tarefa>();
            }
        }

        public async Task<Tarefa?> CreateTarefaAsync(Tarefa tarefa)
        {
            try
            {
                var json = JsonSerializer.Serialize(tarefa);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_baseUrl}/Tarefa", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Tarefa>(responseContent, new JsonSerializerOptions 
                    { 
                        PropertyNameCaseInsensitive = true 
                    });
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar tarefa: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateTarefaAsync(Tarefa tarefa)
        {
            try
            {
                var json = JsonSerializer.Serialize(tarefa);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"{_baseUrl}/Tarefa/{tarefa.Id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar tarefa: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteTarefaAsync(int tarefaId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/Tarefa/{tarefaId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar tarefa: {ex.Message}");
                return false;
            }
        }
    }
}
