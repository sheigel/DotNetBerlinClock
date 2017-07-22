using System;
using TechTalk.SpecFlow;
using System.Linq;
using NUnit.Framework;

namespace BerlinClock
{
	[Binding]
	public class TheBerlinClockSteps
	{
		private ITimeConverter berlinClock = new BerlinClockTimeConverter();
		private String theTime;


		[When(@"the time is ""(.*)""")]
		public void WhenTheTimeIs(string time)
		{
			theTime = time;
		}

		[Then(@"the clock should look like")]
		public void ThenTheClockShouldLookLike(string theExpectedBerlinClockOutput)
		{
			Assert.AreEqual(berlinClock.ConvertTime(theTime), theExpectedBerlinClockOutput);
		}
	}
}