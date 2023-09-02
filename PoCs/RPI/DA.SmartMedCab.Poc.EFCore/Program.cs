using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DA.SmartMedCab.Poc.EFCore
{
	//public class ApplicationSettings
	//{
 //       public Guid AppSettingsID { get; set; }
 //       public string Value { get; set; }
 //       public DateTime CreationDate { get; set; }
 //       public DateTime ChangeDate { get; set; }
 //   }

	public class LocationSettings
	{
        public int LocationID { get; set; }
        public string Name { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime ChangeDate { get; set; }
		public virtual ICollection<WeatherData> WeatherData { get; set; }
	}

	public class WeatherData
	{
		public int WeatherDataID { get; set; }
		public virtual LocationSettings LocationSettings { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime ChangeDate { get; set; }
		public DateTime? DateTime { get; set; }
		public int WmoCode { get; set; }
		public int Temp_2m { get; set; }
	}

	internal class Program
	{
		static void Main(string[] args)
		{
			InsertData();
			PrintData();
		}

		private static void InsertData()
		{
			using (var ctx = new SmcContext())
			{
				ctx.Database.EnsureCreated();
				LocationSettings loc = new LocationSettings()
				{
					//LocationID = Guid.NewGuid(),
					Name = "Leipzig, SN, DE",
					Latitude = 51,
					Longitude = 12,
					CreationDate = DateTime.UtcNow
				};
				ctx.LocationSettings.Add(loc);
				ctx.WeatherData.Add(new WeatherData()
				{
					LocationSettings = loc,
					CreationDate = DateTime.UtcNow,
					DateTime = DateTime.UtcNow,
					WmoCode = 90
				});
				ctx.SaveChanges();
			}
		}

		private static void PrintData()
		{
			using (var ctx = new SmcContext())
			{
				var weatherData = ctx.WeatherData.Include(w => w.LocationSettings);
				foreach (var weather in weatherData)
				{
					StringBuilder output = new StringBuilder();
					output.AppendLine($"Location: {weather.LocationSettings.Name}");
					output.AppendLine($"\tForecast Date {weather.DateTime} WMO: {weather.WmoCode}");
					Console.WriteLine(output.ToString());
				}
			}
		}
	}
}
