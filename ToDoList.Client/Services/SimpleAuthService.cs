using ToDoList.Client.Models;

namespace ToDoList.Client.Services
{
    public class SimpleAuthService
    {
        private static Usuario? _usuarioLogado;
        
        public bool IsLoggedIn => _usuarioLogado != null;
        
        public Usuario? UsuarioLogado => _usuarioLogado;
        
        public void Login(Usuario usuario)
        {
            _usuarioLogado = usuario;
        }
        
        public void Logout()
        {
            _usuarioLogado = null;
        }
    }
}
