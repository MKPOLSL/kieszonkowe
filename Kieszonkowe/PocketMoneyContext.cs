using Kieszonkowe.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kieszonkowe
{
    public class PocketMoneyContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }
        public DbSet<ChildRecord> ChildRecords { get; set; }
        public DbSet<Education> EducationDegrees { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=pocketMoney.db");
    }
}
