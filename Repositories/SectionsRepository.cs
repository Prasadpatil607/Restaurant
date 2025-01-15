using System.Data;
using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;
using static System.Collections.Specialized.BitVector32;

namespace RestaurantDemo.Repositories
{
    public interface ISectionRepository
    {
        List<Sections> GetSections();
        int AddSection(Sections sections);

        void UpdateSection(Sections section);
    }
    public class SectionsRepository : ISectionRepository
    {
        private readonly DbConnection _db;
        public SectionsRepository(DbConnection dbConnection) {
            _db = dbConnection;
        }

        public List<Sections> GetSections()
        {
            var sections = new List<Sections>();
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetSections", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    try
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var sec = new Sections()
                                {
                                    Id = reader.GetInt32(0),
                                    SectionName = reader.GetString(1),
                                    SectionHeading = reader.GetString(2),
                                    SectionDescription = reader.IsDBNull(3) ? null : reader.GetString(3),
                                };
                                sections.Add(sec);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error fetching Sections", ex);
                    }
                }
            }

            return sections;
        }

        public int AddSection(Sections section)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("AddSection", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@SectionName", section.SectionName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SectionHeading", section.SectionHeading ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SectionDescription", section.SectionDescription ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedBy", 1);  

                    try
                    {
                        var result = cmd.ExecuteScalar(); 
                        return Convert.ToInt32(result);
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error adding new section", ex);
                    }
                }
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
