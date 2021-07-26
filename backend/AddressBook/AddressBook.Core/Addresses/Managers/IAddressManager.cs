using AddressBook.Core.Addresses.Managers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Core.Addresses.Managers
{
    public interface IAddressManager
    {
        Task CreateAddress(CreateAddressDto dto);
        Task UpdateAddress(UpdateAddressDto dto);
        Task<AddressDto> GetAddress(int id);
        Task<IEnumerable<AddressDto>> GetAddresses();
        Task DeleteAddress(int id);
    }
}
