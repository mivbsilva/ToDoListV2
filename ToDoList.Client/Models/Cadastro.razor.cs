using System;
using Microsoft.AspNetCore.Components;
using ToDoList.Client.Models;
using System.Text.RegularExpressions;
using ToDoList.Client.Services;


namespace ToDoList.Client.Pages
{
    public partial class Cadastro : ComponentBase
    {
        private List<Usuario> usuarios = new();
        private string nome;
        private string nomeUsuario;
        private string senha;
        private string msgErro;
        private string msgSucesso;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        private UsuarioService UsuarioService { get; set; }

        private async Task CriarNovoUsuario()
        {
            msgErro = string.Empty;
            msgSucesso = string.Empty;

            if (string.IsNullOrWhiteSpace(nomeUsuario))
            {
                msgErro = "O nome de usuário não pode ser vazio.";
            }
            else if (nomeUsuario.Length < 7 || nomeUsuario.Length > 15)
            {
                msgErro = "O nome de usuário deve ter entre 7 e 15 caracteres.";
            }
            else if (usuarios.Any(u => u.NomeUsuario == nomeUsuario))
            {
                msgErro = "O nome de usuário já existe.";
            }
            else if (!SenhaValida(senha))
            {
                msgErro = "A senha deve conter letras, pelo menos um número e pelo menos um caractere especial.";
            }

            if (!string.IsNullOrEmpty(msgErro))
            {
                return;
            }

            var novoUsuario = new Usuario { Nome = nome, NomeUsuario = nomeUsuario, Senha = senha };
            UsuarioService.AdicionarUsuario(novoUsuario);

            msgSucesso = "Usuário criado com sucesso! Você será redirecionado para o login!";

            await Task.Delay(3000);
            NavigationManager.NavigateTo("/login");

            nome = string.Empty;
            nomeUsuario = string.Empty;
            senha = string.Empty;
        }

        private bool SenhaValida(string senha)
        {
            if (string.IsNullOrEmpty(senha))
            {
                return false;
            }

            var regex = new Regex(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[!@#$%^&*()\-_=+\[\]{}|\\:;""'<>,.?/~`]).{6,}$");
            return regex.IsMatch(senha);
        }
    }
}
