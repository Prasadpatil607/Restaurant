using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;

namespace RestaurantDemo.Repositories
{
    public interface IServiceHourRepository
    {
        Task<List<ServiceHours>> GetServiceHours();
    }
    public class ServiceHoursRepository : IServiceHourRepository
    {
        private readonly DbConnection _db;
        public ServiceHoursRepository(DbConnection db)
        {
            _db = db;
        }

        public async Task<List<ServiceHours>> GetServiceHours()
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    var serviceHourList= await conn.QueryAsync<ServiceHours>("GetServiceHours", new { }, commandType: CommandType.StoredProcedure);
                    return serviceHourList.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching Service Hour details", ex);
            }
        }

    }
}
