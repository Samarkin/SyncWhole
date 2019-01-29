using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using SyncWhole.Common;

namespace SyncWhole.Google
{
	public sealed class GoogleCalendarAdapterFactory : IAppointmentSourceFactory, IAppointmentDestinationFactory
	{
		// If modifying these scopes, delete your previously saved credentials
		// at ~/.credentials/calendar-dotnet-quickstart.json
		private static readonly string[] Scopes = { CalendarService.Scope.Calendar };
		private const string ApplicationName = nameof(SyncWhole);

		private readonly string _credentialId;

		public GoogleCalendarAdapterFactory(string credentialId)
		{
			_credentialId = credentialId;
		}

		public async Task<IAppointmentDestination> ConnectDestinationAsync()
		{
			return await ConnectAsync();
		}

		public async Task<IAppointmentSource> ConnectSourceAsync()
		{
			return await ConnectAsync();
		}

		private async Task<GoogleCalendarAdapter> ConnectAsync()
		{
			UserCredential credential;
			using (var stream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream($"{nameof(SyncWhole)}.{nameof(Google)}.credentials.json"))
			{
				credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.Load(stream).Secrets,
					Scopes,
					"user",
					CancellationToken.None,
					new FileDataStore(_credentialId, true)).ConfigureAwait(false);
			}

			// Create Google Calendar API service
			var service = new CalendarService(new BaseClientService.Initializer
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName,
			});

			return new GoogleCalendarAdapter(service);
		}
	}
}