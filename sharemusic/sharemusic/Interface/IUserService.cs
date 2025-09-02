using sharemusic.Models;

namespace sharemusic.Interface
{
    public interface IUserService 
    {
        public Task<UserModel> GetUserData();
    }
}
