using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project_Scorcher.Models
{
    public class Restaraunt
{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int YelpId { get; set; }
        public decimal RestarauntOffset { get; set; }
        public int LevelMin { get; set; }
        public int LevelMax { get; set; }
        public List<RestarauntDish> RestarauntDishes { get; set; }



    }
}
