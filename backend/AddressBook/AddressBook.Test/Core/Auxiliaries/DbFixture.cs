using AddressBook.EntityFramework.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Test.Core.Auxiliaries
{
    public class DbFixture : IDisposable
    {
        private readonly AddressBookContext _context;
        private readonly string dbConntection = "Server=localhost;Database=AddressBook;Trusted_Connection=True;MultipleActiveResultSets=true";
        public DbFixture()
        {
            _context = new AddressBookContext(
                new DbContextOptionsBuilder<AddressBookContext>().UseSqlServer(dbConntection).Options
            );
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
