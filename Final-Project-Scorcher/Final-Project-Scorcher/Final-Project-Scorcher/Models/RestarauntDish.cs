using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project_Scorcher.Models
{
    public class RestarauntDish
{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int RestarauntId { get; set; }
        public int DishId { get; set; }


    }
}
