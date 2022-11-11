using BE_TEST.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_TEST.Infrastructure.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Key
            builder.HasKey(u => u.Id);

            // Property
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name)
                .HasMaxLength(30);

            builder.Property(x => x.Email)
                .HasMaxLength(50);

            builder.Property(x => x.Phone)
                .HasMaxLength(20);

            builder.Property(x => x.Address)
                .HasMaxLength(255);

            // Data
            builder.HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Thomas Hardy",
                    Email = "thomashardy@mail.com",
                    Address = "89 Chiaroscuro Rd, Portland, USA",
                    Phone = "(171) 555-2222"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Dominique perrier",
                    Email = "dominiqueperrier@mail.com",
                    Address = "Obere Str. 57, Berlin, Germany",
                    Phone = "(313) 555-5735"
                },
                new Employee
                {
                    Id = 3,
                    Name = "Maria Anders",
                    Email = "mariaanders@mail.com",
                    Address = "25, rue Lauriston, Paris, France",
                    Phone = "(503) 555-9931"
                },
                new Employee
                {
                    Id = 4,
                    Name = "Fran Wilson",
                    Email = "franwilson@mail.com",
                    Address = "C/ Araquil, 67, Madrid, Spain",
                    Phone = "(204) 619-5731"
                },
                new Employee
                {
                    Id = 5,
                    Name = "Martin Blank",
                    Email = "martinblank@gmail.com",
                    Address = "Via Monte Bianco 34, Turin, Italy",
                    Phone = "(480) 631-2097"
                }
            );
        }
    }
}
