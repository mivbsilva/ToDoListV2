using System;
using Microsoft.AspNetCore.Components;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Client.Components
{
    public partial class Tarefas : ComponentBase
    {
        private List<ListaDeTarefas> listas = new List<ListaDeTarefas>();
        private ListaDeTarefas listaSelecionada;
        private bool criandoNovaLista = false;
        private string nomeNovaLista = string.Empty;
        private string novaTarefa;
        private int? indexDaTarefaEmEdicao = null;
        private string textoDaTarefaEditada = string.Empty;
        [Inject] private UsuarioService UsuarioService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        private string nome;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var usuario = UsuarioService.GetUsuarioLogado();

            if (usuario != null)
            {
                nome = usuario.Nome;
            }
            else
            {
                nome = "Usuário não encontrado";
            }
        }

        private void CriarNovaLista()
        {
            var novaLista = new ListaDeTarefas { Nome = nomeNovaLista };
            nomeNovaLista = string.Empty;
            criandoNovaLista = false;
            listas.Add(novaLista);
            listaSelecionada = novaLista;
        }

        private void SelecionarLista(ListaDeTarefas lista)
        {
            listaSelecionada = lista;
        }

        private void AdicionarTarefa()
        {
            if (!string.IsNullOrWhiteSpace(novaTarefa))
            {
                listaSelecionada.Tarefas.Add(novaTarefa);
                novaTarefa = string.Empty;
            }
        }

        private void EditarTarefa(ListaDeTarefas lista, int index)
        {
            listaSelecionada = lista;
            indexDaTarefaEmEdicao = index;
            textoDaTarefaEditada = lista.Tarefas[index];
        }

        private void SalvarEdicao()
        {
            if (indexDaTarefaEmEdicao != null && listaSelecionada != null && !string.IsNullOrWhiteSpace(textoDaTarefaEditada))
            {
                listaSelecionada.Tarefas[(int)indexDaTarefaEmEdicao] = textoDaTarefaEditada;
                indexDaTarefaEmEdicao = null;
                textoDaTarefaEditada = string.Empty;
            }
        }

        private void CancelarEdicao()
        {
            indexDaTarefaEmEdicao = null;
            textoDaTarefaEditada = string.Empty;
        }

        private void ExcluirTarefa(ListaDeTarefas lista, int index)
        {
            if (index >= 0 && index < lista.Tarefas.Count)
            {
                lista.Tarefas.RemoveAt(index);

                if (lista.Tarefas.Count == 0)
                {
                    listas.Remove(lista);

                    if (listaSelecionada == lista)
                    {
                        listaSelecionada = null;
                    }
                }
            }
        }
    }
}
