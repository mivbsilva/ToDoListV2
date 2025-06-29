using ToDoList.Client.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace ToDoList.Client.Services
{
    public class AuthStateService
    {
        private Usuario? usuarioLogado;
        private readonly IJSRuntime _jsRuntime;
        private const string USER_STORAGE_KEY = "todolist_user";
        
        public event Action? OnAuthStateChanged;

        public bool IsLoggedIn => usuarioLogado != null;
        public Usuario? UsuarioLogado => usuarioLogado;

        public AuthStateService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var userJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", USER_STORAGE_KEY);
                
                if (!string.IsNullOrEmpty(userJson))
                {
                    usuarioLogado = JsonSerializer.Deserialize<Usuario>(userJson, new JsonSerializerOptions 
                    { 
                        PropertyNameCaseInsensitive = true 
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao recuperar usuário do localStorage: {ex.Message}");
            }
        }

        public async Task LoginAsync(Usuario usuario)
        {
            usuarioLogado = usuario;
            try
            {
                var userJson = JsonSerializer.Serialize(usuario);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", USER_STORAGE_KEY, userJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar usuário no localStorage: {ex.Message}");
            }
            NotifyAuthStateChanged();
        }

        public async Task LogoutAsync()
        {
            usuarioLogado = null;
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", USER_STORAGE_KEY);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao remover usuário do localStorage: {ex.Message}");
            }
            NotifyAuthStateChanged();
        }

        // Métodos síncronos para compatibilidade (deprecated, mas mantidos)
        public void Login(Usuario usuario)
        {
            usuarioLogado = usuario;
            NotifyAuthStateChanged();
        }

        public void Logout()
        {
            usuarioLogado = null;
            NotifyAuthStateChanged();
        }

        private void NotifyAuthStateChanged()
        {
            OnAuthStateChanged?.Invoke();
        }
    }
}
