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

        private void FazerLogin()
        {
            msgErro = string.Empty;

            var usuario = UsuarioService.ValidarLogin(nomeUsuario, senha);

            if (usuario != null)
            {
                UsuarioService.SalvarUsuarioLogado(usuario.Nome);

                NavigationManager.NavigateTo("/dashboard");
            }
            else
            {
                msgErro = "Usuário ou senha inválidos.";
            }
        }
    }
}
