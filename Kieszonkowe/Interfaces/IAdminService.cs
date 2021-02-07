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
        Task<Region> AddRegion();
        Task<Education> AddEducation();
        Task<Administrator> AddAdministrator();
        Task<Parent> AddParent();
    }
}
