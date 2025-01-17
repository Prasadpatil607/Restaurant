using Dapper;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;
using System.Data;

namespace RestaurantDemo.Repositories
{
    public interface IDetailsRepository
    {
        public Task<List<Details>> GetDetails();
    }

    public class DetailsRepository : IDetailsRepository
    {
        private readonly DbConnection _db;

        public DetailsRepository(DbConnection db)
        {
            _db = db;
        }

        public async Task<List<Details>> GetDetails()
        {
            
            using (var conn = _db.GetConnection())
            {
                try
                {
                    // Dapper automatically maps the query results to the Details class
                   var  detailsList = await conn.QueryAsync<Details>("GetDetails", new { }, commandType: CommandType.StoredProcedure);
                    return detailsList.ToList();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error fetching feedback details", ex);
                }
            }
        }
    }
}
