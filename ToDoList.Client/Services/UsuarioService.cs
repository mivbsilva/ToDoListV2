using System.Net.Http.Json;
using ToDoList.Client.Models;

namespace ToDoList.Client.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;
        private static Usuario? usuarioLogado;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AdicionarUsuarioAsync(Usuario usuario)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/usuario/cadastrar", usuario);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Usuario?> ValidarLoginAsync(string nomeUsuario, string senha)
        {
            try
            {
                var loginRequest = new { NomeUsuario = nomeUsuario, Senha = senha };
                var response = await _httpClient.PostAsJsonAsync("api/usuario/login", loginRequest);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Usuario>();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public void SalvarUsuarioLogado(Usuario usuario)
        {
            usuarioLogado = usuario;
        }

        public Usuario? GetUsuarioLogado()
        {
            return usuarioLogado;
        }
    }
}