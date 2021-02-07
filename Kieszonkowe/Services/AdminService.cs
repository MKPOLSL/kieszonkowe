using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using Kieszonkowe.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.Services
{
    public class AdminService : IAdminService
    {
        private readonly PocketMoneyContext pocketMoneyContext;
        private readonly DbSet<Administrator> adminSet;

        public AdminService(PocketMoneyContext pocketMoneyContext)
        {
            this.pocketMoneyContext = pocketMoneyContext;
            this.adminSet = pocketMoneyContext.Set<Administrator>();
        }

        public Task<Administrator> AddAdministrator()
        {
            throw new NotImplementedException();
        }

        public Task<ChildRecord> AddChildRecord(ChildDto child)
        {
            throw new NotImplementedException();
        }

        public Task<Education> AddEducation()
        {
            throw new NotImplementedException();
        }

        public Task<Parent> AddParent()
        {
            throw new NotImplementedException();
        }

        public Task<Region> AddRegion()
        {
            throw new NotImplementedException();
        }

        public Administrator AuthenticateAdmin(UserLoginDto admin)
        {
            var authenticatedAdmin = adminSet.Where(a => a.Username == admin.Username && a.Password == admin.Password);
            return authenticatedAdmin.FirstOrDefault();
        }

        public async Task<List<Administrator>> GetAdministrators()
        {
            return await pocketMoneyContext.Administrators.ToListAsync();
        }

        public async Task<List<ChildRecord>> GetChildRecords()
        {
            return await pocketMoneyContext.ChildRecords
                .Include(c => c.Region)
                .Include(c => c.Education)
                .ToListAsync();
        }

        public async Task<List<Education>> GetEducations()
        {
            return await pocketMoneyContext.EducationDegrees.ToListAsync();
        }

        public async Task<List<Parent>> GetParents()
        {
            return await pocketMoneyContext.Parents.ToListAsync();
        }

        public async Task<List<Region>> GetRegions()
        {
            return await pocketMoneyContext.Regions.ToListAsync();
        }
    }
}
