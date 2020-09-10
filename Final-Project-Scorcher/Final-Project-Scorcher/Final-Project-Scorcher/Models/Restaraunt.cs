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
        public string YelpId { get; set; }
        public string YelpCategory { get; set; }
        public decimal RestarauntOffset { get; set; }
        public int LevelMin { get; set; }
        public int LevelMax { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

    }
}