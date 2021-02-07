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

        public async Task<Administrator> AddAdministrator(Administrator admin)
        {
            pocketMoneyContext.Administrators.Add(admin);
            await pocketMoneyContext.SaveChangesAsync();
            return admin;
        }

        public async Task<ChildRecord> AddChildRecord(ChildDto child)
        {
            var region = pocketMoneyContext.Regions
                .Where(x => x.RegionName == child.Region)
                .FirstOrDefault();
            var education = pocketMoneyContext.EducationDegrees
                .Where(x => x.EducationDegree == child.Education)
                .FirstOrDefault();
            if (education == null || region == null)
                return null;
            var addedChild = new ChildRecord()
            {
                Name = child.Name,
                ParentId = child.ParentId,
                PlannedAmount = child.PlannedAmount,
                ActualAmount = child.ActualAmount,
                Education = education,
                Region = region
            };
            pocketMoneyContext.ChildRecords.Add(addedChild);
            await pocketMoneyContext.SaveChangesAsync();
            return addedChild;
        }

        public async Task<Education> AddEducation(Education education)
        {
            pocketMoneyContext.EducationDegrees.Add(education);
            await pocketMoneyContext.SaveChangesAsync();
            return education;
        }

        public async Task<Parent> AddParent(ParentDto parent)
        {
            var isActive = false;
            if (parent.IsActive.Equals("true"))
                isActive = true;
            var addedParent = new Parent()
            {
                BirthDate = parent.Birthdate,
                Password = parent.Password,
                IsActive = isActive,
                Email = parent.Email,
                Username = parent.Username
            };
            pocketMoneyContext.Parents.Add(addedParent);
            await pocketMoneyContext.SaveChangesAsync();
            return addedParent;
        }

        public async Task<Region> AddRegion(RegionDto region)
        {
            var isCity = false;
            if (region.isCity.Equals("true"))
                isCity = true;
            var addedRegion = new Region()
            {
                RegionName = region.RegionName,
                IsCity = isCity
            };
            pocketMoneyContext.Regions.Add(addedRegion);
            await pocketMoneyContext.SaveChangesAsync();
            return addedRegion;
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
