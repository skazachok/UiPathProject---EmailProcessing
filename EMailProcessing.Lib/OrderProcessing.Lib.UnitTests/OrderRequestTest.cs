using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OrderProcessing.Lib.UnitTests
{
    [TestClass]
    public class OrderRequestTest
    {
        [TestMethod]
        public void FromEmail()
        {
            string emailBody = File.ReadAllText("OrderRequests\\Order Request-Antonio Moreno Taquería.txt");
            MailMessage email = new MailMessage
            {
                Subject = "Order Request",
                Body = emailBody
            };
            DateTime myDefault = DateTime.Today.AddDays(7);
            DateTime myOrderDate = new DateTime(2018, 12, 4);
            DateTime myRequiredDate = new DateTime(2018, 12, 11);
            
            OrderRequests notice = OrderRequests.FromEmail(email);
            Assert.AreEqual("Antonio Moreno Taquería (ANTON)", notice.Company);
            Assert.AreEqual("Michael Suyama (6)", notice.Orderer);
            Assert.AreEqual(myOrderDate, notice.OrderDate);
            Assert.AreEqual(myRequiredDate, notice.RequiredDate);
            Assert.AreEqual("Speedy Express (1)", notice.Shipper);
            Assert.AreEqual(1554.98M, notice.Freight);
            Assert.AreEqual(myDefault, notice.Shipping);

            ProductDetails details = notice.ProductDetails.Last();
            Assert.AreEqual("Röd Kaviar (73)", details.ProductID);
            Assert.AreEqual(30, details.Quentity);
            Assert.AreEqual(0.03m, details.Discount);
            // Röd Kaviar (73)	30	0.03
        }

        [TestMethod]
        public void TestAllEmails()
        {
            foreach (string fpath in Directory.GetFiles("OrderRequests", "*.txt"))
            {
                MailMessage msg = new MailMessage
                {
                    Subject = "Order Request",
                    Body = File.ReadAllText(fpath)
                };
                Console.WriteLine(fpath);
                OrderRequests notice = OrderRequests.FromEmail(msg);
                Assert.IsTrue(notice.Freight > 0);
                Assert.IsFalse(string.IsNullOrEmpty(notice.Orderer));
                Assert.IsFalse(string.IsNullOrEmpty(notice.Shipper));
                Assert.IsNotNull(notice.Company);
                Assert.IsNotNull(notice.OrderDate);
                Assert.IsNotNull(notice.RequiredDate);
                Assert.IsNotNull(notice.Shipping);
            }
        }
    }
}
