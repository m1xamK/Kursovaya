
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

using NUnit.Framework;

namespace Agent.Tests
{
    [TestFixture]
    class AgentTest
    {
        [Test]
        public void Null()
        {
            var zeroHash = new Agent().Md5Hash("0");

            var passArr = new Agent().Calculate("0 500 " + zeroHash);
            List<KeyValuePair<string, string>> answer = new List<KeyValuePair<string, string>>();
            var a = new KeyValuePair<string, string>(zeroHash, "0");
            answer.Add(a);
            Assert.That(answer, Is.EqualTo(passArr));
        }

        [Test]
        public void First()
        {
            var passArr = new Agent().Calculate("0 500 4124bc0a9335c27f086f24ba207a4912 07159c47ee1b19ae4fb9c40d480856c4");
            List<KeyValuePair<string, string>> answer = new List<KeyValuePair<string, string>>();
            var a = new KeyValuePair<string, string>("4124bc0a9335c27f086f24ba207a4912", "aa");
            var b = new KeyValuePair<string, string>("07159c47ee1b19ae4fb9c40d480856c4", "ba");
            answer.Add(a);
            answer.Add(b);
            Assert.That(answer, Is.EqualTo(passArr));
        }

        //[Test]
        //public void Sec()
        //{
        //    string[] hashArr = { "e62595ee98b585153dac87ce1ab69c3c", "588d39bce7c5fcae6a8529c3997387ea" };
        //    var passArr = Agent.SearchPassword("ZZ", "zzzzz", hashArr);
        //    string[] answer = { "aab", "hush" };
        //    Assert.That(answer, Is.EqualTo(passArr));
        //}

        //[Test]
        //public void Trois()
        //{
        //    string[] hashArr = { "5e394281dfac81c1e7dddcaf4d35d1f6" };
        //    var passArr = Agent.SearchPassword("aaaZ", "zzzzz", hashArr);
        //    string[] answer = { "aabb"};
        //    Assert.That(answer, Is.EqualTo(passArr));
        //}
    }
}
