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
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Date { get; set; }

    }
}
