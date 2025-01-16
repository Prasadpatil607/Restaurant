using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;

namespace RestaurantDemo.Repositories
{
    public interface IContactUsRepository
    {
        string AddContact(ContactUs contactUs);
        List<ContactUs> GetContacts();
        string UpdateContact(ContactUs contactUs);
        string DeleteContact(int id);
    }
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly DbConnection _db;
        public ContactUsRepository(DbConnection dbConnection) {
            _db = dbConnection;
        }

        public string AddContact(ContactUs contactUs)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("AddContactUs", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@Name", contactUs.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", contactUs.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Subject", contactUs.Subject ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Message", contactUs.Message ?? (object)DBNull.Value);

                    try
                    {
                        var res = cmd.ExecuteScalar();
                            return "Contact added";
                        
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error adding new cotact details",ex);
                    }
                }
                
            }
        }

        public List<ContactUs> GetContacts()
        {
            List<ContactUs> contactUsList = new List<ContactUs>();
            using(var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetContactUs", conn)
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
                                var contact = new ContactUs()
                                {

                                    Name = read.GetString(read.GetOrdinal("Name")),
                                    Email = read.GetString(read.GetOrdinal("Email")),
                                    Subject = read.GetString(read.GetOrdinal("Subject")),
                                    Message = read.GetString(read.GetOrdinal("Message"))
                                };
                                contactUsList.Add(contact);
                            }
                        }
                    }
                    catch (Exception ex) { throw new ApplicationException("Error fetching Cotact details", ex); }
                }
            }
            return contactUsList;
        }


        public string UpdateContact(ContactUs contactUs)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("UpdateContactUs", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@Id", contactUs.Id);
                    cmd.Parameters.AddWithValue("@Name", contactUs.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", contactUs.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Subject", contactUs.Subject ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Message", contactUs.Message ?? (object)DBNull.Value);

                    try
                    {
                        var res = cmd.ExecuteScalar();
                        return "Contact updated successfully";
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error updating contact details", ex);
                    }
                }
            }
        }


        public string DeleteContact(int contactId)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("DeleteContactUs", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@Id", contactId);

                    try
                    {
                        var res = cmd.ExecuteScalar();
                        return "Contact deleted successfully";
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error deleting contact", ex);
                    }
                }
            }
        }



    }
}
