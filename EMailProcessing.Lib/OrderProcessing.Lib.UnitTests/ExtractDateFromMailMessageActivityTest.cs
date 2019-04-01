using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OrderProcessing.Lib.UnitTests
{
    [TestClass]
    public class ExtractDateFromMailMessageActivityTest
    {
        [TestMethod]
        public void ParseDate()
        {
            //PrivateObject po = new PrivateObject(typeof(ExtractDateFromMailMessageActivity));
            //DateTime result = (DateTime)po.Invoke("ParseDate", "15 Mar 2019 09:36:49 -0400");
            DateTime result = ExtractDateFromMailMessageActivity.ParseDate("15 Mar 2019 09:36:49 -0400");
            Assert.AreEqual(new DateTime(2019, 3, 15, 9, 36, 49), result);
        }
    }
}
