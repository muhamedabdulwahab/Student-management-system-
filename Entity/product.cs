using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Entity
{
    public class product
    {
        public int Id            { get; set; }
        public string Name       { get; set; }
        public int Quantity      { get; set; }
        public int Price         { get; set; }
        public DateTime CreatedAt{ get; set; }
        public override string ToString()
        {
            return $"ID is {Id} The name of product is{Name} and Quantity is {Quantity} items and price is {Price}";
        }
    }
}
