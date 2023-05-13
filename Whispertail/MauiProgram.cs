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

#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
#endif
			builder.Services.AddMauiBlazorWebView();

			return builder.Build();
		}
	}
}