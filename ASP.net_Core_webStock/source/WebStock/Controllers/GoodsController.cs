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
    public class GoodsController : ControllerBase
    {

        DataContext db;
        public GoodsController(DataContext data) => db = data;
        /// <summary>
        /// Список всех продуктов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        => await db.Goods.ToListAsync();
        /// <summary>
        /// Добавить продукт
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            if (product == null)
                return BadRequest();

            db.Goods.Add(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }
        /// <summary>
        /// Редактировать продукт
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Product>> Put(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            if (!db.Goods.Any(x => x.Id == product.Id))
            {
                return NotFound();
            }

            db.Update(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }
        /// <summary>
        /// Удалить продукт
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stock>> Delete(int id)
        {
            Product product = db.Goods.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            db.Goods.Remove(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }
    }
}