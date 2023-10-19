using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SoftwareControle.Services.Authentication.Classes;
using SoftwareControle.Site;
using SoftwareControle.Site.APICall.Authenticacao;
using SoftwareControle.Site.APICall.Ferramenta;
using SoftwareControle.Site.APICall.Ordem;
using SoftwareControle.Site.APICall.Usuario;
using SoftwareControle.Site.Generator;
using SoftwareControle.Site.Sessao;
using SoftwareControle.Website.Session;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<ISessaoUsuario, SessaoUsuario>();
builder.Services.AddScoped<IRelatorioGenerator, RelatorioGenerator>();

builder.Services.AddScoped<IUsuarioEndpoints, UsuarioEndpoints>();
builder.Services.AddScoped<IFerramentaEndpoints, FerramentaEndpoints>();
builder.Services.AddScoped<IOrdemEndpoints, OrdemEndpoints>();
builder.Services.AddScoped<IRelatorioEndpoints, RelatorioEndpoints>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
