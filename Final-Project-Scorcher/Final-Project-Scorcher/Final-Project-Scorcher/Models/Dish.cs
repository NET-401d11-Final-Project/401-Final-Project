using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project_Scorcher.Models
{
    public class Dish
{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal RestarauntDishOffset { get; set; }
        public int TotalVotes { get; set; }
        public decimal TotalLevel { get; set; }
        public decimal AvgLevel { get; set; }
    }
}
