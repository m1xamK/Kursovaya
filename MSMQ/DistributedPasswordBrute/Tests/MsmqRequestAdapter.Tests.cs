using System;

using MsmqAdapters;
using NUnit.Framework;

namespace Agent.Tests
{
	[TestFixture]
	class MsmqRequestorAdapterTest
	{
		[Test]
		public void One()
		{
			MsmqRequestorAdapter requestAdapter = new MsmqRequestorAdapter(".\\Private$\\RequestQueue", ".\\Private$\\ReplyQueue");

			string start = "start";
			int count = 42;
			string[] hash = {"one", "two", "three"};
			string id = requestAdapter.Send(start, count, hash);
	
			//requestAdapter.ReceiveSync();

			//Assert.That("asd", Is.EqualTo(id));
		}
	}
}
