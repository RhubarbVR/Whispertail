using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazored.LocalStorage;
using Blazored.SessionStorage;

using GabbracoonClient;

namespace Whispertail_Shared
{
	public sealed class SessionStorageLink : IGabbaSsessionStorage
	{
		public SessionStorageLink(ISessionStorageService sessionStorageService) {
			StorageService = sessionStorageService;
		}

		public ISessionStorageService StorageService { get; }

		public async Task DeleteItemAsync(string key) {
			await StorageService.RemoveItemAsync(key);
		}

		public async Task<string> GetItemAsync(string key) {
			return await StorageService.GetItemAsStringAsync(key);
		}

		public async Task<string[]> GetKeysAsync() {
			return (await StorageService.KeysAsync()).ToArray();
		}

		public async Task SetItemAsync(string key, string value) {
			await StorageService.SetItemAsStringAsync(key, value);
		}
	}

	public sealed class LocalStorageLink : IGabbaLocalStorage
	{
		public LocalStorageLink(ILocalStorageService localStorageService) {
			StorageService = localStorageService;
		}

		public ILocalStorageService StorageService { get; }


		public async Task DeleteItemAsync(string key) {
			await StorageService.RemoveItemAsync(key);
		}

		public async Task<string> GetItemAsync(string key) {
			return await StorageService.GetItemAsStringAsync(key);
		}

		public async Task<string[]> GetKeysAsync() {
			return (await StorageService.KeysAsync()).ToArray();
		}

		public async Task SetItemAsync(string key, string value) {
			await StorageService.SetItemAsStringAsync(key, value);
		}
	}
}
