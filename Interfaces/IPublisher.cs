using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.Publisher;

namespace VideoGameApi.Interfaces
{
    public interface IPublisher
    {
        Task<List<Publisher>> GetAllPublishers();
        Task<Publisher?> GetPublisherById(string publisherId);
        Task<List<MdGetPublisherGames>> GetAllPubGames();
        Task<MdGetPublisherGames> GetPubGames(string pubId);
        Task<MdResponse> CreatePublisher(Publisher publisher);
        Task<MdResponse> UpdatePublisher(string publisherId, string Name);
        Task<MdResponse> DeletePublisher(string publisherId);



    }
}
