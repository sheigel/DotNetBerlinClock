using NUnit.Framework;

namespace BerlinClock
{
	public class BerlinClockTimeConverterTests
	{
		[TestFixture]
		public class ConvertSeconds
		{
			[TestCase("Y", 0)]
			[TestCase("O", 1)]
			[TestCase("Y", 2)]
			[TestCase("Y", 58)]
			[TestCase("O", 59)]
			public void YellowLampBlinksEveryOtherSecond(string expected, int seconds)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.ConvertSeconds(seconds), $"converting {seconds} seconds");
			}
		}
		
		[TestFixture]
		public class ConvertMinutes
		{
			[TestCase("Y", 0)]
			[TestCase("O", 1)]
			[TestCase("Y", 2)]
			[TestCase("Y", 58)]
			[TestCase("O", 59)]
			public void YellowLampBlinksEveryOtherSecond(string expected, int seconds)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.ConvertSeconds(seconds), $"converting {seconds} seconds");
			}
		}
	}
}