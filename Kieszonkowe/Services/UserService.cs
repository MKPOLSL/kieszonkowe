using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using Kieszonkowe.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.Services
{
    public class UserService : IUserService
    {
        private readonly PocketMoneyContext pocketMoneyContext;
        private readonly DbSet<Parent> parentSet;
        private readonly DbSet<Parent> userSet;

        public UserService(PocketMoneyContext pocketMoneyContext)
        {
            this.pocketMoneyContext = pocketMoneyContext;
            parentSet = pocketMoneyContext.Set<Parent>();

        }
        public async Task<Parent> CreateUser(Parent parent)
        {
            if (CheckIfLoginExists(parent.Username))
                return null;
            var createdParent = await parentSet.AddAsync(parent);
            return createdParent.Entity;

        }

        private bool CheckIfLoginExists(string login)
        {
            return parentSet.Any(x => x.Username == login);
        }

        public User AuthenticateUser(UserLoginDto userLogin)
        {
            var user = parentSet.Where(e => e.Username == userLogin.Username && e.Password == userLogin.Password);
            return user.FirstOrDefault();
        }
    }
}
