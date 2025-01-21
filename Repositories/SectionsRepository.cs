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

        Task<Sections> UpdateSection(Sections section);
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

        public async Task<Sections> UpdateSection(Sections sections)
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    var parameters = new
                    {
                        sections.Id,
                        sections.SectionName,
                        sections.SectionHeading,
                        sections.SectionDescription
                    };

                    var result = await conn.ExecuteAsync("UpdateSections", parameters, commandType: CommandType.StoredProcedure);

                    if (result > 0)
                    {
                        return sections; 
                    }
                    else
                    {
                        throw new ApplicationException("No rows were updated.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating section", ex);
            }
        }
    }
}
