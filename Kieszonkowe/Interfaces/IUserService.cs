using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IUserService
    {
        Task<Parent> CreateUser(Parent parent);
        public User AuthenticateUser(UserLoginDto userLogin);
        public Task<Parent> UpdateParentData(ParentChangeDataDto parentData);
        public Task<Parent> UpdateParentPassword(ParentChangePasswordDto parentPassword);
    }
}
