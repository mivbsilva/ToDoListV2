using ToDoList.Client.Models;

namespace ToDoList.Client.Services
{
    public class UsuarioService
    {
        private List<Usuario> usuarios = new();
        private static Usuario usuarioLogado;

        public void AdicionarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
        }

        public Usuario ValidarLogin(string nomeUsuario, string senha)
        {
            return usuarios.FirstOrDefault(u => u.NomeUsuario == nomeUsuario && u.Senha == senha);
        }

        public Usuario SalvarUsuarioLogado(string nome)
        {
            usuarioLogado = usuarios.FirstOrDefault(u => u.Nome == nome);
            return usuarioLogado;
        }

        public Usuario GetUsuarioLogado()
        {
            return usuarioLogado;
        }
    }
}

