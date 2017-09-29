using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;

namespace Study.Motion.Web
{
    [TestFixture]
    public class TestCase
    {
        [Test]
        public void Jieba_Should_Success()
        {
            var detail = JiebaExecute.GetMotionDialogDetail("颱風天來了，好可怕", "1");
            //Assert.AreEqual();
        }

    }
}