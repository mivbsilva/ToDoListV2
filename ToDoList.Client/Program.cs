using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ToDoList.Client;
using ToDoList.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5199/") });
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ListaService>();
builder.Services.AddScoped<TarefaService>();
builder.Services.AddSingleton<AuthStateService>();

var app = builder.Build();

// Inicializar o AuthStateService
var authService = app.Services.GetRequiredService<AuthStateService>();
await authService.InitializeAsync();

await app.RunAsync();