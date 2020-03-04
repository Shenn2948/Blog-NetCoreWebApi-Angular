using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.DataAccess.DbContext;
using Blog.Services.Exceptions;
using MongoDB.Driver;
using DbUser = Blog.DataAccess.Entities.Identity.ApplicationUser;

namespace Blog.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMongoDbContext _dbContext;

        private readonly IMapper _mapper;

        public UserService(IMongoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<User> GetUserAsync(string id)
        {
            var dbUser = await _dbContext.Users.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (dbUser == null)
            {
                throw new RequestedResourceNotFoundException($"user with{id} wasn't found.");
            }

            return _mapper.Map<User>(dbUser);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var dbUsers = await _dbContext.Users.Find(_ => true).ToListAsync();
            var users = dbUsers.Select(_mapper.Map<User>).ToList();

            return users;
        }

        public async Task<User> AddUserAsync(UpdateUserRequest userIn)
        {
            var dbUsers = await _dbContext.Users.FindAsync(user => user.UserName == userIn.UserName);

            if (dbUsers.Any())
            {
                throw new RequestedResourceHasConflictException();
            }

            DbUser dbUser = _mapper.Map<UpdateUserRequest, DbUser>(userIn);

            await _dbContext.Users.InsertOneAsync(dbUser);

            return _mapper.Map<User>(dbUser);
        }

        public async Task<User> UpdateUserAsync(string id, UpdateUserRequest userIn)
        {
            var filter = Builders<DbUser>.Filter.Eq(s => s.Id, id);
            var update = Builders<DbUser>.Update.Set(s => s.UserName, userIn.UserName).Set(s => s.Email, userIn.Email);

            DbUser dbUser = await _dbContext.Users.FindOneAndUpdateAsync(filter, update);

            return _mapper.Map<User>(dbUser);
        }

        public async Task DeleteUserAsync(string id)
        {
            var dbUser = await _dbContext.Users.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (dbUser == null)
            {
                throw new RequestedResourceNotFoundException($"user with{id} wasn't found.");
            }

            await _dbContext.Users.DeleteOneAsync(Builders<DbUser>.Filter.Eq(x => x.Id, id));
        }
    }
}