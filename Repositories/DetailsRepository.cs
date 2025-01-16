using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;

namespace RestaurantDemo.Repositories
{
    public interface IDetailsRepository
    {
        List<Details> GetDetails();
    }
    public class DetailsRepository : IDetailsRepository
    {
        private readonly DbConnection _db;
        public DetailsRepository(DbConnection db)
        {
            _db = db;
        }

        public List<Details> GetDetails()
        {
            List<Details> detailsList = new List<Details>();
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetDetails", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    try
                    {
                        using (var read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                var details = new Details()
                                {

                                    Address = read.GetString(read.GetOrdinal("Address")),
                                    PhoneNo = read.GetString(read.GetOrdinal("PhoneNo")),
                                    Email = read.GetString(read.GetOrdinal("Email")),
                                    TelephoneSupport = read.GetString(read.GetOrdinal("TelephoneSupport"))

                                };
                                detailsList.Add(details);
                            }
                        }
                    }
                    catch (Exception ex) { throw new ApplicationException("Error fetching feedback details", ex); }
                }
            }
            return detailsList;
        }
    }
}
