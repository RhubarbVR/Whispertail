using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nakama;

namespace Whispertail_Shared
{
	public sealed class ClientManager
	{
		private readonly List<ISession> _sessions = new();

		public ClientManager(Client client) { Client = client; }

		public Client Client { get; }

		public IReadOnlyList<ISession> Sessions => _sessions;

		private ISession _currentSession;

		public ISession CurrentSession
		{
			get => _currentSession;
			set {
				_currentSession = value;
				SessionChanged?.Invoke(_currentSession);
			}
		}

		public event Action<ISession> SessionChanged;

		public ISession RegisterSession(ISession session) {
			CurrentSession = session;
			_sessions.Add(session);
			return session;
		}

		public async Task RefreshTokens() {
			foreach (var session in _sessions) {
				await Client.SessionRefreshAsync(session);
			}
		}

		public async Task CloseSession(ISession session) {
			await Client.SessionLogoutAsync(session);
			_sessions.Remove(session);
		}

		public async Task<ISession> RegisterSession(Func<Client, Task<ISession>> sessionTask) {
			var session = await sessionTask(Client);
			_sessions.Add(session);
			return session;
		}

		public async Task<ISession> AuthenticateAppleAsync(string token, string username = null, bool create = true, Dictionary<string, string> vars = null, RetryConfiguration retryConfiguration = null, CancellationToken canceller = default) {
			var session = await Client.AuthenticateAppleAsync(token, username, create, vars, retryConfiguration, canceller);
			if (!(session?.IsExpired ?? true)) {
				RegisterSession(session);
			}
			return session;
		}

		public async Task<ISession> AuthenticateCustomAsync(string id, string username = null, bool create = true, Dictionary<string, string> vars = null, RetryConfiguration retryConfiguration = null, CancellationToken canceller = default) {
			var session = await Client.AuthenticateCustomAsync(id, username, create, vars, retryConfiguration, canceller);
			if (!(session?.IsExpired ?? true)) {
				RegisterSession(session);
			}
			return session;
		}

		public async Task<ISession> AuthenticateDeviceAsync(string id, string username = null, bool create = true, Dictionary<string, string> vars = null, RetryConfiguration retryConfiguration = null, CancellationToken canceller = default) {
			var session = await Client.AuthenticateDeviceAsync(id, username, create, vars, retryConfiguration, canceller);
			if (!(session?.IsExpired ?? true)) {
				RegisterSession(session);
			}
			return session;
		}

		public async Task<ISession> AuthenticateEmailAsync(string email, string password, string username = null, bool create = true, Dictionary<string, string> vars = null, RetryConfiguration retryConfiguration = null, CancellationToken canceller = default) {
			var session = await Client.AuthenticateEmailAsync(email, password, username, create, vars, retryConfiguration, canceller);
			if (!(session?.IsExpired ?? true)) {
				RegisterSession(session);
			}
			return session;
		}

		public async Task<ISession> AuthenticateFacebookAsync(string token, string username = null, bool create = true, bool import = true, Dictionary<string, string> vars = null, RetryConfiguration retryConfiguration = null, CancellationToken canceller = default) {
			var session = await Client.AuthenticateFacebookAsync(token, username, create, import, vars, retryConfiguration, canceller);
			if (!(session?.IsExpired ?? true)) {
				RegisterSession(session);
			}
			return session;
		}

		public async Task<ISession> AuthenticateGameCenterAsync(string bundleId, string playerId, string publicKeyUrl, string salt, string signature, string timestamp, string username = null, bool create = true, Dictionary<string, string> vars = null, RetryConfiguration retryConfiguration = null, CancellationToken canceller = default) {
			var session = await Client.AuthenticateGameCenterAsync(bundleId, playerId, publicKeyUrl, salt, signature, timestamp, username, create, vars, retryConfiguration, canceller);
			if (!(session?.IsExpired ?? true)) {
				RegisterSession(session);
			}
			return session;
		}

		public async Task<ISession> AuthenticateGoogleAsync(string token, string username = null, bool create = true, Dictionary<string, string> vars = null, RetryConfiguration retryConfiguration = null, CancellationToken canceller = default) {
			var session = await Client.AuthenticateGoogleAsync(token, username, create, vars, retryConfiguration, canceller);
			if (!(session?.IsExpired ?? true)) {
				RegisterSession(session);
			}
			return session;
		}

		public async Task<ISession> AuthenticateSteamAsync(string token, string username = null, bool create = true, bool import = true, Dictionary<string, string> vars = null, RetryConfiguration retryConfiguration = null, CancellationToken canceller = default) {
			var session = await Client.AuthenticateSteamAsync(token, username, create, import, vars, retryConfiguration, canceller);
			if (!(session?.IsExpired ?? true)) {
				RegisterSession(session);
			}
			return session;
		}

	}
}
