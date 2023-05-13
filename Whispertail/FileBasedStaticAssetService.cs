using System.Net;

using Microsoft.Fast.Components.FluentUI.Infrastructure;

namespace Microsoft.Fast.Components.FluentUI;

public class FileBasedStaticAssetService : IStaticAssetService
{
	public async Task<string> GetAsync(string assetUrl, bool useCache = false) {
		string result = null;
		var message = CreateMessage(assetUrl);
		if (string.IsNullOrEmpty(result)) {
			result = await ReadData(assetUrl);
		}
		return result;
	}
	private static HttpRequestMessage CreateMessage(string url) {
		return new(HttpMethod.Get, url);
	}

	private static async Task<string> ReadData(string file) {
		using var stream = await FileSystem.OpenAppPackageFileAsync($"wwwroot/{file}");
		using var reader = new StreamReader(stream);
		return await reader.ReadToEndAsync();
	}
}