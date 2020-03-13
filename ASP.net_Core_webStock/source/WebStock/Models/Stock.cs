using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStock.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<StockInGoods> StockInGoods { get; set; }

    }
}
