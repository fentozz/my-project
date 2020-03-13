using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace WebStock.Models
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// Склады
        /// </summary>
        public DbSet<Stock> Stocks { get; set; }
        /// <summary>
        /// Товары
        /// </summary>
        public DbSet<Product> Goods { get; set; }
        /// <summary>
        /// Товары на складах
        /// </summary>
        public DbSet<StockInGoods> StockInGoods { get; set; }
        /// <summary>
        /// Типы операций
        /// </summary>
        public DbSet<OperationType> OperationTypes { get; set; }
        /// <summary>
        /// Операции
        /// </summary>
        public DbSet<Operation> Operations { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperationType>().HasData(
                new OperationType[] {
                     new OperationType { Id = 1, NameType ="Приход"},
                     new OperationType { Id = 2, NameType ="Расход"},
                     new OperationType { Id = 3, NameType ="Внутреннее перемещение"}
                }) ;

            modelBuilder.Entity<Stock>().HasData(
                new Stock[]
                {
                    new Stock {Id = 1, Name = "Первый склад"}
                }) ;



        }
    }
}
