using Kieszonkowe.Entities;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IUserService
    {
        Task<Parent> CreateUser(Parent parent);
    }
}
