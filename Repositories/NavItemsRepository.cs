using Dapper;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;
using System.Data;

namespace RestaurantDemo.Repositories
{
    public interface INavItemsRepository
    {
        Task<List<NavItems>> GetNavItems();
    }

    public class NavItemsRepository : INavItemsRepository
    {
        private readonly DbConnection _db;

        public NavItemsRepository(DbConnection dbConnection)
        {
            _db = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<List<NavItems>> GetNavItems()
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    var navList = await conn.QueryAsync<NavItems>("GetNavItems", new { }, commandType: CommandType.StoredProcedure);
                    return navList.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching NavItems", ex);
            }
        }
    }
}
