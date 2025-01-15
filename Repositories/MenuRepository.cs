using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;

namespace RestaurantDemo.Repositories
{
    public interface IMenuRepository
    {
        List<Menu> GetMenu();
        int AddMenuItem(Menu menuItem);
        string DeleteMenuItem(int id);
        string UpdateMenuItem(Menu menu);
    }
    public class MenuRepository : IMenuRepository
    {
        private readonly DbConnection _db;
        public MenuRepository(DbConnection dbConnection) { 
            _db = dbConnection;
        }

        public List<Menu> GetMenu()
        {
            List<Menu> menuItems = new List<Menu>();
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using(var cmd = new SqlCommand("GetMenu", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    try
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var menu = new Menu()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                    ItemName = reader.GetString(reader.GetOrdinal("ItemName")),
                                    ItemDescription = reader.GetString(reader.GetOrdinal("ItemDescrition")),
                                    ItemPrice = reader.GetString(reader.GetOrdinal("ItemPrice")),
                                    IsBreakfast = reader.GetBoolean(reader.GetOrdinal("isBreakfast")),
                                    IsLunch = reader.GetBoolean(reader.GetOrdinal("isLunch")),
                                    IsDinner = reader.GetBoolean(reader.GetOrdinal("isDinner")),
                                    IsDessert = reader.GetBoolean(reader.GetOrdinal("isDessert")),
                                    IsDrink = reader.GetBoolean(reader.GetOrdinal("isDrink"))
                                };
                                menuItems.Add(menu);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error fetching Menu", ex);
                    }
                }
            }
            return menuItems;
        }

        public int AddMenuItem(Menu menuItem)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("AddMenuItem", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@ItemName", menuItem.ItemName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ItemDescrition", menuItem.ItemDescription ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ItemPrice", menuItem.ItemPrice ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@isBreakfast", menuItem.IsBreakfast);
                    cmd.Parameters.AddWithValue("@isLunch", menuItem.IsLunch);
                    cmd.Parameters.AddWithValue("@isDinner", menuItem.IsDinner);
                    cmd.Parameters.AddWithValue("@isDessert", menuItem.IsDessert);
                    cmd.Parameters.AddWithValue("@isDrink", menuItem.IsDrink);
                   // cmd.Parameters.AddWithValue("@CreatedBy", 1);

                    try
                    {
                        var result = cmd.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error adding new menu item", ex);
                    }
                }
            }
        }

        public string DeleteMenuItem(int id)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("DeleteMenuItem", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        var result = cmd.ExecuteScalar();
                        return "Menu item Deleted Successfully!!";
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error deleting menu item", ex);
                    }
                }
            }
        }

        public string UpdateMenuItem(Menu menuItem)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("UpdateMenuItem", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@ID", menuItem.Id);
                    cmd.Parameters.AddWithValue("@ItemName", menuItem.ItemName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ItemDescrition", menuItem.ItemDescription ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ItemPrice", menuItem.ItemPrice ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@isBreakfast", menuItem.IsBreakfast);
                    cmd.Parameters.AddWithValue("@isLunch", menuItem.IsLunch);
                    cmd.Parameters.AddWithValue("@isDinner", menuItem.IsDinner);
                    cmd.Parameters.AddWithValue("@isDessert", menuItem.IsDessert);
                    cmd.Parameters.AddWithValue("@isDrink", menuItem.IsDrink);

                    try
                    {
                        int res=cmd.ExecuteNonQuery();
                        if (res > 0)
                            return "Menu item updated successfully";
                        else return "Nothig is updated";
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error updating menu item", ex);
                    }
                }
            }
        }

    }
}
