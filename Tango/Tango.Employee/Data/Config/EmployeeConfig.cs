using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tango.Employee.Entities;

namespace Tango.Employee.Data.Config
{
    public class EmployeeConfig : IEntityTypeConfiguration<EmployeeEntityModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntityModel> builder)
        {
            builder.HasKey(rec => rec.EmployeeID);
            
            builder.Property(rec => rec.EmployeeID).UseIdentityColumn();
            builder.Property(rec => rec.EmployeeName).IsRequired().HasMaxLength(100);
            builder.Property(rec => rec.Department).HasColumnName("DepartmentName").IsRequired().HasMaxLength(100);
            builder.Property(rec => rec.CityCode).IsRequired().HasMaxLength(3);

            builder.HasData(
                new List<EmployeeEntityModel>
                {
                    new EmployeeEntityModel
                    {
                        EmployeeID = 1,
                        EmployeeName = "Alex",
                        Department = "IT",
                        CityCode = "CHN"
                    },
                    new EmployeeEntityModel
                    {
                        EmployeeID = 2,
                        EmployeeName = "Bob",
                        Department = "HR",
                        CityCode = "BLG"
                    }
                }
            );

        }
    }
}
