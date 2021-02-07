using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IAdminService
    {
        Administrator AuthenticateAdmin(UserLoginDto admin);

        Task<List<Region>> GetRegions();
        Task<List<ChildRecord>> GetChildRecords();
        Task<List<Education>> GetEducations();
        Task<List<Parent>> GetParents();
        Task<List<Administrator>> GetAdministrators();

        Task<ChildRecord> AddChildRecord(ChildDto child);
        Task<Region> AddRegion(Region region);
        Task<Education> AddEducation(Education education);
        Task<Administrator> AddAdministrator(Administrator admin);
        Task<Parent> AddParent(Parent parent);
    }
}
