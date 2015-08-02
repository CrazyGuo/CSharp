using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCodes.UnitTest
{
    [TestFixture]
    public class MyStringClassTests
    {
        [Test]
        public void TestReverse()
        {
            var reverse = new MyStringClass();
            var strResult = reverse.Reverse("hello");
            Assert.That(strResult, Is.EqualTo("olleh"));
        }

        [Test]
        public void TestReverseWithNull()
        {
            var reverse = new MyStringClass();
            var strResult = reverse.Reverse(null);
            Assert.That(strResult, Is.Null);
        }
    }
}
