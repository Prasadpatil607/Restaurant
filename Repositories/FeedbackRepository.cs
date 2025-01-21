using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;
using Microsoft.AspNetCore.Rewrite;

namespace RestaurantDemo.Repositories
{

    public interface IFeedbackRepository
    {
        Task<Feedback> AddFeedback(Feedback feedback);
        Task<List<Feedback>> GetFeedbacks();
        Task<bool> DeleteFeedback(int id);
        
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
                throw new ApplicationException("Error fetching feedback", ex);
            }
        }


        public async Task<bool> DeleteFeedback(int id)
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    var parameters = new { Id = id };

                    var result = await conn.ExecuteAsync("DeleteFeedback", parameters, commandType: CommandType.StoredProcedure);

                    return result > 0; 
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error deleting feedback", ex);
            }
        }

    }
}
