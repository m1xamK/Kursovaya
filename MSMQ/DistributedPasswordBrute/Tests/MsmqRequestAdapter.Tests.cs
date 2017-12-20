using System.Messaging;
using MsmqAdapters;
using NUnit.Framework;

namespace Agent.Tests
{
	[TestFixture]
	class MsmqRequestorAdapterTest
	{
		private readonly string RequestQueue = ".\\Private$\\RequestQueue";
		private readonly string ReplyQueue = ".\\Private$\\ReplyQueue";
		private readonly string InvalidQueue = ".\\Private$\\InvalidQueue";

		[Test]
		public void ValidMessage()
		{
			MsmqRequestorAdapter requestAdapter = new MsmqRequestorAdapter(RequestQueue, ReplyQueue);

			// ReSharper disable once UnusedVariable
			// нужен неявно, так как в конструкторе подписывается на событие прихода сообщения в очередь и обрабатывает его.
			MsmqAdapters.MsmqReplierAdapter replierAdapter = new MsmqAdapters.MsmqReplierAdapter(RequestQueue, InvalidQueue, new Agent());

			string start = "0";
			int finish = 500;
			//string finish = "hi";

			string str = "1";
			var hash = new Agent().Md5Hash(str);
			string[] hashArr = { hash };

			string id = requestAdapter.Send(start, finish, hashArr);
			Message message = requestAdapter.ReceiveSync();
			
			Assert.That(id, Is.EqualTo(message.CorrelationId));
			Assert.That(hash + " " + str, Is.EqualTo(message.Body.ToString()));
		}

		[Test]
		public void InvalidMessage()
		{
			MsmqRequestorAdapter requestAdapter = new MsmqRequestorAdapter(RequestQueue, ReplyQueue);

			// ReSharper disable once UnusedVariable
			// нужен неявно, так как в конструкторе подписывается на событие прихода сообщения в очередь и обрабатывает его.
			MsmqAdapters.MsmqReplierAdapter replierAdapter = new MsmqAdapters.MsmqReplierAdapter(RequestQueue, InvalidQueue, new Agent());

			string start = "0";
			int finish = 500;
			//string finish = "hi";

			string str = "1";
			var hash = new Agent().Md5Hash(str);
			string[] hashArr = { hash };

			string id = requestAdapter.Send(start, finish, hashArr);
			Message message = requestAdapter.ReceiveSync();

			Assert.That(id, Is.EqualTo(message.CorrelationId));
			Assert.That(hash + " " + str, Is.EqualTo(message.Body.ToString()));
		}
	}
}
