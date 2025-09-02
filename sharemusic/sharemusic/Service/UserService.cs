using Microsoft.EntityFrameworkCore;
using sharemusic.Db;
using sharemusic.Interface;
using sharemusic.Models;

namespace sharemusic.Service
{
    public class UserService : IUserService
    {
        private readonly MusicDbContext _context;
        public UserService(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> GetUserData()
        {
            var userData = await _context.Users.FirstOrDefaultAsync();
            if (userData == null)
            {
                throw new ArgumentException("User doesnt have any data in database.");
            }
            return userData;  
        }
    }
}
