using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IUserService
    {
        Task<Parent> CreateUser(Parent parent);
        public User AuthenticateUser(UserLoginDto userLogin);
        public bool UpdateUserData(ParentChangeDataDto userData);
        public bool UpdateUserPassword(ParentChangePasswordDto userPassword);
    }
}
