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
    public class OperationTypeController : ControllerBase
    {
        DataContext db;
        public OperationTypeController(DataContext data) => db = data;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationType>>> GetAll()
             => await db.OperationTypes.ToListAsync();
    }
}