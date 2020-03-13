using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStock.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Article { get; set; }
        /// <summary>
        /// Вес
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Обьем
        /// </summary>
        public int Size { get; set; }

        public List<StockInGoods> StockInGoods { get; set; }

        public List<Operation> Operations { get; set; }
    }
}
