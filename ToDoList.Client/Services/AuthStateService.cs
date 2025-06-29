using ToDoList.Client.Models;

namespace ToDoList.Client.Services
{
    public class AuthStateService
    {
        private Usuario? usuarioLogado;
        public event Action? OnAuthStateChanged;

        public bool IsLoggedIn => usuarioLogado != null;
        public Usuario? UsuarioLogado => usuarioLogado;

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
