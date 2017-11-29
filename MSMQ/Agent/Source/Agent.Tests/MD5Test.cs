using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Tests
{
    using NUnit.Framework;
    [TestFixture]
    class MD5Test
    {
        [Test]
        public void First()
        {
            string[] hash_arr = { "4124bc0a9335c27f086f24ba207a4912", "07159c47ee1b19ae4fb9c40d480856c4" };
            string[] pass_arr = Agent.SearchPassword("Z", "zzzzz", hash_arr);
            string[] answer = { "aa", "ba" };
            Assert.That(answer, Is.EqualTo(pass_arr));
        }

        [Test]
        public void Sec()
        {
            string[] hash_arr = { "e62595ee98b585153dac87ce1ab69c3c", "588d39bce7c5fcae6a8529c3997387ea" };
            string[] pass_arr = Agent.SearchPassword("ZZ", "zzzzz", hash_arr);
            string[] answer = { "aab", "hush" };
            Assert.That(answer, Is.EqualTo(pass_arr));
        }

        [Test]
        public void Trois()
        {
            string[] hash_arr = { "5e394281dfac81c1e7dddcaf4d35d1f6" };
            string[] pass_arr = Agent.SearchPassword("aaaZ", "zzzzz", hash_arr);
            string[] answer = { "aabb"};
            Assert.That(answer, Is.EqualTo(pass_arr));
        }
    }
}
