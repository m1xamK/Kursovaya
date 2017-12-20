using Manager;
using NUnit.Framework;

namespace Agent.Tests
{
	[TestFixture]
	class ManagerTest
	{
		[Test]
		public void NextWordSimpleTest()
		{
			string res = NextWord.Get("z000");

			Assert.That("10000", Is.EqualTo(res));
		}

		[Test]
		public void NextWordTestTwo()
		{
			string res = NextWord.Get("zz000");

			Assert.That("100000", Is.EqualTo(res));
		}

		[Test]
		public void NextWordTestThree()
		{
			string res = NextWord.Get("z5000");

			Assert.That("z6000", Is.EqualTo(res));
		}

		[Test]
		public void NextWordTestFour()
		{
			string res = NextWord.Get("0");

			Assert.That("1000", Is.EqualTo(res));
		}
	}
}
