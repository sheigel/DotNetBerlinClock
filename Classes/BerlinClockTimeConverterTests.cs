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
				Assert.AreEqual(expected, BerlinClockTimeConverter.ConvertSeconds(seconds), $"converting {seconds} seconds");
			}
		}

		[TestFixture]
		public class ConvertMinutes
		{
			[Test]
			public void GeneratesTwoRowsOfLamp()
			{
				Assert.AreEqual(2, BerlinClockTimeConverter.ConvertMinutes(0).Length);
			}

			[TestCase("OOOO", 0)]
			[TestCase("OOOO", 1)]
			[TestCase("OOOR", 5)]
			[TestCase("OORR", 12)]
			[TestCase("ORRR", 15)]
			[TestCase("RRRR", 24)]
			public void FirstRow_EachLampIs5Hours(string expected, int minutes)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.ConvertMinutes(minutes)[0],
					$"converting {minutes} minutes");
			}

			[TestCase("OOOO", 0)]
			[TestCase("OOOR", 1)]
			[TestCase("RRRR", 4)]
			[TestCase("OOOO", 5)]
			[TestCase("OORR", 12)]
			[TestCase("RRRR", 24)]
			public void SecondRow_EachLampIs1Hour(string expected, int minutes)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.ConvertMinutes(minutes).Last(), $"converting {minutes} minutes");
			}
		}
	}
}