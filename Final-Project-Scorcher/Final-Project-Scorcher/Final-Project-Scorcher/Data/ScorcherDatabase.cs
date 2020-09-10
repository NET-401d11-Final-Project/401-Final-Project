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

        public ScorcherDatabase(string DbPath)
        {
            _database = new SQLiteAsyncConnection(DbPath);
            _database.CreateTableAsync<Restaraunt>().Wait();
            _database.CreateTableAsync<RestarauntDish>().Wait();
            _database.CreateTableAsync<Dish>().Wait();
        }

        public async void DeleteAllRestaurants()
        {
            await _database.DropTableAsync<Dish>();
            await _database.DropTableAsync<RestarauntDish>();
            await _database.DropTableAsync<Restaraunt>();

        }

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

        public async Task UpdateRestarauntOffSet(string yelpId)
        {
            List<Dish> restaurantDishes = await GetAllDishesByYelpId(yelpId);
            int count = restaurantDishes.Count;
            decimal totalOffset = 0;

            foreach(var item in restaurantDishes)
            {
                totalOffset += item.RestaurantDishOffset;
            }
            decimal offSet = totalOffset / count;
            Restaraunt restaraunt = await FindRestarauntYelpId(yelpId);
            restaraunt.RestarauntOffset = offSet;
            await CreateRestaraunt(restaraunt);
        }

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

        public async Task<Restaraunt> GetRestaraunt(int id)
        {
            return await _database.Table<Restaraunt>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Restaraunt> DeleteRestaraunt(Restaraunt restaraunt)
        {
            await _database.DeleteAsync(restaraunt);
            return restaraunt;
        }


        public async Task<List<RestarauntDish>> GetAllRestarauntDishes(int id)
        {
            return await _database.Table<RestarauntDish>().ToListAsync();
        }

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

        public async Task<RestarauntDish> GetRestarauntDish(int id)
        {
            return await _database.Table<RestarauntDish>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<RestarauntDish> DeleteRestarauntDish(RestarauntDish restarauntDish)
        {
            await _database.DeleteAsync(restarauntDish);
            return restarauntDish;
        }

        public async Task<List<Dish>> GetAllDishes()
        {
            return await _database.Table<Dish>().ToListAsync();
        }

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

        public async Task<string> ReturnYelpIdByDish(int dishId)
        {
            var yelpId = await _database.Table<RestarauntDish>().Where(x => x.DishId == dishId).FirstOrDefaultAsync();
            return yelpId.YelpId;
        }

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

        public async Task<Dish> GetDish(int id)
        {
            return await _database.Table<Dish>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Dish> DeleteDish(Dish dish)
        {
            await _database.DeleteAsync(dish);
            return dish;
        }


    }
}