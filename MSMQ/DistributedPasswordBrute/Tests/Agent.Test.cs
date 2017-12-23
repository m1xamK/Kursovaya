using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Agent.Tests
{
	[TestFixture]
	class AgentTest
	{
        /// <summary>
        /// Подбирается один короткий пароль
        /// </summary>
		[Test]
		public void TestOneSimple()
        {
	        var password = "123";
			string hash = "202cb962ac59075b964b07152d234b70";
			var expectedPair = new KeyValuePair<string, string>(hash, password);

	        var resultPair = new Agent().SearchPassword("0", "1000", new List<string> {hash});

			Assert.That(resultPair[0].Key, Is.EqualTo(expectedPair.Key));
			Assert.That(resultPair[0].Value, Is.EqualTo(expectedPair.Value));
		}

        /// <summary>
        /// Подбирается один относительно большой пароль.
        /// </summary>
	    [Test]
	    public void TestOneLong()
	    {
			var password = "Aac";
			string hash = "74787479717e3e2fa0e72ec5f96d47ff";
			var expectedPair = new KeyValuePair<string, string>(hash, password);

			var resultPair = new Agent().SearchPassword("0", "1000", new List<string> { hash });

			Assert.That(resultPair[0].Key, Is.EqualTo(expectedPair.Key));
			Assert.That(resultPair[0].Value, Is.EqualTo(expectedPair.Value));
	    }

	    /// <summary>
	    /// Подбираем несколько паролей. Ожидается полное совпадение правильного ответа с полученным.
	    /// </summary>
	    [Test]
	    public void TestSeveralFull()
	    {
	        string[] passwdArr = {"fag", "123", "Aac"};
			var hashArr = new List<string> { "c592eff5625d551b0c5be656377ff871", "202cb962ac59075b964b07152d234b70", "74787479717e3e2fa0e72ec5f96d47ff" };          // Массив хешей

			List<KeyValuePair<string, string>> expectedList = new List<KeyValuePair<string, string>>{
				new KeyValuePair<string, string>("202cb962ac59075b964b07152d234b70", "123"),
				new KeyValuePair<string, string>("c592eff5625d551b0c5be656377ff871", "fag"),
				new KeyValuePair<string, string>("74787479717e3e2fa0e72ec5f96d47ff", "Aac")
				};

			var answerList = new Agent().SearchPassword("0", "zzzz", hashArr);   // Результат поиска от 0 до zzzz.

			Assert.That(answerList, Is.EqualTo(expectedList));
	    }
    }
}
