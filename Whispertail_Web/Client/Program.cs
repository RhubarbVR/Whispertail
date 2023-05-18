using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Whispertail_Shared;


using System.Net.Http;
using System.Reflection;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Fast.Components.FluentUI.DesignTokens;
using GabbracoonClient;
using Blazored.LocalStorage;
using Blazored.SessionStorage;

namespace Whispertail_Web.Client
{
	public class Program
	{

		public static async Task Main(string[] args) {
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

			builder.Services.AddFluentUIComponents(options => {
				options.HostingModel = BlazorHostingModel.WebAssembly;
				options.IconConfiguration = ConfigurationGenerator.GetIconConfiguration();
				options.EmojiConfiguration = ConfigurationGenerator.GetEmojiConfiguration();
			});

			builder.Services.AddBlazoredSessionStorage();
			builder.Services.AddBlazoredLocalStorage();
			builder.Services.AddScoped<IGabbaLocalStorage, LocalStorageLink>();
			builder.Services.AddScoped<IGabbaSsessionStorage, SessionStorageLink>();

			builder.Services.AddScoped<ThemeManager>();
			builder.Services.AddScoped<GabbracoonClientManager>();
			static IEnumerable<string> GetFiles() {
				return Assembly.GetAssembly(typeof(DynamicLocalisation))?.GetManifestResourceNames() ?? Array.Empty<string>();
			}
			builder.Services.AddScoped<Localisation>((thing) => new DynamicLocalisation(GetFiles, (item) => new StreamReader(Assembly.GetAssembly(typeof(DynamicLocalisation)).GetManifestResourceStream(item)).ReadToEnd(), null));
			await builder.Build().RunAsync();
		}
	}
}