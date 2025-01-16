using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;

namespace RestaurantDemo.Repositories
{

    public interface IFeedbackRepository
    {
        string AddFeedback(Feedback feedback);
        List<Feedback> GetFeedbacks();
        string DeleteFeedback(int id);
    }
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DbConnection _db;
        public FeedbackRepository(DbConnection connection) {
            _db = connection;
        }

        public string AddFeedback(Feedback feedback)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("AddFeedback", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@FeedbackMessage", feedback.FeedbackMessage ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Name", feedback.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ratings", feedback.Ratings);

                    try
                    {
                        var res = cmd.ExecuteScalar();
                        return "Feedback submitted!!";

                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error adding new feedback ", ex);
                    }
                }

            }
        }


        public List<Feedback> GetFeedbacks()
        {
            List<Feedback> feedbacksList = new List<Feedback>();
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetFeedback", conn)
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
                                var feedback = new Feedback()
                                {
                                    
                                    FeedbackMessage = read.GetString(read.GetOrdinal("FeedbackMessage")),
                                    Name = read.GetString(read.GetOrdinal("Name")),
                                    Ratings = read.GetInt32(read.GetOrdinal("Ratings"))
                                    
                                };
                                feedbacksList.Add(feedback);
                            }
                        }
                    }
                    catch (Exception ex) { throw new ApplicationException("Error fetching feedback details", ex); }
                }
            }
            return feedbacksList;
        }


        public string DeleteFeedback(int id)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("DeleteFeedback", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        var res = cmd.ExecuteScalar();
                        return "Feedback deleted successfully";
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error deleting feedback", ex);
                    }
                }
            }
        }

    }
}
