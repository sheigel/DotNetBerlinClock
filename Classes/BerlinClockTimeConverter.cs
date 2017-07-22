using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BerlinClock
{
	public class BerlinClockTimeConverter : ITimeConverter
	{
		private const char RedLamp = 'R';
		private const char OffLamp = 'O';
		private const char YellowLamp = 'Y';

		public string ConvertTime(string time)
		{
			var timeElements = time.Split(':').Select(el => Convert.ToInt32(el)).ToArray();
			Console.WriteLine("The time:{0}", time);
			return ConvertTime(timeElements[0], timeElements[1], timeElements[2]);
		}

		private string ConvertTime(int hours, int minutes, int seconds)
		{
			return $"{ConvertSeconds(seconds)}\r\n{ConvertHours(hours)}\r\n{ConvertMinutes(minutes)}";
		}

		public static string[] ConvertSeconds(int timeSecond)
		{
			return timeSecond % 2 == 0 ? new[] {YellowLamp.ToString()} : new[] {OffLamp.ToString()};
		}

		public static string[] ConvertMinutes(int minutes)
		{
			
			string MinutesFirstRow()
			{
				var lampsCount = minutes / 5;

				return BuildMinutesRow(lampsCount);
			}

			string MinutesSecondRow()
			{
				var lampsCount = minutes % 5;
				return BuildMinutesRow(lampsCount);
			}

			var minutesFirstRow = MinutesFirstRow();
			var minutesSecondRow = MinutesSecondRow();
			return new[] {minutesFirstRow, minutesSecondRow};
		}

		private static string BuildMinutesRow(int lampsOnCount)
		{
			return string.Join("", Enumerable.Repeat(RedLamp, lampsOnCount)).PadLeft(4, OffLamp);
		}


		public static string ConvertHours(int hour)
		{
			return hour.ToString();
		}
	}
}