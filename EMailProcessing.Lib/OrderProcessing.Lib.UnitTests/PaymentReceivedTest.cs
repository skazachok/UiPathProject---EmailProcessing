using System;
using System.IO;
using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OrderProcessing.Lib.UnitTests
{
    [TestClass]
    public class PaymentReceivedTest
    {
        [TestMethod]
        public void FromEmail()
        {
            string emailBody = File.ReadAllText("PaymentReceived\\Payment Received - Bólido Comidas preparadas.txt");
            MailMessage email = new MailMessage
            {
                Subject = "Payment Received",
                Body = emailBody
            };

            DateTime myOrderDate = new DateTime(2019, 3, 12);

            PaymentReceived notice = PaymentReceived.FromEmail(email);
            Assert.AreEqual(545.98M, notice.Amount);
            Assert.AreEqual(11078, notice.OrderNumber);
            Assert.AreEqual(myOrderDate, notice.OrderDate);
            Assert.AreEqual(495, notice.CheckNumber);
        }

        [TestMethod]
        public void TestAllEmails()
        {
            foreach (string fpath in Directory.GetFiles("PaymentReceived", "*.txt"))
            {
                MailMessage msg = new MailMessage
                {
                    Subject = "Payment Received",
                    Body = File.ReadAllText(fpath)
                };
                Console.WriteLine(fpath);
                PaymentReceived notice = PaymentReceived.FromEmail(msg);
                Assert.IsTrue(notice.Amount > 0);
                Assert.IsTrue(notice.OrderNumber > 0);
                Assert.IsNotNull(notice.CheckNumber);
                Assert.IsNotNull(notice.OrderDate);
                Assert.IsNotNull(notice.CheckNumber);
                Assert.IsTrue(notice.CheckNumber > 0);
            }
        }
    }
}
