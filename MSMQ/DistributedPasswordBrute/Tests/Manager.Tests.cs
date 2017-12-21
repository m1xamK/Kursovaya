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
			string res = NextDiapazone.Get("z000");

			Assert.That("10000", Is.EqualTo(res));
		}

		[Test]
		public void NextWordTestTwo()
		{
			string res = NextDiapazone.Get("zz000");

			Assert.That("100000", Is.EqualTo(res));
		}

		[Test]
		public void NextWordTestThree()
		{
			string res = NextDiapazone.Get("z5000");

			Assert.That("z6000", Is.EqualTo(res));
		}

		[Test]
		public void NextWordTestFour()
		{
			string res = NextDiapazone.Get("0");

			Assert.That("1000", Is.EqualTo(res));
		}

		[Test]
		public void NextWordTestFive()
		{
			string res = NextDiapazone.Get("a000");

			Assert.That("b000", Is.EqualTo(res));
		}
	}
}
