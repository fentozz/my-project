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
    public class StockController : ControllerBase
    {
        DataContext db;
        public StockController(DataContext data) => db = data;

        /// <summary>
        /// Список всех складов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetAll()
        => await db.Stocks.ToListAsync();
        /// <summary>
        /// Добавить склад
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Stock>> Post(Stock stock)
        {
            if (stock == null)
            {
                return BadRequest();
            }
            if (!db.Stocks.Any(q => q.Name == stock.Name))
            {
                db.Stocks.Add(stock);
                await db.SaveChangesAsync();
            }
            return Ok(stock);
        }
        /// <summary>
        /// Редактировать склад
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Stock>> Put(Stock stock)
        {
            if (stock == null)
            {
                return BadRequest();
            }
            if (!db.Stocks.Any(x => x.Id == stock.Id) )
            {
                return NotFound();
            }
            
            db.Update(stock);
            await db.SaveChangesAsync();
            
            return Ok(stock);
        }
        /// <summary>
        /// Удалить склад
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stock>> Delete(int id)
        {
            Stock stock = db.Stocks.FirstOrDefault(x => x.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            db.Stocks.Remove(stock);
            await db.SaveChangesAsync();
            return Ok(stock);
        }
    }
}