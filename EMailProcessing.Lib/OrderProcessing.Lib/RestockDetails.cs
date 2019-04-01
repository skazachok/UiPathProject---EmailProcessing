using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Lib
{
    public class RestockDetails
    {
        internal RestockDetails(string detailLine)
        {
            string[] parts = detailLine.Split('\t');
            ProductID = int.Parse(parts[0]);
            ProductName = parts[1];
            Quantity = int.Parse(parts[2]);
        }

        public int ProductID { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
    }
}
