using AddressBook.Core.Auxiliaries.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Core.Addresses.Entities
{
    public class Address: Entity, ICreationAudit, IModificationAudit, ISoftDelete
    {
        [MaxLengthAttribute(30)]
        public string FirstName { get; set; }
        [MaxLengthAttribute(30)]
        public string LastName { get; set; }
        [MaxLengthAttribute(60)]
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public bool IsDeleted { get; set; }

        public Address() { }
        public Address(string firstName, string lastName,
            string company, string email, long phone)
        {
            InternalUpdate(firstName, lastName, company, email, phone);
            CreationTime = DateTime.Now;
        }
        public void Update(string firstName, string lastName,
            string company, string email, long phone)
        {
            InternalUpdate(firstName, lastName, company, email, phone);
            LastModificationTime = DateTime.Now;
        }
        private void InternalUpdate(string firstName, string lastName,
            string company, string email, long phone)
        {
            FirstName = firstName;
            LastName = lastName;
            CompanyName = company;
            Email = email;
            Phone = phone;
        }
    }
}
