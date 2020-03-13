using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStock.Models
{
    public class Operation
    {
        public int Id { get; set; }

        public int OperationTypeId { get; set; }
        public OperationType OperationType { get; set; }

        public string OfStock { get; set; }

        public string InStock { get; set; }

        public string Goods { get; set; }

        public int Number { get; set; }

        public string Date { get; set; } 
    }
}
