using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Handlers
{
    public interface IContactUsHandler
    {
        Task<List<ContactUs>> GetContacts();
    }
    public class ContactUsHandler : IContactUsHandler
    {
        private readonly IRestaurantRepository _contactUsRepository;
        public ContactUsHandler(IRestaurantRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        public async Task<List<ContactUs>> GetContacts()
        {
            return await _contactUsRepository.GetContacts();
        }

    }
}
