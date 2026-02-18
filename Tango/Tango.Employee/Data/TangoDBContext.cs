using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tango.Employee.Data.Config;
using Tango.Employee.DTOs;
using Tango.Employee.Entities;

namespace Tango.Employee.Data
{
    public class TangoDBContext : DbContext
    {
        public TangoDBContext(DbContextOptions<TangoDBContext> options) : base(options)
        {
            
        }

        //table name
        public DbSet<EmployeeEntityModel>? Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfig());
        }

    }
}
