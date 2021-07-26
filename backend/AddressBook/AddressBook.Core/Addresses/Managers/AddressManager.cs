using AddressBook.Core.Addresses.Entities;
using AddressBook.Core.Addresses.Managers.Dtos;
using AddressBook.Core.Auxiliaries.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddressBook.Core.Addresses.Managers
{
    public class AddressManager : IAddressManager
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IMapper _mapper;
        public AddressManager(IRepository<Address> addressRepository,
            IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new address and executes related bussiness logic
        /// </summary>
        public async Task CreateAddress(CreateAddressDto dto)
        {
            ValidateFields(dto.FirstName, dto.LastName,
                dto.CompanyName, dto.Email, dto.Phone);
            var registry = new Address(dto.FirstName, dto.LastName, dto.CompanyName,
                dto.Email, dto.Phone);
            await _addressRepository.AddAsync(registry);

            //related business logic
            /*Examples:
             - Save the address to other services.
             - Other rules, creating connected objects for auditing, logging, etc
             - Sends event to EventBus so other tasks are initiated.
             */
        }

        /// <summary>
        /// Deletes address and executes related bussiness logic
        /// </summary>
        public async Task DeleteAddress(int id)
        {
            var registry = await _addressRepository.GetByIdAsync(id);
            if (registry == null)
            {
                throw new Exception("The address to delete doesn't exist.");
            }
            await _addressRepository.DeleteAsync(registry);
        }

        /// <summary>
        /// Gets address by Id
        /// </summary>
        public async Task<AddressDto> GetAddress(int id)
        {
            var registry = await _addressRepository.GetByIdAsync(id);
            if (registry == null)
            {
                throw new Exception("The requested address doesn't exist.");
            }
            //If the dto that the client sees needs data from other services. They should be called here
            var dto =  _mapper.Map<Address, AddressDto>(registry);
            return dto;
        }

        /// <summary>
        /// Gets all the addresses
        /// </summary>
        public async Task<IEnumerable<AddressDto>> GetAddresses()
        {
            var registries = await _addressRepository.GetAllAsync();
            //If the dto that the client sees needs data from other services. They should be called here
            var dto = _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDto>>(registries);
            return dto;
        }

        /// <summary>
        /// Updates address and executes related bussiness logic
        /// </summary>
        public async Task UpdateAddress(UpdateAddressDto dto)
        {
            ValidateFields(dto.FirstName, dto.LastName,
                dto.CompanyName, dto.Email, dto.Phone);

            var current = await _addressRepository.GetByIdAsync(dto.Id);

            if(current == null)
            {
                throw new Exception("The updated address doesn't exist.");
            }

            await _addressRepository.UpdateAsync(current);
        }

        /// <summary>
        /// Validates the fields of address
        /// </summary>
        private void ValidateFields(string firstName, string lastName,
            string company, string email, long phone)
        {
            /* Validates that only letters are in names. Could be changed if needed, there is no real reason 
             except having some kind of validation of regex*/
            if (!Regex.Match(firstName, "^[A-Z][a-zA-Z]*$").Success && firstName.Length > 30)
            {
                throw new Exception("The format of the first name isn't valid.");
            }
            if (!Regex.Match(lastName, "^[A-Z][a-zA-Z]*$").Success && lastName.Length > 30)
            {
                throw new Exception("The format of the last name isn't valid.");
            }
            //Validate company name max length 60
            if (company.Length > 60)
            {
                throw new Exception("The length of the company name isn't valid.");
            }
            //Validates that email has email format
            try
            {
                MailAddress mail = new MailAddress(email);
            }
            catch
            {
                throw new Exception("The format of the email address isn't valid.");
            }
            //Validates phone number max length 12/ min length 7
            if(phone < 1000000 || phone > 999999999)
            {
                throw new Exception("The format of the email address isn't valid.");
            }
        }
    }
}
