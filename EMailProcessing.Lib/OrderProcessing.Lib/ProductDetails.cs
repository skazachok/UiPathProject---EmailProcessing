using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Lib
{
    public class ProductDetails 
    {
        internal ProductDetails(string detailLine)
        {
            string[] parts = detailLine.Split('\t');
            ProductID = parts[0];
            Quentity = int.Parse(parts[1]);
            Discount = decimal.Parse(parts[2]);
        }

        public string ProductID { get; internal set; }
        public int Quentity { get; internal set; }
        public decimal Discount { get; internal set; }
    }
}
