using Manager;
using NUnit.Framework;

namespace Agent.Tests
{
	[TestFixture]
	class ManagerTest
	{
		/*[Test]
		public void ValidMessage()
		{
			// ReSharper disable once UnusedVariable
			// нужен неявно, так как в конструкторе подписывается на событие прихода сообщения в очередь и обрабатывает его.
			var replierAdapter = new MsmqReplierAdapter(managerIp, agentIp, new Agent());

			string start = "0";
			string finish = "1000";

			string password = "zs";
			var hash = new Agent().Md5Hash(password);
			string[] hashArr = { hash };

			var message = SendTestMessage(start, finish, hashArr);

			Assert.That(hash + " " + password, Is.EqualTo(message.Body.ToString()));
		}*/
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
