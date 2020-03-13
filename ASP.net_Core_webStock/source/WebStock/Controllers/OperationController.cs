using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStock.Models;

namespace WebStock.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        DataContext db;
        public OperationController(DataContext data) => db = data;
        /// <summary>
        /// Все операции
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operation>>> GetAll()
             => await db.Operations.ToListAsync();

        /// <summary>
        /// Добавить операцию
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Operation>> Post(Operation operation)
        {
            if (operation == null)            
                return BadRequest();

            
            if (operation.OperationTypeId == 0)
            {
                int old = db.StockInGoods.FirstOrDefault(q =>
               q.Goods.Name == operation.Goods && q.Stock.Name == operation.OfStock).Number;
                if (operation.Number - old > 0)
                {
                    db.Operations.Add(new Operation()
                    {
                        OperationTypeId = 1,
                        OfStock = operation.OfStock,
                        InStock= " ",
                        Goods = operation.Goods,
                        Number = operation.Number - old,
                        Date = DateTime.Now.ToString()
                    }) ;
                }
                else if (operation.Number - old < 0)
                {
                    db.Operations.Add(new Operation()
                    {
                        OperationTypeId = 2,
                        OfStock = " ",
                        InStock = operation.OfStock,
                        Goods = operation.Goods,
                        Number = Math.Abs(operation.Number - old),
                        Date = DateTime.Now.ToString()
                    });
                }
                else
                    return BadRequest();
            }
            else
            {
                operation.Date = DateTime.Now.ToString();
                db.Operations.Add(operation);
            }

            await db.SaveChangesAsync();
            
            return Ok(operation);
        }
        /// <summary>
        /// Изменить операцию
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Operation>> Put(Operation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }
            if (!db.Operations.Any(x => x.Id == operation.Id))
            {
                return NotFound();
            }

            db.Update(operation);
            await db.SaveChangesAsync();

            return Ok(operation);
        }
        /// <summary>
        /// Удалить операцию
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Operation>> Delete(int id)
        {
            Operation opearion = db.Operations.FirstOrDefault(x => x.Id == id);
            if (opearion == null)
            {
                return NotFound();
            }
            db.Operations.Remove(opearion);
            await db.SaveChangesAsync();
            return Ok(opearion);
        }

    }
}
