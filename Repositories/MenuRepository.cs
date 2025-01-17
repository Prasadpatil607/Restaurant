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
        public Task<Menu> AddMenuItem(Menu menuItem);
        //public Task<Menu> DeleteMenuItem(int id);
        //public Task<Menu> UpdateMenuItem(Menu menu);
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

        public async Task<Menu> AddMenuItem(Menu menuItem)
        {
            try
            {
                using (var conn = _db.GetConnection()) 
                {
                    var Id = await conn.ExecuteScalarAsync<int>("AddMenuItem", new
                    {
                        ItemName = menuItem.ItemName,
                        ItemDescription = menuItem.ItemDescription,
                        ItemPrice = menuItem.ItemPrice,
                        IsBreakfast = menuItem.IsBreakfast,
                        IsLunch = menuItem.IsLunch,
                        IsDinner = menuItem.IsDinner,
                        IsDessert = menuItem.IsDessert,
                        IsDrink = menuItem.IsDrink
                    }, commandType: CommandType.StoredProcedure);
                    var menuItems = new Menu
                    {
                        Id = Id,
                        ItemName = menuItem.ItemName,
                        ItemDescription = menuItem.ItemDescription,
                        ItemPrice = menuItem.ItemPrice
                    };
                    return menuItem;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error adding new menu item", ex);
            }
        }

        //public string DeleteMenuItem(int id)
        //{
        //    using (var conn = _db.GetConnection())
        //    {
        //        conn.Open();
        //        using (var cmd = new SqlCommand("DeleteMenuItem", conn)
        //        {
        //            CommandType = System.Data.CommandType.StoredProcedure
        //        })
        //        {
        //            cmd.Parameters.AddWithValue("@Id", id);

        //            try
        //            {
        //                var result = cmd.ExecuteScalar();
        //                return "Menu item Deleted Successfully!!";
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new ApplicationException("Error deleting menu item", ex);
        //            }
        //        }
        //    }
        //}

        //public string UpdateMenuItem(Menu menuItem)
        //{
        //    using (var conn = _db.GetConnection())
        //    {
        //        conn.Open();
        //        using (var cmd = new SqlCommand("UpdateMenuItem", conn)
        //        {
        //            CommandType = System.Data.CommandType.StoredProcedure
        //        })
        //        {
        //            cmd.Parameters.AddWithValue("@ID", menuItem.Id);
        //            cmd.Parameters.AddWithValue("@ItemName", menuItem.ItemName ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@ItemDescrition", menuItem.ItemDescription ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@ItemPrice", menuItem.ItemPrice ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@isBreakfast", menuItem.IsBreakfast);
        //            cmd.Parameters.AddWithValue("@isLunch", menuItem.IsLunch);
        //            cmd.Parameters.AddWithValue("@isDinner", menuItem.IsDinner);
        //            cmd.Parameters.AddWithValue("@isDessert", menuItem.IsDessert);
        //            cmd.Parameters.AddWithValue("@isDrink", menuItem.IsDrink);

        //            try
        //            {
        //                int res=cmd.ExecuteNonQuery();
        //                if (res > 0)
        //                    return "Menu item updated successfully";
        //                else return "Nothig is updated";
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new ApplicationException("Error updating menu item", ex);
        //            }
        //        }
        //    }
        //}

    }
}
