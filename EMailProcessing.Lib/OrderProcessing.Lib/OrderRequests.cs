using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderProcessing.Lib
{
    public class OrderRequests
    {
        private static readonly Regex myRegEx =
            new Regex(@":\t(.*)[^\n]", RegexOptions.Compiled);
        private static readonly Regex myRegEx2 = new Regex(@"(.*)\t(\d*)\t(.*)", RegexOptions.Compiled);
        private static readonly char[] delimiters = new char[] { '-' };

        public static OrderRequests FromEmail(MailMessage email)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));
            if (email.Subject != "Order Request")
                throw new ArgumentNullException($"Wrong email typ: {email.Subject}");

            DateTime myDefault = DateTime.Today.AddDays(7);
            MatchCollection myCollection = myRegEx.Matches(email.Body);

            OrderRequests notice = new OrderRequests
            {
                Company = myCollection[0].Groups[1].Value,
                Orderer = myCollection[1].Groups[1].Value,
                Shipper = myCollection[4].Groups[1].Value,
                Freight = decimal.Parse(myCollection[5].Groups[1].Value.Substring(1))
            };
            
            if (myCollection[2].Groups[1].Value == "default")
            {
                notice.OrderDate = myDefault;
            } else
            {
                string[] myString = myCollection[2].Groups[1].Value.Split(delimiters);
                DateTime myDateTime = new DateTime(int.Parse(myString[2]), int.Parse(myString[0]), int.Parse(myString[1]));
                notice.OrderDate = myDateTime;
            }

            if (myCollection[3].Groups[1].Value == "default")
            {
                notice.RequiredDate = myDefault;
            }
            else
            {
                string[] myString = myCollection[3].Groups[1].Value.Split(delimiters);
                DateTime myDateTime = new DateTime(int.Parse(myString[2]), int.Parse(myString[0]), int.Parse(myString[1]));
                notice.RequiredDate = myDateTime;
            }
            if (myCollection[6].Groups[1].Value == "default")
            {
                notice.Shipping = myDefault;
            }
            else
            {
                string[] myString = myCollection[6].Groups[1].Value.Split(delimiters);
                DateTime myDateTime = new DateTime(int.Parse(myString[2]), int.Parse(myString[0]), int.Parse(myString[1]));
                notice.Shipping = myDateTime;
            }

            MatchCollection myMatchCollection = myRegEx2.Matches(email.Body);
            List<ProductDetails> productDetailsList = new List<ProductDetails>();

            foreach (Match a in myMatchCollection)
            {
                ProductDetails  myDetails = new ProductDetails(a.Value);
                productDetailsList.Add(myDetails);
            }
            notice.ProductDetails = productDetailsList;
            
            return notice;
            
        }

        public string Company { get; private set; }
        public string Orderer { get; private set; }
        public DateTime OrderDate { get; private set; }
        public DateTime RequiredDate { get; private set; }
        public string Shipper { get; private set; }
        public decimal Freight { get; private set; }
        public DateTime Shipping { get; private set; } 
        public IReadOnlyCollection<ProductDetails> ProductDetails { get; private set; }
    }
}