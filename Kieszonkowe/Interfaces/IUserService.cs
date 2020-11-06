using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IUserService
    {
        Task<Parent> CreateUser(Parent parent);
        public User AuthenticateUser(UserLoginDto userLogin);
        public List<ChildRecord> GetAllChildrenForUser(Guid id);
        public void AddChildRecord(ChildRecord childRecord, Guid parentId);
    }
}
