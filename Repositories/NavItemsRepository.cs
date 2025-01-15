using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;
using System;
using System.Collections.Generic;

namespace RestaurantDemo.Repositories
{
    public interface INavItemsRepository
    {
        List<NavItems> GetNavItems();
    }

    public class NavItemsRepository : INavItemsRepository
    {
        private readonly DbConnection _db;

        public NavItemsRepository(DbConnection dbConnection)
        {
            _db = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public List<NavItems> GetNavItems()
        {
            var navItemsList = new List<NavItems>();

            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetNavItems", conn)
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
                                var navItem = new NavItems
                                {
                                    NavItem = reader.GetString(0)                                    
                                };
                                navItemsList.Add(navItem);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error fetching NavItems", ex);
                    }
                }
            }

            return navItemsList;
        }
    }
}
