using System.Collections.Generic;
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
			var zeroHash = new Agent().Md5Hash("0");                        // MD5, который мы ищем.
		    var hashArr = new List<string>();                               // Массив из искомых хешей.
		    hashArr.Add(zeroHash);

            var resArr = new Agent().SearchPassword("0", "zzz", hashArr);   // Результат поиска от 0 до zzz.
			
            // Ожидаемый ответ.
			List<KeyValuePair<string, string>> answerArr = new List<KeyValuePair<string, string>>();
			var zeroPair = new KeyValuePair<string, string>(zeroHash, "0");
			answerArr.Add(zeroPair);

			Assert.That(answerArr, Is.EqualTo(resArr));
		}

        /// <summary>
        /// Подбирается один относительно большой пароль.
        /// </summary>
	    [Test]
	    public void TestOneLong()
	    {
            var longHash = new Agent().Md5Hash("small"); // MD5, который мы ищем.
            var hashArr = new List<string>();           // Массив из искомых хешей.
	        hashArr.Add(longHash);

            var resArr = new Agent().SearchPassword("s0000", "zzzzz", hashArr);  // Результат поиска от zzzz до zzzzz.

            // Ожидаемый ответ.
	        List<KeyValuePair<string, string>> answerArr = new List<KeyValuePair<string, string>>();
	        var longPair = new KeyValuePair<string, string>(longHash, "small");
	        answerArr.Add(longPair);

	        Assert.That(answerArr, Is.EqualTo(resArr));
	    }

	    /// <summary>
	    /// Подбираем несколько паролей. Ожидается полное совпадение правильного ответа с полученным.
	    /// </summary>
	    [Test]
	    public void TestSeveralFull()
	    {
	        string[] passwdArr = {"aac", "bfd", "gfds"};

	        List<string> hashArr = new List<string>();          // Массив хешей
	        // Ожидаемый ответ.
	        List<KeyValuePair<string, string>> answerArr = new List<KeyValuePair<string, string>>();

	        foreach (var passwd in passwdArr)
	        {
                var hash = new Agent().Md5Hash(passwd);     
                hashArr.Add(hash);
                answerArr.Add(new KeyValuePair<string, string>(hash, passwd));
	        }

	        var resArr = new Agent().SearchPassword("0", "zzzz", hashArr);   // Результат поиска от 0 до zzzz.

	        Assert.That(answerArr, Is.EqualTo(resArr));
	    }

	    /// <summary>
	    /// Подбираем несколько паролей. Ожидается полное совпадение правильного ответа с полученным.
	    /// </summary>
	    [Test]
	    public void TestSeveralPart()
	    {
	        string[] passwdArr = { "aac", "bfd", "gfds", "feiod" };

	        List<string> hashArr = new List<string>();          // Массив хешей
	        // Ожидаемый ответ.
	        List<KeyValuePair<string, string>> answerArr = new List<KeyValuePair<string, string>>();

	        foreach (var passwd in passwdArr)
	        {
	            var hash = new Agent().Md5Hash(passwd);
	            hashArr.Add(hash);
	            answerArr.Add(new KeyValuePair<string, string>(hash, passwd));
	        }

	        var resArr = new Agent().SearchPassword("0", "zzzz", hashArr);   // Результат поиска от 0 до zzzz.

	        Assert.That(answerArr, !Is.SubsetOf(resArr));                    // Правильный ответ не является подмножеством полученного результата.
	    }
    }
}
