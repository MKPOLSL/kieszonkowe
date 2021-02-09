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
            int? actualAmount = child.ActualAmount;
            if (child.ActualAmount == -1)
                actualAmount = null;
            var addedChild = new ChildRecord()
            {
                Name = child.Name,
                ParentId = child.ParentId,
                PlannedAmount = child.PlannedAmount,
                ActualAmount = actualAmount,
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

        public async Task<Parent> BanOrUnbanUser(Guid parentId)
        {
            var user = parentSet.Where(p => p.Id == parentId).FirstOrDefault();
            if (user != null)
                user.IsBanned = !user.IsBanned;
            await pocketMoneyContext.SaveChangesAsync();
            return user;
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
            var education = educationSet.Where(a => a.Id == educationId).FirstOrDefault();
            if (education == null)
                return false;
            education.IsHidden = true;
            await pocketMoneyContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteParent(Guid parentId)
        {
            var parent = parentSet.Where(a => a.Id == parentId).FirstOrDefault();
            if (parent == null)
                return false;
            parent.IsHidden = true;
            await pocketMoneyContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRegion(Guid regionId)
        {
            var region = regionSet.Where(a => a.Id == regionId).FirstOrDefault();
            if (region == null)
                return false;
            region.IsHidden = true;
            await pocketMoneyContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Administrator>> GetAdministrators(Guid adminId)
        {
            return await pocketMoneyContext.Administrators.Where(c => c.IsHidden == false && c.Id != adminId).ToListAsync();
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
            var updatedAdmin = adminSet.Where(a => a.Id == admin.Id).FirstOrDefault();
            if(updatedAdmin != null)
            {
                updatedAdmin.Username = admin.Username;
                updatedAdmin.Password = admin.Password;
            }
            await pocketMoneyContext.SaveChangesAsync();
            return updatedAdmin;
        }

        public async Task<ChildRecord> UpdateChildRecord(ChildDto child)
        {
            var region = regionSet.Where(r => r.RegionName == child.Region).FirstOrDefault();
            var education = educationSet.Where(e => e.EducationDegree == child.Education).FirstOrDefault();
            if (region == null || education == null)
                return null;
            var updatedChildRecord = childSet.Where(a => a.Id == child.Id).FirstOrDefault();
            if(updatedChildRecord != null)
            {
                updatedChildRecord.ActualAmount = child.ActualAmount;
                updatedChildRecord.PlannedAmount = child.PlannedAmount;
                updatedChildRecord.ParentId = child.ParentId;
                updatedChildRecord.Region = region;
                updatedChildRecord.Education = education;
                updatedChildRecord.DateAdded = DateTime.Now;
            }
            await pocketMoneyContext.SaveChangesAsync();
            return updatedChildRecord;
        }

        public async Task<Education> UpdateEducation(Education education)
        {
            var updatedEducation = educationSet.Where(a => a.Id == education.Id).FirstOrDefault();
            if(updatedEducation != null)
            {
                updatedEducation.EducationDegree = education.EducationDegree;
            }
            await pocketMoneyContext.SaveChangesAsync();
            return updatedEducation; 
        }

        public async Task<Parent> UpdateParent(Parent parent)
        {
            var updatedParent = parentSet.Where(a => a.Id == parent.Id).FirstOrDefault();
            if(updatedParent != null)
            {
                updatedParent.BirthDate = parent.BirthDate;
                updatedParent.Email = parent.Email;
                updatedParent.IsActive = parent.IsActive;
                updatedParent.Password = parent.Password;
                updatedParent.Username = parent.Username;
            }
            await pocketMoneyContext.SaveChangesAsync();
            return updatedParent;
        }

        public async Task<Region> UpdateRegion(Region region)
        {
            var updatedRegion = regionSet.Where(a => a.Id == region.Id).FirstOrDefault();
            if(updatedRegion != null)
            {
                updatedRegion.RegionName = region.RegionName;
                updatedRegion.IsCity = region.IsCity;
            }
            await pocketMoneyContext.SaveChangesAsync();
            return updatedRegion;
        }
    }
}
