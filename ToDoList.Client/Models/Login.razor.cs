using Microsoft.AspNetCore.Components;
using ToDoList.Client.Services;

namespace ToDoList.Client.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        private UsuarioService UsuarioService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private string nomeUsuario;
        private string senha;
        private string msgErro;

        private async Task FazerLogin()
        {
            msgErro = string.Empty;

            var usuario = await UsuarioService.ValidarLoginAsync(nomeUsuario, senha);

            if (usuario != null)
            {
                UsuarioService.SalvarUsuarioLogado(usuario);
                NavigationManager.NavigateTo("/dashboard");
            }
            else
            {
                msgErro = "Usuário ou senha inválidos.";
            }
        }
    }
}