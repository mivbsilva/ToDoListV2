using System;
using Microsoft.AspNetCore.Components;
using ToDoList.Client.Models;
using ToDoList.Client.Services;

namespace ToDoList.Client.Components
{
    public partial class Tarefas : ComponentBase
    {
        private List<ListaDeTarefas> listas = new List<ListaDeTarefas>();
        private ListaDeTarefas? listaSelecionada;
        private bool criandoNovaLista = false;
        private string nomeNovaLista = string.Empty;
        private string novaTarefa = string.Empty;
        private int? tarefaEmEdicaoId = null;
        private string textoDaTarefaEditada = string.Empty;
        private bool carregando = false;
        private string nome = string.Empty;
        private Usuario? usuarioLogado;

        [Inject] private UsuarioService UsuarioService { get; set; } = null!;
        [Inject] private ListaService ListaService { get; set; } = null!;
        [Inject] private TarefaService TarefaService { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            usuarioLogado = UsuarioService.GetUsuarioLogado();

            if (usuarioLogado != null)
            {
                nome = usuarioLogado.Nome;
                await CarregarListas();
            }
            else
            {
                nome = "Usuário não encontrado";
            }
        }

        private async Task CarregarListas()
        {
            if (usuarioLogado == null) return;

            carregando = true;
            StateHasChanged();

            try
            {
                listas = await ListaService.GetListasAsync(usuarioLogado.Id);
                
                // Carregar tarefas para cada lista
                foreach (var lista in listas)
                {
                    lista.Tarefas = await TarefaService.GetTarefasAsync(lista.Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar listas: {ex.Message}");
            }
            finally
            {
                carregando = false;
                StateHasChanged();
            }
        }

        private async Task CriarNovaLista()
        {
            if (usuarioLogado == null || string.IsNullOrWhiteSpace(nomeNovaLista)) return;

            var novaLista = new ListaDeTarefas 
            { 
                Nome = nomeNovaLista,
                UsuarioId = usuarioLogado.Id
            };

            var listaCreated = await ListaService.CreateListaAsync(novaLista);
            
            if (listaCreated != null)
            {
                listaCreated.Tarefas = new List<Tarefa>();
                listas.Add(listaCreated);
                listaSelecionada = listaCreated;
                
                nomeNovaLista = string.Empty;
                criandoNovaLista = false;
                StateHasChanged();
            }
        }

        private void SelecionarLista(ListaDeTarefas lista)
        {
            listaSelecionada = lista;
        }

        private async Task AdicionarTarefa()
        {
            if (listaSelecionada == null || string.IsNullOrWhiteSpace(novaTarefa)) return;

            var novaTarefaObj = new Tarefa
            {
                Texto = novaTarefa,
                ListaId = listaSelecionada.Id,
                Concluida = false,
                DataCriacao = DateTime.Now
            };

            var tarefaCreated = await TarefaService.CreateTarefaAsync(novaTarefaObj);
            
            if (tarefaCreated != null)
            {
                listaSelecionada.Tarefas.Add(tarefaCreated);
                novaTarefa = string.Empty;
                StateHasChanged();
            }
        }

        private void EditarTarefa(ListaDeTarefas lista, Tarefa tarefa)
        {
            listaSelecionada = lista;
            tarefaEmEdicaoId = tarefa.Id;
            textoDaTarefaEditada = tarefa.Texto;
        }

        private async Task SalvarEdicao()
        {
            if (tarefaEmEdicaoId == null || listaSelecionada == null || string.IsNullOrWhiteSpace(textoDaTarefaEditada))
                return;

            var tarefa = listaSelecionada.Tarefas.FirstOrDefault(t => t.Id == tarefaEmEdicaoId);
            if (tarefa != null)
            {
                tarefa.Texto = textoDaTarefaEditada;
                
                var success = await TarefaService.UpdateTarefaAsync(tarefa);
                if (success)
                {
                    tarefaEmEdicaoId = null;
                    textoDaTarefaEditada = string.Empty;
                    StateHasChanged();
                }
            }
        }

        private void CancelarEdicao()
        {
            tarefaEmEdicaoId = null;
            textoDaTarefaEditada = string.Empty;
        }

        private async Task ToggleConcluida(Tarefa tarefa)
        {
            tarefa.Concluida = !tarefa.Concluida;
            
            var success = await TarefaService.UpdateTarefaAsync(tarefa);
            if (success)
            {
                StateHasChanged();
            }
            else
            {
                // Reverter se a atualização falhou
                tarefa.Concluida = !tarefa.Concluida;
            }
        }

        private async Task ExcluirTarefa(ListaDeTarefas lista, Tarefa tarefa)
        {
            var success = await TarefaService.DeleteTarefaAsync(tarefa.Id);
            
            if (success)
            {
                lista.Tarefas.Remove(tarefa);
                
                // Se a lista ficou vazia, remover a lista também
                if (lista.Tarefas.Count == 0)
                {
                    var listaDeleted = await ListaService.DeleteListaAsync(lista.Id);
                    if (listaDeleted)
                    {
                        listas.Remove(lista);
                        
                        if (listaSelecionada == lista)
                        {
                            listaSelecionada = null;
                        }
                    }
                }
                
                StateHasChanged();
            }
        }
    }
}
