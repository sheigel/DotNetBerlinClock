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
			var allRows = new[]
			{
				SecondsRows(seconds),
				HoursRows(hours),
				MinutesRows(minutes)
			}.SelectMany(x => x);
			return string.Join("\r\n", allRows);
		}

		public static string[] SecondsRows(int timeSecond)
		{
			return timeSecond % 2 == 0 ? new[] {YellowLamp.ToString()} : new[] {OffLamp.ToString()};
		}

		public static string[] HoursRows(int hours)
		{
			var firstRowLampsOnCount = hours / 5;
			var secondRowLampsOnCount = hours % 5;
			return new[] {BuildHourRow(firstRowLampsOnCount), BuildHourRow(secondRowLampsOnCount)};
		}

		private static string BuildHourRow(int lampsOnCount)
		{
			return string.Join("", Enumerable.Repeat(RedLamp, lampsOnCount)).PadRight(4, OffLamp);
		}


		public static string[] MinutesRows(int minutes)
		{
			return new[] {BuildFirstMinutesRow(minutes), BuildSecondMinutesRow(minutes)};
		}

		private static string BuildFirstMinutesRow(int minutes)
		{
			return string.Join("", GenerateMinutesLamps(minutes)).PadRight(11, OffLamp);
		}

		private static IEnumerable<char> GenerateMinutesLamps(int minutes)
		{
			bool IsQuarterLamp(int i)
			{
				return i * 5 % 15 == 0;
			}

			var lampsOnCount = minutes / 5;
			for (var i = 1; i <= lampsOnCount; i++)
			{
				if (IsQuarterLamp(i))
				{
					yield return RedLamp;
				}
				else
				{
					yield return YellowLamp;
				}
			}
		}


		private static string BuildSecondMinutesRow(int minutes)
		{
			var lampsOnCount = minutes % 5;
			return string.Join("", Enumerable.Repeat(YellowLamp, lampsOnCount)).PadRight(4, OffLamp);
		}
	}
}