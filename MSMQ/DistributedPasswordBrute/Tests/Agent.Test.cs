/*using NUnit.Framework;

namespace Agent.Tests
{
    [TestFixture]
    class AgentTest
    {
        [Test]
        public void First()
        {
            string[] hashArr = { "4124bc0a9335c27f086f24ba207a4912", "07159c47ee1b19ae4fb9c40d480856c4" };
            string[] passArr = Agent.SearchPassword("Z", "zzzzz", hashArr);
            string[] answer = { "aa", "ba" };
            Assert.That(answer, Is.EqualTo(passArr));
        }

        [Test]
        public void Sec()
        {
            string[] hashArr = { "e62595ee98b585153dac87ce1ab69c3c", "588d39bce7c5fcae6a8529c3997387ea" };
            string[] passArr = Agent.SearchPassword("ZZ", "zzzzz", hashArr);
            string[] answer = { "aab", "hush" };
            Assert.That(answer, Is.EqualTo(passArr));
        }

        [Test]
        public void Trois()
        {
            string[] hashArr = { "5e394281dfac81c1e7dddcaf4d35d1f6" };
            string[] passArr = Agent.SearchPassword("aaaZ", "zzzzz", hashArr);
            string[] answer = { "aabb"};
            Assert.That(answer, Is.EqualTo(passArr));
        }
    }
}
*/