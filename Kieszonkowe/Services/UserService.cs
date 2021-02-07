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

        public bool UpdateUserData(ParentChangeDataDto userData)
        {
            var user = parentSet.Where(e => e.Id == userData.Id);
            if (user == null) 
                return false;
            user.FirstOrDefault().Email = userData.email;
            user.FirstOrDefault().Username = userData.username;
            user.FirstOrDefault().BirthDate = userData.birthDate;
            return true;
        }

        public bool UpdateUserPassword(ParentChangePasswordDto userPassword)
        {
            var user = parentSet.Where(e => e.Id == userPassword.Id);
            if (user == null)
                return false;
            user.FirstOrDefault().Password = userPassword.password;
            return true;
        }
    }
}
