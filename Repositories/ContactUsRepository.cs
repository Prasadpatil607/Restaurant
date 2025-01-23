using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;

namespace RestaurantDemo.Repositories
{
    public interface IContactUsRepository
    {
        
        Task<List<ContactUs>> GetContacts();
    }
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly DbConnection _db;
        public ContactUsRepository(DbConnection dbConnection) {
            _db = dbConnection;
        }
        public async Task<List<ContactUs>> GetContacts()
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    var contactList = await conn.QueryAsync<ContactUs>("GetContactUs", new { }, commandType: CommandType.StoredProcedure);
                    return contactList.ToList();
                }
            }
            catch (Exception ex)
            {
                
                throw new ApplicationException("Error fetching Contacts", ex);
            }
        }




    }
}
