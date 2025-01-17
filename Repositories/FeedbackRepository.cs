using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;

namespace RestaurantDemo.Repositories
{

    public interface IFeedbackRepository
    {
        Task<Feedback> AddFeedback(Feedback feedback);
        Task<List<Feedback>> GetFeedbacks();
        string DeleteFeedback(int id);
    }
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DbConnection _db;
        public FeedbackRepository(DbConnection connection) {
            _db = connection;
        }

        public async Task<Feedback> AddFeedback(Feedback feedback)
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    var Id = await conn.ExecuteScalarAsync<int>("AddFeedback", new
                    {
                        FeedbackMessage = feedback.FeedbackMessage,
                        Name = feedback.Name,
                        Ratings = feedback.Ratings
                    }, commandType: CommandType.StoredProcedure);
                    var feedbacks = new Feedback
                    {
                        Id = Id,
                        FeedbackMessage = feedback.FeedbackMessage,
                        Name = feedback.Name,
                        Ratings = feedback.Ratings
                       
                    };
                    return feedbacks;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error adding new Feedback item", ex);
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
                throw new ApplicationException("Error fetching Menu", ex);
            }
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
