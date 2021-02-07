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
            await pocketMoneyContext.SaveChangesAsync();
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

        public async Task<Parent> UpdateParentData(ParentChangeDataDto parentData)
        {
            var user = parentSet.Where(e => e.Id == parentData.Id).FirstOrDefault();
            if (user == null) 
                return null;
            user.Email = parentData.Email;
            user.Username = parentData.Username;
            user.BirthDate = parentData.BirthDate;
            await this.pocketMoneyContext.SaveChangesAsync();
            return user;
        }

        public async Task<Parent> UpdateParentPassword(ParentChangePasswordDto parentPassword)
        {
            var user = parentSet.Where(e => e.Id == parentPassword.Id).FirstOrDefault();
            if (user == null)
                return null;
            user.Password = parentPassword.password;
            await this.pocketMoneyContext.SaveChangesAsync();
            return user;
        }
    }
}
