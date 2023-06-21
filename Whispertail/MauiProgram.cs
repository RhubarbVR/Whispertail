using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.LifecycleEvents;

using Whispertail_Shared;

using System.Net.Http;
using System.Reflection;
using Microsoft.Fast.Components.FluentUI;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using Microsoft.Fast.Components.FluentUI.Infrastructure;
using System.Drawing;
using Blazored.SessionStorage;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nakama;

namespace Whispertail
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp() {
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));
			builder.Services.AddFluentUIComponents(options =>
			{
				options.HostingModel = BlazorHostingModel.Hybrid;
				options.IconConfiguration = ConfigurationGenerator.GetIconConfiguration();
				options.EmojiConfiguration = ConfigurationGenerator.GetEmojiConfiguration();
			});
			builder.Services.AddScoped<IStaticAssetService, FileBasedStaticAssetService>();

			builder.Services.AddBlazoredSessionStorage();
			builder.Services.AddBlazoredLocalStorage();

			builder.Services.AddScoped<ThemeManager>();
			builder.Services.AddSingleton(new ClientManager(new Client(new Uri("http://127.0.0.1:7350/"), "defaultkey")));

			static IEnumerable<string> GetFiles() {
				return Assembly.GetAssembly(typeof(Localisation.Localisation))?.GetManifestResourceNames() ?? Array.Empty<string>();
			}
			builder.Services.AddScoped<Localisation.Localisation>((thing) => new Localisation.DynamicLocalisation(GetFiles, (item) => new StreamReader(Assembly.GetAssembly(typeof(Localisation.Localisation)).GetManifestResourceStream(item)).ReadToEnd(), null));
#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
#endif
			builder.Services.AddMauiBlazorWebView();

			return builder.Build();
		}
	}
}