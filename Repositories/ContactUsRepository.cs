using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using RestaurantDemo.DatabaseConnection;
using RestaurantDemo.Models;

namespace RestaurantDemo.Repositories
{
    public interface IContactUsRepository
    {
        Task<ContactUs> AddContact(ContactUs contactUs);
        Task<List<ContactUs>> GetContacts();
        //string UpdateContact(ContactUs contactUs);
        //string DeleteContact(int id);
    }
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly DbConnection _db;
        public ContactUsRepository(DbConnection dbConnection) {
            _db = dbConnection;
        }

        public async Task<ContactUs> AddContact(ContactUs contactUs)
        {
            try
            {
                using(var conn = _db.GetConnection())
                {
                    var Id = await conn.ExecuteScalarAsync<int>("AddContactUs", new
                    {
                        Name = contactUs.Name,
                        Email = contactUs.Email,
                        Subject = contactUs.Subject,
                        Message = contactUs.Message,
                    }, commandType: CommandType.StoredProcedure);
                        var contactList = new ContactUs
                        {
                            Id = Id,
                            Name = contactUs.Name,
                            Email = contactUs.Email,
                            Subject = contactUs.Subject,
                            Message = contactUs.Message,
                        };
                    return contactList;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error adding new contact item", ex);
            }
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
                throw new ApplicationException("Error fetching Menu", ex);
            }
        }


        //public string UpdateContact(ContactUs contactUs)
        //{
        //    using (var conn = _db.GetConnection())
        //    {
        //        conn.Open();
        //        using (var cmd = new SqlCommand("UpdateContactUs", conn)
        //        {
        //            CommandType = System.Data.CommandType.StoredProcedure
        //        })
        //        {
        //            cmd.Parameters.AddWithValue("@Id", contactUs.Id);
        //            cmd.Parameters.AddWithValue("@Name", contactUs.Name ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@Email", contactUs.Email ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@Subject", contactUs.Subject ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@Message", contactUs.Message ?? (object)DBNull.Value);

        //            try
        //            {
        //                var res = cmd.ExecuteScalar();
        //                return "Contact updated successfully";
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new ApplicationException("Error updating contact details", ex);
        //            }
        //        }
        //    }
        //}


        //public string DeleteContact(int contactId)
        //{
        //    using (var conn = _db.GetConnection())
        //    {
        //        conn.Open();
        //        using (var cmd = new SqlCommand("DeleteContactUs", conn)
        //        {
        //            CommandType = System.Data.CommandType.StoredProcedure
        //        })
        //        {
        //            cmd.Parameters.AddWithValue("@Id", contactId);

        //            try
        //            {
        //                var res = cmd.ExecuteScalar();
        //                return "Contact deleted successfully";
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new ApplicationException("Error deleting contact", ex);
        //            }
        //        }
        //    }
        //}



    }
}
