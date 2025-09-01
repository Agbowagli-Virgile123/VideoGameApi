using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.User;

namespace VideoGameApi.Interfaces
{
    public interface IUser
    {
        Task<(MdResponse ,User)> RegisterUser(MdUser request);
        Task<(MdResponse, string,string, User)> LogInUser(MdUser cred);
        Task<(string, string)> RefreshTokensAsync(MdRefreshTokenRequest request);
    }
}
