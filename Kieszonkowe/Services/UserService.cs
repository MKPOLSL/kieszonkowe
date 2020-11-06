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
    public class UserService : IUserService
    {
        private readonly PocketMoneyContext pocketMoneyContext;
        private readonly DbSet<Parent> parentSet;
        private readonly DbSet<Parent> userSet;
        private readonly DbSet<ChildRecord> childSet;

        public UserService(PocketMoneyContext pocketMoneyContext)
        {
            this.pocketMoneyContext = pocketMoneyContext;
            parentSet = pocketMoneyContext.Set<Parent>();
            childSet = pocketMoneyContext.Set<ChildRecord>();

        }
        public async Task<Parent> CreateUser(Parent parent)
        {
            if (CheckIfLoginExists(parent.Username))
                return null;
            var createdParent = await parentSet.AddAsync(parent);
            pocketMoneyContext.SaveChanges();
            return createdParent.Entity;

        }

        private bool CheckIfLoginExists(string login)
        {
            return parentSet.Any(x => x.Username == login);
        }

        public User AuthenticateUser(UserLoginDto userLogin)
        {
            var user = parentSet.Where(e => e.Username == userLogin.Username && e.Password == userLogin.Password);
            return user.FirstOrDefault();
        }

        public List<ChildRecord> GetAllChildrenForUser(Guid id)
        {
            id = new Guid("2d7ad57f-a263-4c4e-9796-233108fb2009");
                    var parent = parentSet
                .Where(c => c.Id == id)
                .Include(c => c.Children)
                .ThenInclude(e => e.Education)
                .Include(c => c.Children)
                .ThenInclude(e => e.Region)
                .SelectMany(u => u.Children);
            
            var children = parent.ToList();

            return children;
        }

        public void AddChildRecord(ChildRecord childRecord, Guid parentId)
        {
            var parent = parentSet.Where(c => c.Id == parentId).Single();
            parent.Children.Add(childRecord);
            parentSet.Add(parent);
            pocketMoneyContext.SaveChanges();
        }
    }
}
