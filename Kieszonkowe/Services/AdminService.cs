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
        private readonly DbSet<Parent> parentSet;
        private readonly DbSet<Region> regionSet;
        private readonly DbSet<ChildRecord> childSet;
        private readonly DbSet<Education> educationSet;

        public AdminService(PocketMoneyContext pocketMoneyContext)
        {
            this.pocketMoneyContext = pocketMoneyContext;
            this.adminSet = pocketMoneyContext.Set<Administrator>();
            this.childSet = pocketMoneyContext.Set<ChildRecord>();
            this.parentSet = pocketMoneyContext.Set<Parent>();
            this.educationSet = pocketMoneyContext.Set<Education>();
            this.regionSet = pocketMoneyContext.Set<Region>();
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
                Region = region,
                DateAdded = DateTime.Now
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

        public async Task<Parent> AddParent(Parent parent)
        {
            pocketMoneyContext.Parents.Add(parent);
            await pocketMoneyContext.SaveChangesAsync();
            return parent;
        }

        public async Task<Region> AddRegion(Region region)
        {
            pocketMoneyContext.Regions.Add(region);
            await pocketMoneyContext.SaveChangesAsync();
            return region;
        }

        public Administrator AuthenticateAdmin(UserLoginDto admin)
        {
            var authenticatedAdmin = adminSet.Where(a => a.Username == admin.Username && a.Password == admin.Password && a.IsHidden == false);
            return authenticatedAdmin.FirstOrDefault();
        }

        public async Task<bool> DeleteAdministrator(Guid adminId)
        {
            var admin = adminSet.Where(a => a.Id == adminId).FirstOrDefault();
            if (admin == null)
                return false;
            admin.IsHidden = true;
            await pocketMoneyContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteChildRecord(Guid childId)
        {
            var child = childSet.Where(a => a.Id == childId).FirstOrDefault();
            if (child == null)
                return false;
            child.IsHidden = true;
            await pocketMoneyContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEducation(Guid educationId)
        {
            var education = adminSet.Where(a => a.Id == educationId).FirstOrDefault();
            if (education == null)
                return false;
            education.IsHidden = true;
            await pocketMoneyContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteParent(Guid parentId)
        {
            var parent = adminSet.Where(a => a.Id == parentId).FirstOrDefault();
            if (parent == null)
                return false;
            parent.IsHidden = true;
            await pocketMoneyContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRegion(Guid regionId)
        {
            var region = adminSet.Where(a => a.Id == regionId).FirstOrDefault();
            if (region == null)
                return false;
            region.IsHidden = true;
            await pocketMoneyContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Administrator>> GetAdministrators()
        {
            return await pocketMoneyContext.Administrators.Where(c => c.IsHidden == false).ToListAsync();
        }

        public async Task<List<ChildRecord>> GetChildRecords()
        {
            return await pocketMoneyContext.ChildRecords
                .Include(c => c.Region)
                .Include(c => c.Education)
                .Where(c => c.IsHidden == false)
                .ToListAsync();
        }

        public async Task<List<Education>> GetEducations()
        {
            return await pocketMoneyContext.EducationDegrees.Where(c => c.IsHidden == false).ToListAsync();
        }

        public async Task<List<Parent>> GetParents()
        {
            return await pocketMoneyContext.Parents.Where(c => c.IsHidden == false).ToListAsync();
        }

        public async Task<List<Region>> GetRegions()
        {
            return await pocketMoneyContext.Regions.Where(c => c.IsHidden == false).ToListAsync();
        }

        public async Task<Administrator> UpdateAdministrator(Administrator admin)
        {
            throw new NotImplementedException();
        }

        public async Task<ChildRecord> UpdateChildRecord(ChildDto child)
        {
            throw new NotImplementedException();
        }

        public async Task<Education> UpdateEducation(Education education)
        {
            throw new NotImplementedException();
        }

        public async Task<Parent> UpdateParent(Parent parent)
        {
            throw new NotImplementedException();
        }

        public async Task<Region> UpdateRegion(Region region)
        {
            throw new NotImplementedException();
        }
    }
}
