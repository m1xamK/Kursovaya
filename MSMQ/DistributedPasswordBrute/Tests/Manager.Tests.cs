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
			string res = NextDiapason.Get("z000");

			Assert.That("10000", Is.EqualTo(res));
		}

		[Test]
		public void NextWordTestTwo()
		{
			string res = NextDiapason.Get("zz000");

			Assert.That("100000", Is.EqualTo(res));
		}

		[Test]
		public void NextWordTestThree()
		{
			string res = NextDiapason.Get("z5000");

			Assert.That("z6000", Is.EqualTo(res));
		}

		[Test]
		public void NextWordTestFour()
		{
			string res = NextDiapason.Get("0");

			Assert.That("1000", Is.EqualTo(res));
		}

		[Test]
		public void NextWordTestFive()
		{
			string res = NextDiapason.Get("a000");

			Assert.That("b000", Is.EqualTo(res));
		}
	}
}
