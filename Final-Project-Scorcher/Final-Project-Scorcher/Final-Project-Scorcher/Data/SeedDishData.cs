using Final_Project_Scorcher.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Scorcher.Data
{
    public class SeedDishData
    {
        /// <summary>
        /// the following method puts dish data into our Yelp API origin restaurants
        /// </summary>
        /// <param name="yelpId">the assigned yelp id each restaurant has</param>
        /// <returns>seeded data within restaurants (dishes for a user to peruse)</returns>
        public async static Task SeedRestaurantDataFromYelpId(string yelpId)
        {
            switch (yelpId)
            {
                case "8sZ27zjv8tYxEmx_0dngrA":
                    await SeedJaiThaiDishes();
                    break;
                case "lVSi-ilqM8FzT_HnK7cQ4A":
                    await SeedIlTerrazzoCarmineDishes();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// the following method seeds dish data into the Jai Thai restaurant
        /// </summary>
        /// <returns>dish data when one "taps" on the Jai Thai restaurant</returns>
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
                    RestaurantDishOffset = 0.0m,
                    TotalVotes = 0,
                    AvgLevel = 0
                }
            );
            dishes.Add(
            new Dish
                {
                    Level = 4,
                    Name = "Phud Kee Mao",
                    Description = "Fresh wide rice noodles stir fried with chili paste, egg and mixed vegetables.",
                    Cost = 12.95m,
                    RestaurantDishOffset = 0.0m,
                    TotalVotes = 0,
                    AvgLevel = 0
                }
            );
            dishes.Add(
            new Dish
                {
                    Level = 2,
                    Name = "Ba Mee Num",
                    Description = "Egg noodles, green onion, cilantro, bean sprouts, baby bokchoy, toasted garlic in chicken broth or vegetarian broth.",
                    Cost = 10.95m,
                    RestaurantDishOffset = 0.0m,
                    TotalVotes = 0,
                    AvgLevel = 0
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
            foreach (RestarauntDish oneRestDish in restaurantDishes)
            {
                await App.database.CreateRestarauntDish(oneRestDish);
            }

            return dishes;
        }
        /// <summary>
        /// the following method seeds dish data into the Il Terrazzo Carmine restaurant
        /// </summary>
        /// <returns>dish data when one "taps" on the Italian restaurant</returns>
        public async static Task<List<Dish>> SeedIlTerrazzoCarmineDishes()
        {

            List<Dish> dishes = new List<Dish>();
            dishes.Add(
            new Dish
            {
                Level = 2,
                Name = "Parmigiana Di Melanzane",
                Description = "Eggplant baked with tomato sauce and mozzarella",
                Cost = 16.00m,
                RestaurantDishOffset = 0.0m,
                TotalVotes = 0,
                AvgLevel = 0
            }
            );
            dishes.Add(
            new Dish
            {
                Level = 4,
                Name = "Rigatoni Bolognese",
                Description = "House ground veal, pork and beef with tomatoes, herbs, and red wine.",
                Cost = 21.00m,
                RestaurantDishOffset = 0.0m,
                TotalVotes = 0,
                AvgLevel = 0
            }
            );
            dishes.Add(
            new Dish
            {
                Level = 3,
                Name = "Ossobuco",
                Description = "Center cut veal shank braised in wine and vegetables, served with fettuccine al burro.",
                Cost = 10.95m,
                RestaurantDishOffset = 0.0m,
                TotalVotes = 0,
                AvgLevel = 0
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
                    YelpId = "lVSi-ilqM8FzT_HnK7cQ4A",
                    DishId = 4
                }
            );
            restaurantDishes.Add(
                new RestarauntDish
                {
                    YelpId = "lVSi-ilqM8FzT_HnK7cQ4A",
                    DishId = 5
                }
            );
            restaurantDishes.Add(
                new RestarauntDish
                {
                    YelpId = "lVSi-ilqM8FzT_HnK7cQ4A",
                    DishId = 6
                }
            );
            foreach (RestarauntDish oneRestDish in restaurantDishes)
            {
                await App.database.CreateRestarauntDish(oneRestDish);
            }

            return dishes;
        }
    }
}
