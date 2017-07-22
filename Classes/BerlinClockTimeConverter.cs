using System;
using System.Collections.Generic;
using System.Linq;

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
			if (timeSecond < 0 || timeSecond > 59)
			{
				throw new ArgumentOutOfRangeException(nameof(timeSecond), "seconds have to be between 00 and 59");
			}
			return timeSecond % 2 == 0 ? new[] {YellowLamp.ToString()} : new[] {OffLamp.ToString()};
		}

		public static string[] HoursRows(int hours)
		{
			if (hours < 0 || hours > 24)
			{
				throw new ArgumentOutOfRangeException(nameof(hours), "hours have to be between 00 and 24");
			}

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
			if (minutes < 0 || minutes > 59)
			{
				throw new ArgumentOutOfRangeException(nameof(minutes), "minutes have to be between 00 and 59");
			}
			return new[] {BuildFirstMinutesRow(minutes), BuildSecondMinutesRow(minutes)};
		}

		private static string BuildFirstMinutesRow(int minutes)
		{
			bool IsQuarterLamp(int i)
			{
				return i * 5 % 15 == 0;
			}

			IEnumerable<char> GenerateLampsOn(int lampsCount)
			{
				for (var i = 1; i <= lampsCount; i++)
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

			var lampsOnCount = minutes / 5;
			var totalLamps = 11;
			return string.Join("", GenerateLampsOn(lampsOnCount)).PadRight(totalLamps, OffLamp);
		}


		private static string BuildSecondMinutesRow(int minutes)
		{
			var lampsOnCount = minutes % 5;
			return string.Join("", Enumerable.Repeat(YellowLamp, lampsOnCount)).PadRight(4, OffLamp);
		}
	}
}