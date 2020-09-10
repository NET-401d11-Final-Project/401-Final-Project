using Final_Project_Scorcher.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Scorcher.Data
{
    public class SeedDishData
    {
        public async static Task SeedRestaurantDataFromYelpId(string yelpId)
        {
            switch (yelpId)
            {
                case "8sZ27zjv8tYxEmx_0dngrA":
                   await SeedJaiThaiDishes();
                    break;
                default:
                    break;
            }
           
        }

        public async static Task<List<Dish>> SeedJaiThaiDishes()
        {

            List<Dish> dishes = new List<Dish>();
            dishes.Add(
            new Dish
            {
                Level = 3,
                Name = "Panang Curry",
                Description = "Spicy red curry with coconut milk, bamboo shoots, chili and sweet basil.",
                Cost = 12.95m,
                RestarauntDishOffset = 0.0m,
                TotalVotes = 0,
                AvgLevel = 3
            }
            );
            dishes.Add(
            new Dish
            {
                Level = 4,
                Name = "Phud Kee Mao",
                Description = "Fresh wide rice noodles stir fried with chili paste, egg and mixed vegetables.",
                Cost = 12.95m,
                RestarauntDishOffset = 0.0m,
                TotalVotes = 0,
                AvgLevel = 4
            }
            );
            dishes.Add(
            new Dish
            {
                Level = 2,
                Name = "Ba Mee Num",
                Description = "Egg noodles, green onion, cilantro, bean sprouts, baby bokchoy, toasted garlic in chicken broth or vegetarian broth.",
                Cost = 10.95m,
                RestarauntDishOffset = 0.0m,
                TotalVotes = 0,
                AvgLevel = 2
            }
            );

            foreach (Dish oneDish in dishes)
            {
                await App.database.CreateDish(oneDish);
            }            

            List<RestarauntDish> restaurantDishes = new List<RestarauntDish>();
            restaurantDishes.Add(
                new RestarauntDish
                {
                    YelpId = "8sZ27zjv8tYxEmx_0dngrA",
                    DishId = 1
                }
            );
            restaurantDishes.Add(
                new RestarauntDish
                {
                    YelpId = "8sZ27zjv8tYxEmx_0dngrA",
                    DishId = 2
                }
            );
            restaurantDishes.Add(
                new RestarauntDish
                {
                    YelpId = "8sZ27zjv8tYxEmx_0dngrA",
                    DishId = 3
                }
            );
            foreach (var dish in restaurantDishes)
            {
                await App.database.CreateRestarauntDish(dish);
            }

            return dishes;
        }
    }
}

//public class Dish
//{
//    [PrimaryKey, AutoIncrement]
//    public int Id { get; set; }
//    public int Level { get; set; }
//    public string Name { get; set; }
//    public string Description { get; set; }
//    public decimal Cost { get; set; }
//    public decimal RestarauntDishOffset { get; set; }
//    public int TotalVotes { get; set; }
//    public decimal AvgLevel { get; set; }
//}


//public class RestarauntDish
//{
//    [PrimaryKey, AutoIncrement]
//    public int Id { get; set; }
//    public int RestarauntId { get; set; }
//    public string YelpId { get; set; }
//    public int DishId { get; set; }


//}