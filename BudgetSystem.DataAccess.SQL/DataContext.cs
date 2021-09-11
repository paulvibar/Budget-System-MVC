using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public DbSet<ResponsibilityCenter> ResponsibilityCenters { get; set; }
        public DbSet<MFOPAP> MFOPAPs { get; set; }
        public DbSet<Identifier> Identifiers { get; set; }
        public DbSet<UACS> UACS { get; set; }
        public DbSet<UACSGroup> UACSGroup { get; set; }
        public DbSet<UACSClass> UACSClass { get; set; }
        public DbSet<UACSClassification> UACSClassification { get; set; }
        public DbSet<UACSObject> UACSObject { get; set; }
        public DbSet<BoxBSignatory> BoxBSignatory { get; set; }
        public DbSet<FundCluster> FundCluster { get; set; }
        public DbSet<FundSource> FundSource { get; set; }
        public DbSet<ORSMainInformation> ORSMainInformation { get; set; }
        public DbSet<ORSDetailsInformation> ORSDetailsInformation { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
