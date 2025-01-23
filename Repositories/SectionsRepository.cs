using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;
using static System.Collections.Specialized.BitVector32;

namespace RestaurantDemo.Repositories
{
    public interface ISectionRepository
    {
        Task<List<Sections>> GetSections();
        
    }
    public class SectionsRepository : ISectionRepository
    {
        private readonly DbConnection _db;
        public SectionsRepository(DbConnection dbConnection) {
            _db = dbConnection;
        }

        public async Task<List<Sections>> GetSections()
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    var sectionList = await conn.QueryAsync<Sections>("GetSections", new { }, commandType: CommandType.StoredProcedure);
                    return sectionList.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching sections", ex);
            }
        }

    }
}
