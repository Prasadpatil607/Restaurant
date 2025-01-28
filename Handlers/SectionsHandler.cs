using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Handlers
{
    public interface ISectionsHandler
    {
        Task<List<Sections>> GetSections();
    }
    public class SectionsHandler : ISectionsHandler
    {
        private readonly IRestaurantRepository _sectionsRepository;
        public SectionsHandler(IRestaurantRepository sectionsRepository)
        {
            _sectionsRepository = sectionsRepository;
        }

        public async Task<List<Sections>> GetSections()
        {
            return await _sectionsRepository.GetSections();
        }
    }
}
