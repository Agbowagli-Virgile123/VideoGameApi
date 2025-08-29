using VideoGameApi.Models;
using VideoGameApi.Models.User;

namespace VideoGameApi.Interfaces
{
    public interface IUser
    {
        Task<(MdResponse ,User)> RegisterUser(MdUser request);
        Task<(MdResponse, string, User)> LogInUser(MdUser cred);
    }
}
