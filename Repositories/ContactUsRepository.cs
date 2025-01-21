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
        Task<ContactUs> UpdateContact(ContactUs contactUs);
        
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
                throw new ApplicationException("Error fetching Contacts", ex);
            }
        }


        public async Task<ContactUs> UpdateContact(ContactUs contact)
        {
            try
            {
                using (var conn = _db.GetConnection())
                {
                    var parameters = new
                    {
                        contact.Id,
                        contact.Name,
                        contact.Email,
                        contact.Subject,
                        contact.Message
                    };

                    var result = await conn.ExecuteAsync("UpdateContactUs", parameters, commandType: CommandType.StoredProcedure);

                    if (result > 0)
                    {
                        return contact;
                    }
                    else
                    {
                        throw new ApplicationException("No rows were updated.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating contacts", ex);
            }
        }



    }
}
