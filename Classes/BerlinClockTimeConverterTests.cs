using System.Linq;
using NUnit.Framework;

namespace BerlinClock
{
	public class BerlinClockTimeConverterTests
	{
		[TestFixture]
		public class ConvertSeconds
		{
			[TestCase(new[] {"Y"}, 0)]
			[TestCase(new[] {"O"}, 1)]
			[TestCase(new[] {"Y"}, 2)]
			[TestCase(new[] {"Y"}, 58)]
			[TestCase(new[] {"O"}, 59)]
			public void YellowLampBlinksEveryOtherSecond(string[] expected, int seconds)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.SecondsRows(seconds), $"converting {seconds} seconds");
			}
		}

		
		[TestFixture]
		public class ConvertHours
		{
			[Test]
			public void GeneratesTwoRowsOfLamp()
			{
				Assert.AreEqual(2, BerlinClockTimeConverter.HoursRows(0).Length);
			}

			[TestCase("OOOO", 0)]
			[TestCase("OOOO", 1)]
			[TestCase("OOOO", 4)]
			[TestCase("ROOO", 5)]
			[TestCase("RROO", 13)]
			[TestCase("RRRO", 15)]
			[TestCase("RRRR", 24)]
			public void FirstRow_EachLampIs5Hours(string expected, int hours)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.HoursRows(hours)[0],
					$"converting {hours} hours");
			}

			[TestCase("OOOO", 0)]
			[TestCase("ROOO", 1)]
			[TestCase("RRRR", 4)]
			[TestCase("OOOO", 5)]
			[TestCase("RRRO", 13)]
			[TestCase("OOOO", 15)]
			[TestCase("RRRR", 24)]
			public void SecondRow_EachLampIs1Hour(string expected, int hours)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.HoursRows(hours).Last(), $"converting {hours} hours");
			}
			
			
		}
	}
}