using AddressBook.Core.Addresses.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.EntityFramework.EntityFramework
{
    public class AddressBookContext: DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public AddressBookContext (DbContextOptions<AddressBookContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().ToTable("Address");
        }
    }
}
