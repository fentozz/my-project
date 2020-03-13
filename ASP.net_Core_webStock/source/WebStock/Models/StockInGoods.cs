using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStock.Models
{
    public class StockInGoods
    {
        public int Id { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public int GoodsId { get; set; }
        public Product Goods { get; set; }

        public int Number { get; set; }
    }
}
