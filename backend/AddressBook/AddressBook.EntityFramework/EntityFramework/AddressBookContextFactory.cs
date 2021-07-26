using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.EntityFramework.EntityFramework
{    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class AddressBookContextFactory : IDesignTimeDbContextFactory<AddressBookContext>
    {
        public AddressBookContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AddressBookContext>();
            builder.UseSqlServer("Server=localhost;Database=AddressBook;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new AddressBookContext(builder.Options);
        }
    }
}
