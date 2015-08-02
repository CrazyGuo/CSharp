using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonServiceImpl;

namespace MyUnitTest.UnitTest
{
    [TestFixture]
    public class AopTest
    {
        [Test]
        public void TestAopService()
        {
            //IExtendService service = new SportService();
            //service.GetDeleteSqlId();

            //AopTestService s = new AopTestService();
            //s.GetDeleteSqlId();
            SportAopService ss = new SportAopService();
            ss.GetDeleteSqlId();
        }
    }
}
