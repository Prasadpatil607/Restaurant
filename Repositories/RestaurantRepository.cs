using Dapper;
using System.Data;
using RestaurantDemo.Infrastructure;
using RestaurantDemo.Models;

namespace RestaurantDemo.Repositories
{

    public interface IRestaurantRepository
    {
        Task<List<ContactUs>> GetContacts();
        Task<List<Details>> GetDetails();
        Task<List<NavItems>> GetNavItems();
        Task<List<ServiceHours>> GetServiceHours();
        Task<List<Feedback>> GetFeedbacks();
        Task<List<Sections>> GetSections();
        Task<List<Menu>> GetMenu();
    }

    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly DbConnection _db;

        public RestaurantRepository(DbConnection db)
        {
            _db = db;
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

        public async Task<List<Details>> GetDetails()
        {

            using (var conn = _db.GetConnection())
            {
                try
                {

                    var detailsList = await conn.QueryAsync<Details>("GetDetails", new { }, commandType: CommandType.StoredProcedure);
                    return detailsList.ToList();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error fetching feedback details", ex);
                }
            }
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

        public async Task<List<ServiceHours>> GetServiceHours()
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    var serviceHourList = await conn.QueryAsync<ServiceHours>("GetServiceHours", new { }, commandType: CommandType.StoredProcedure);
                    return serviceHourList.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching Service Hour details", ex);
            }
        }

        public async Task<List<Feedback>> GetFeedbacks()
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    var feedbackList = await conn.QueryAsync<Feedback>("GetFeedback", new { }, commandType: CommandType.StoredProcedure);
                    return feedbackList.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching feedback", ex);
            }
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

        public async Task<List<Menu>> GetMenu()
        {
            try
            {
                using (var conn = _db.GetConnection())
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
    }
}
