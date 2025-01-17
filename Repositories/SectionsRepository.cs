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
        Task<Sections> AddSection(Sections sections);

        void UpdateSection(Sections section);
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
                throw new ApplicationException("Error fetching Menu", ex);
            }
        }

        public async Task<Sections> AddSection(Sections section)
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    var Id = await conn.ExecuteScalarAsync<int>("AddSection", new
                    {
                        SectionName = section.SectionName,
                        SectionHeading = section.SectionHeading,
                        SectionDescription = section.SectionDescription,
                    }, commandType: CommandType.StoredProcedure);
                    var sections = new Sections
                    {
                        Id = Id,
                        SectionName = section.SectionName,
                        SectionHeading = section.SectionHeading,
                        SectionDescription = section.SectionDescription
                    };
                    return sections;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error adding new Section details", ex);
            }
        }

        public void UpdateSection(Sections section)
        {
            
            var parameters = new[]
            {
            new SqlParameter("@ID", section.Id),
            new SqlParameter("@SectionName", section.SectionName),
            new SqlParameter("@SectionHeading", section.SectionHeading),
            new SqlParameter("@SectionDescription", section.SectionDescription),
            //new SqlParameter("@ModifiedBy", section.ModifiedBy)
            };
            try
            {
                using (var conn = _db.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("UpdateSection", conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure

                    })
                    {
                        cmd.Parameters.AddRange(parameters);
                        cmd.ExecuteNonQuery();
                    }
                        
                }

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the section.", ex);
            }
        }
    }
}
