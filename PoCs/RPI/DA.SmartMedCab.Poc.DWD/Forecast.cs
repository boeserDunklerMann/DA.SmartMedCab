using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.SmartMedCab.Poc.DWD
{
	public class RESTForecast
	{
		public Trend trend { get; set; }
		public Day[] days { get; set; }
		public OpenForecast forecast { get; set; }
	}

	public class Trend
	{
		public long start { get; set; }
		public int[] temperature { get; set; }
		public int[] temperatureStd { get; set; }
		public int[] precipitationProbabilityHigh { get; set; }
		public int[] precipitationProbabilityLow { get; set; }
	}

	public class Forecast
	{
		public long start { get; set; }
		public int[] temperature { get; set; }
		public int[] temperatureStd { get; set; }
		public int[] windDirection { get; set; }
		public int[] windGust { get; set; }
		public int[] windSpeed { get; set; }
		public int[] icon { get; set; }
		public int[] precipitationTotal { get; set; }
	}

	public class Day
	{
		public object stationId { get; set; }
		public string dayDate { get; set; }
		public int temperatureMin { get; set; }
		public int temperatureMax { get; set; }
		public int precipitation { get; set; }
		public int windSpeed { get; set; }
		public int windGust { get; set; }
		public int windDirection { get; set; }
		public int sunshine { get; set; }
		public long sunrise { get; set; }
		public long sunset { get; set; }
		public long moonrise { get; set; }
		public long moonset { get; set; }
		public int moonPhase { get; set; }
		public object icon { get; set; }
		public int icon1 { get; set; }
		public int icon2 { get; set; }
	}
}
