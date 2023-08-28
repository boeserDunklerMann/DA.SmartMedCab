using Newtonsoft.Json.Linq;

namespace DA.SmartMedCab.Poc.DWD
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");

			using (var http = new HttpClient())
			{
				var dwd = new NSwag.Client("https://dwd.api.proxy.bund.dev/v30/", http);
				var bla = dwd.StationOverviewExtendedAsync(new string[] { "10471" }).Result;
			}
			OpenMeteo().Wait();
		}

		private static async Task Stations(/*NSwag.Client client*/)
		{
			//NSwag.StationOverview overview = await client.StationOverviewExtendedAsync(new string[] { "02928" });
			var clientHandler = new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip };
			var client = new HttpClient(clientHandler);
			var request = new HttpRequestMessage(HttpMethod.Get, "https://s3.eu-central-1.amazonaws.com/app-prod-static.warnwetter.de/v16/forecast_mosmix_10471.json");
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			string json = await response.Content.ReadAsStringAsync();
			Console.WriteLine(json);
			var x = Newtonsoft.Json.JsonConvert.DeserializeObject<RESTForecast>(json);
			long sset = x.days[0].sunset;
		}

		private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dateTime;
		}

		/// <summary>
		/// Das ist exakt, was ich brauche! -DA
		/// See also: https://geocoding-api.open-meteo.com/v1/search?name=leipzig&count=10&language=de&format=json
		/// </summary>
		/// <returns></returns>
		private static async Task<OpenForecast> OpenMeteo()
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://api.open-meteo.com/v1/dwd-icon?latitude=51.3396&longitude=12.3713&daily=weathercode&timezone=auto");
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			string json = await response.Content.ReadAsStringAsync();
			Console.WriteLine(json);
			JObject o = JObject.Parse(json);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603
			JToken daily = o.SelectToken("daily");
			var forecast = daily?.ToObject<OpenForecast>();
			return forecast;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8603
		}
	}

	public class OpenForecast
	{
		public string[]? time { get; set; }
		public int[]? weathercode { get; set; }
	}
}