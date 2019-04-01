using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OrderProcessing.Lib.UnitTests
{
    [TestClass]
    public class RestockNoticesTest
    {
        [TestMethod]
        public void FromEmail()
        {
            string emailBody = File.ReadAllText("RestockNotices\\Shipment Received - Cooperativa de Quesos 'Las Cabras'.txt");
            MailMessage email = new MailMessage
            {
                Subject = "Shipment Received",
                Body = emailBody
            };

            RestockNotices notice = RestockNotices.FromEmail(email);
            RestockDetails details = notice.RestockDetails.First();

            Assert.AreEqual(11, details.ProductID);
            Assert.AreEqual("Queso Cabrales", details.ProductName);
            Assert.AreEqual(30, details.Quantity);
        }

        [TestMethod]
        public void TestAllEmails()
        {
            foreach (string fpath in Directory.GetFiles("RestockNotices", "*.txt"))
            {
                MailMessage msg = new MailMessage
                {
                    Subject = "Shipment Received",
                    Body = File.ReadAllText(fpath)
                };
                Console.WriteLine(fpath);
                RestockNotices notice = RestockNotices.FromEmail(msg);
                RestockDetails details = notice.RestockDetails.First();
                Assert.IsTrue(details.Quantity > 0);
                Assert.IsTrue(details.ProductID > 0);
                Assert.IsFalse(string.IsNullOrEmpty(details.ProductName));
                Assert.IsNotNull(details.Quantity);
            }
        }
    }
}
