using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;

namespace RestaurantDemo.Repositories
{
    public interface IMenuRepository
    {
        public Task<List<Menu>> GetMenu();
        
    }
    public class MenuRepository : IMenuRepository
    {
        private readonly DbConnection _db;
        public MenuRepository(DbConnection dbConnection) { 
            _db = dbConnection;
        }

        public async Task<List<Menu>> GetMenu()
        {
            try
            {
                using(var conn = _db.GetConnection())
                {
                    var menuList = await conn.QueryAsync<Menu>("GetMenu", new { }, commandType: CommandType.StoredProcedure);
                    return menuList.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching Menu", ex);
            }
        }

        //public async Task<Menu> AddMenuItem(Menu menuItem)
        //{
        //    try
        //    {
        //        using (var conn = _db.GetConnection()) 
        //        {
        //            var Id = await conn.ExecuteScalarAsync<int>("AddMenuItem", new
        //            {
        //                ItemName = menuItem.ItemName,
        //                ItemDescription = menuItem.ItemDescription,
        //                ItemPrice = menuItem.ItemPrice,
        //                IsBreakfast = menuItem.IsBreakfast,
        //                IsLunch = menuItem.IsLunch,
        //                IsDinner = menuItem.IsDinner,
        //                IsDessert = menuItem.IsDessert,
        //                IsDrink = menuItem.IsDrink
        //            }, commandType: CommandType.StoredProcedure);
        //            var menuItems = new Menu
        //            {
        //                Id = Id,
        //                ItemName = menuItem.ItemName,
        //                ItemDescription = menuItem.ItemDescription,
        //                ItemPrice = menuItem.ItemPrice
        //            };
        //            return menuItem;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("Error adding new menu item", ex);
        //    }
        //}

        //public async Task<Menu> UpdateMenuItem(Menu menuItem)
        //{
        //    try
        //    {
        //        using (var conn = _db.GetConnection())
        //        {
        //            var parameters = new
        //            {
        //                menuItem.Id,
        //                menuItem.ItemName,
        //                menuItem.ItemDescription,
        //                menuItem.ItemPrice,
        //                menuItem.IsBreakfast,
        //                menuItem.IsLunch,
        //                menuItem.IsDinner,
        //                menuItem.IsDessert,
        //                menuItem.IsDrink
        //            };

        //            var result = await conn.ExecuteAsync("UpdateMenuItem", parameters, commandType: CommandType.StoredProcedure);

        //            if (result > 0)
        //            {
        //                return menuItem; 
        //            }
        //            else
        //            {
        //                throw new ApplicationException("No rows were updated.");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("Error updating menu item", ex);
        //    }
        //}

        //public async Task<bool> DeleteMenuItem(int id)
        //{
        //    try
        //    {
        //        using (var conn = _db.GetConnection())
        //        {
        //            var parameters = new { Id = id };

        //            var result = await conn.ExecuteAsync("DeleteMenuItem", parameters, commandType: CommandType.StoredProcedure);

        //            return result > 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("Error deleting menu item", ex);
        //    }
        //}

    }
}
