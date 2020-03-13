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
    public class StockingoodsController : ControllerBase
    {
        DataContext db;
        public StockingoodsController(DataContext data) => db = data;
        /// <summary>
        /// Список всех товаров на складах
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockInGoods>>> GetAll()
        => await db.StockInGoods.ToListAsync();
        /// <summary>
        /// Добавить товар на складе
        /// </summary>
        /// <param name="stockingoods"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<StockInGoods>> PostStockInGoods(StockInGoods stockingoods)
        {
            if (stockingoods == null)
                return BadRequest();

            StockInGoods curStG = db.StockInGoods.FirstOrDefault(q =>
            q.GoodsId == stockingoods.GoodsId && q.StockId == stockingoods.StockId);
            if (curStG != null)
            {
                curStG.Number += stockingoods.Number;
                db.Update(curStG);
            }
            else
            {
                db.StockInGoods.Add(new StockInGoods
                {
                    Number = stockingoods.Number,
                    Stock = db.Stocks.FirstOrDefault(q => stockingoods.StockId == q.Id),
                    Goods = db.Goods.FirstOrDefault(q => stockingoods.GoodsId == q.Id)
                });
            }
            await db.SaveChangesAsync();
            return Ok(stockingoods);
        }
        /// <summary>
        /// Редактировать товар на складе
        /// </summary>
        /// <param name="stockingoods"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<StockInGoods>> Put(StockInGoods stockingoods)
        {
            if (stockingoods == null)
            {
                return BadRequest();
            }
            if (!db.StockInGoods.Any(x => x.Id == stockingoods.Id))
            {
                return NotFound();
            }

            db.Update( new StockInGoods 
            {
                Id = stockingoods.Id,
                Number = stockingoods.Number,
                Stock = db.Stocks.FirstOrDefault(q => stockingoods.StockId == q.Id),
                Goods = db.Goods.FirstOrDefault(q => stockingoods.GoodsId == q.Id)

            }); //(stockingoods);
            await db.SaveChangesAsync();

            return Ok(stockingoods);
        }
        /// <summary>
        /// Удалить товар на складе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<StockInGoods>> Delete(int id)
        {
            StockInGoods stockingoods = db.StockInGoods.FirstOrDefault(x => x.Id == id);
            if (stockingoods == null)
            {
                return NotFound();
            }
            db.StockInGoods.Remove(stockingoods);
            await db.SaveChangesAsync();
            return Ok(stockingoods);
        }
    }
}