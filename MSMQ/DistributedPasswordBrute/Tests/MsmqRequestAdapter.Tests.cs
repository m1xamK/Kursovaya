using System;
using System.Messaging;
using MsmqAdapters;
using NUnit.Framework;

namespace Agent.Tests
{
	[TestFixture]
	class MsmqRequestorAdapterTest
	{
		private readonly string RequestQueue = "FormatName:Direct=TCP:192.168.43.233\\Private$\\RequestQueue";
		private readonly string ReplyQueue = "FormatName:Direct=TCP:192.168.43.233\\Private$\\ReplyQueue";
		private readonly string InvalidQueue = "FormatName:Direct=TCP:192.168.43.233\\Private$\\InvalidQueue";

		private Message SendTestMessage(string start, string finish, string[] hashArr)
		{
			var requestAdapter = new MsmqRequestorAdapter(RequestQueue, ReplyQueue);

			requestAdapter.Send(start, finish, hashArr);
			return requestAdapter.ReceiveSync();
		}

		[Test]
		public void ValidMessage()
		{
			// ReSharper disable once UnusedVariable
			// нужен неявно, так как в конструкторе подписывается на событие прихода сообщения в очередь и обрабатывает его.
			var replierAdapter = new MsmqReplierAdapter(RequestQueue, InvalidQueue, new Agent());

			string start = "0";
			string finish = "1000";

			string password = "zs";
			var hash = new Agent().Md5Hash(password);
			string[] hashArr = { hash };

			var message = SendTestMessage(start, finish, hashArr);
			
			Assert.That(hash + " " + password, Is.EqualTo(message.Body.ToString()));
		}
	}
}
