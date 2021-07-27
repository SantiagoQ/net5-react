using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Core.Addresses.Managers.Dtos
{
    public class CreateAddressDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public CreateAddressDto(string firstName, string lastName,
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
