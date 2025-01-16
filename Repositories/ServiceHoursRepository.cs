using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;

namespace RestaurantDemo.Repositories
{
    public interface IServiceHourRepository
    {
        List<ServiceHours> GetServiceHours();
    }
    public class ServiceHoursRepository : IServiceHourRepository
    {
        private readonly DbConnection _db;
        public ServiceHoursRepository(DbConnection db)
        {
            _db = db;
        }

        public List<ServiceHours> GetServiceHours()
        {
            List<ServiceHours> serviceList = new List<ServiceHours>();
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetServiceHours", conn)
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
                                var service = new ServiceHours()
                                {

                                    MondayToSaturday = read.GetString(read.GetOrdinal("MondayToSaturday")),
                                    Sunday = read.GetString(read.GetOrdinal("Sunday")),
                                    
                                };
                                serviceList.Add(service);
                            }
                        }
                    }
                    catch (Exception ex) { throw new ApplicationException("Error fetching Service details", ex); }
                }
            }
            return serviceList;
        }

    }
}
