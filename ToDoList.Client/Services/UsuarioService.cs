using System.Net.Http.Json;
using ToDoList.Client.Models;

namespace ToDoList.Client.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthStateService _authState;

        public UsuarioService(HttpClient httpClient, AuthStateService authState)
        {
            _httpClient = httpClient;
            _authState = authState;
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
            _authState.Login(usuario);
        }

        public Usuario? GetUsuarioLogado()
        {
            return _authState.UsuarioLogado;
        }

        public void Logout()
        {
            _authState.Logout();
        }
    }
}