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
        public async Task<bool> FindRestarauntYelpId(int yelpId)
        {
            var result = await _database.Table<Restaraunt>().Where(x => x.YelpId == yelpId).FirstOrDefaultAsync();
            if(result.YelpId == yelpId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<Restaraunt>> GetAllRestaraunts(int id)
        {
            return await _database.Table<Restaraunt>().ToListAsync();
        }

        public async Task<Restaraunt> CreateRestaraunt(Restaraunt restaraunt)
        {
            if(restaraunt.Id != 0)
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

        public async Task<List<Restaraunt>> GetAllDishes(int id)
        {
            return await _database.Table<Restaraunt>().ToListAsync();
        }

        public async Task<Dish> CreateDatabase(Dish dish)
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
