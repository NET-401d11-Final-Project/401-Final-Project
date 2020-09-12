using Final_Project_Scorcher.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Scorcher.Data
{
    public class ScorcherDatabase
    {
        readonly SQLiteAsyncConnection _database;

        /// <summary>
        /// Constructor that creates all 3 tables
        /// </summary>
        /// <param name="DbPath">The path of where the database is to be saved at</param>
        public ScorcherDatabase(string DbPath)
        {
            _database = new SQLiteAsyncConnection(DbPath);
            _database.CreateTableAsync<Restaraunt>().Wait();
            _database.CreateTableAsync<RestarauntDish>().Wait();
            _database.CreateTableAsync<Dish>().Wait();
        }

        /// <summary>
        /// Running this method will drop all the tables from the database
        /// </summary>
        public async void DeleteAllData()
        {
            await _database.DropTableAsync<Dish>();
            await _database.DropTableAsync<RestarauntDish>();
            await _database.DropTableAsync<Restaraunt>();

        }

        /// <summary>
        /// This method will find a restaurant in the database by the YelpId property
        /// </summary>
        /// <param name="yelpId">The id property assignment that gets saved from the Yelp API call</param>
        /// <returns>Returns the Restaurant object</returns>
        public async Task<Restaraunt> FindRestarauntYelpId(string yelpId)
        {
            return await _database.Table<Restaraunt>().Where(x => x.YelpId == yelpId).FirstOrDefaultAsync();

        }
        public async Task<List<Restaraunt>> GetAllRestaraunts(string searchTerm)
        {
            return await _database.Table<Restaraunt>()
                .Where(x => x.YelpCategory == searchTerm)
                .ToListAsync();
        }

        /// <summary>
        /// Updates the offset for therestaurant associated with the YelpId
        /// </summary>
        /// <param name="yelpId">Yelp Id associated with a particular restaurant</param>
        /// <returns></returns>
        public async Task UpdateRestarauntOffSet(string yelpId)
        {
            List<Dish> restaurantDishes = await GetAllDishesByYelpId(yelpId);
            int count = 0;
            decimal totalOffset = 0;

            foreach(var item in restaurantDishes)
            {
                if (item.RestaurantDishOffset != 0)
                {
                    totalOffset += item.RestaurantDishOffset;
                    count++;
                }
            }
            decimal offSet = totalOffset / count;
            Restaraunt restaraunt = await FindRestarauntYelpId(yelpId);
            restaraunt.RestarauntOffset = Math.Round(offSet, 1);
            await CreateRestaraunt(restaraunt);
        }

        /// <summary>
        /// Given the lat and long of the users location and the restaurants lat and long determiens if its within a 10 miles radius
        /// </summary>
        /// <param name="lat1">users latitude</param>
        /// <param name="lon1">users longiotude</param>
        /// <param name="lat2">restaurants latitude</param>
        /// <param name="lon2">restaurant longitude</param>
        /// <returns>distance</returns>
        private double CalculateLocationRadius(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371e3; // metres
            var φ1 = lat1 * Math.PI / 180; // φ, λ in radians
            var φ2 = lat2 * Math.PI / 180;
            var Δφ = (lat2 - lat1) * Math.PI / 180;
            var Δλ = (lon2 - lon1) * Math.PI / 180;
            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                      Math.Cos(φ1) * Math.Cos(φ2) *
                      Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c; // in metres

        }

        /// <summary>
        /// Retrieves all the restaurants within a 10 miles radius of the user
        /// </summary>
        /// <param name="lat">users latitude</param>
        /// <param name="lon">users longitude</param>
        /// <param name="searchTerm">Business Name or Cuisine type to search the database for</param>
        /// <returns>List of restaurants matching the Business name or Cuisine type within a 10 mile raidus of the user</returns>
        public async Task<List<Restaraunt>> GetAllRestarauntsByLocation(double lat, double lon, string searchTerm)
        {
            var list = await GetAllRestaraunts(searchTerm);
            List<Restaraunt> result = new List<Restaraunt>();
            foreach (var item in list)
            {
                var d = CalculateLocationRadius(item.Lat, item.Lon, lat, lon);
                if (d < 16093)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public async Task<Restaraunt> UpdateRestaraunt(Restaraunt restaraunt)
        {
            await _database.UpdateAsync(restaraunt);
            return await GetRestaraunt(restaraunt.Id);
        }

        /// <summary>
        /// Creates the restaurant if it doesn't already exist otherwise it updates the restaurant in the database
        /// </summary>
        /// <param name="restaraunt">Restaurant to be updated</param>
        /// <returns>the restaurant after being updated</returns>
        public async Task<Restaraunt> CreateRestaraunt(Restaraunt restaraunt)
        {
            if (restaraunt.Id != 0)
            {
                await _database.UpdateAsync(restaraunt);
                return restaraunt;
            }
            else
            {
                int id = await _database.InsertAsync(restaraunt);
                restaraunt.Id = id;
                return restaraunt;
            }
        }

        /// <summary>
        /// Retrieves the restaurant from the databse
        /// </summary>
        /// <param name="id">the ide of the restaurant to be retrieved</param>
        /// <returns>the restaurant</returns>
        public async Task<Restaraunt> GetRestaraunt(int id)
        {
            return await _database.Table<Restaraunt>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Deletes a restaurant from the database
        /// </summary>
        /// <param name="restaraunt">The restaurant to be deleted</param>
        /// <returns>The restaurant that was deleted</returns>
        public async Task<Restaraunt> DeleteRestaraunt(Restaraunt restaraunt)
        {
            await _database.DeleteAsync(restaraunt);
            return restaraunt;
        }

        /// <summary>
        /// Retrieves all the dishes associated with a Restaurant
        /// </summary>
        /// <param name="id">The id of the restaurant of dishes to be retrieved</param>
        /// <returns>List of dishes associated with a particular Restaurant</returns>
        public async Task<List<RestarauntDish>> GetAllRestarauntDishes(int id)
        {
            return await _database.Table<RestarauntDish>().ToListAsync();
        }

        /// <summary>
        /// Updates or adds a RestaurantDish entity to the database.
        /// </summary>
        /// <param name="restarauntDish">
        /// RestaurantDish: the entity to update or add.
        /// </param>
        /// <returns>
        /// Task<RestaurantDish>: the updated or created RestaurantDish entity.
        /// </returns>
        public async Task<RestarauntDish> CreateRestarauntDish(RestarauntDish restarauntDish)
        {
            if (restarauntDish.Id != 0)
            {
                await _database.UpdateAsync(restarauntDish);
                return restarauntDish;
            }
            else
            {
                int id = await _database.InsertAsync(restarauntDish);
                restarauntDish.Id = id;
                return restarauntDish;
            }
        }

        /// <summary>
        /// Retrieves the RestaurantDish that contains the association between a dish and a restaurant
        /// </summary>
        /// <param name="id">The id of the restaurant dish to retrieved from the database</param>
        /// <returns>the restaurant dish that was retrieved from the database</returns>
        public async Task<RestarauntDish> GetRestarauntDish(int id)
        {
            return await _database.Table<RestarauntDish>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Deletes a RestaurantDish entity from the database.
        /// </summary>
        /// <param name="restarauntDish">
        /// RestaurantDish: the entity to be deleted.
        /// </param>
        /// <returns>
        /// RestaurantDish: the deleted entity.
        /// </returns>
        public async Task<RestarauntDish> DeleteRestarauntDish(RestarauntDish restarauntDish)
        {
            await _database.DeleteAsync(restarauntDish);
            return restarauntDish;
        }

        /// <summary>
        /// Gets all dishes, for all restaurants, in the database.
        /// </summary>
        /// <returns>
        /// Task<List<Dish>>: a collection of all dishes in the database.
        /// </returns>
        public async Task<List<Dish>> GetAllDishes()
        {
            return await _database.Table<Dish>().ToListAsync();
        }

        /// <summary>
        /// Gets all Dish objects for a restaurant from the restaurant's YelpId.
        /// </summary>
        /// <param name="yelpId">
        /// string: the YelpID for a given restaurant.
        /// </param>
        /// <returns>
        /// Task<List<Dish>>: a collection of all dishes for a given restaurant with a given YelpId.
        /// </returns>
        public async Task<List<Dish>> GetAllDishesByYelpId(string yelpId)
        {
           var dishId = await _database.Table<RestarauntDish>().Where(x => x.YelpId == yelpId).ToListAsync();
           List<Dish> dishList = new List<Dish>();
           foreach (var item in dishId)
           {
               dishList.Add(await GetDish(item.Id));
           }
           return dishList;
        }

        /// <summary>
        /// the below method allows one to query the yelp id of a dish
        /// </summary>
        /// <param name="dishId">the dish whose yelp id is requested</param>
        /// <returns>the Yelp Id</returns>
        public async Task<string> ReturnYelpIdByDish(int dishId)
        {
            var yelpId = await _database.Table<RestarauntDish>().Where(x => x.DishId == dishId).FirstOrDefaultAsync();
            return yelpId.YelpId;
        }

        /// <summary>
        /// allows the creation of a new dish if the dish already exists it updates the dish in the database
        /// </summary>
        /// <param name="dish">the dish that is to be created</param>
        /// <returns>the created dish</returns>
        public async Task<Dish> CreateDish(Dish dish)
        {
            if (dish.Id != 0)
            {
                await _database.UpdateAsync(dish);
                return dish;
            }
            else
            {
                int id = await _database.InsertAsync(dish);
                dish.Id = id;
                return dish;
            }
        }

        /// <summary>
        /// the below method allows one to to access a particular dish
        /// </summary>
        /// <param name="id">the id of the dish that is being accessed</param>
        /// <returns>the dish in question</returns>
        public async Task<Dish> GetDish(int id)
        {
            return await _database.Table<Dish>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// the below method allows the deletion of a dish
        /// </summary>
        /// <param name="dish">the dish to be deleted</param>
        /// <returns>the successful completion of the deletion task</returns>
        public async Task<Dish> DeleteDish(Dish dish)
        {
            await _database.DeleteAsync(dish);
            return dish;
        }


    }
}