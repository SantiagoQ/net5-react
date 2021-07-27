using AddressBook.API.Utils.Automapper;
using AddressBook.Core.Addresses.Entities;
using AddressBook.Core.Addresses.Managers;
using AddressBook.Core.Addresses.Managers.Dtos;
using AddressBook.Core.Auxiliaries.Repositories;
using AddressBook.Test.Core.Auxiliaries;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace AddressBook.Test.Core
{
    public class AddressUnitTests
    {
        //DI mock
        private readonly IRepository<Address> _addressRepository;
        private readonly AddressManager _addressManager;
        private readonly IMapper _mapper;

        //Data mock
        private List<Address> addressesDbMock = new List<Address>() {
            new Address("Krishan", "Lovell", "Techyx", "amir.mila430@nproxi.com", 7345873903) { Id = 1 },
            new Address("Amal", "Simmonds", "SavvyTech", "9khalil.bnamorg@gmailya.com", 9792189280) { Id = 2 },
        };
        public AddressUnitTests()
        {
            //Mocking
            var addressRepositoryMock = new Mock<IRepository<Address>>();
            addressRepositoryMock.Setup(mock => mock.GetAllAsync().Result).Returns(addressesDbMock);
            addressRepositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<int>()).Result).Returns<int, object>(
                (id, par) =>  {
                return addressesDbMock.FirstOrDefault(a => a.Id == id); 
            });
            addressRepositoryMock.Setup(mock => mock.AddAsync(It.IsAny<Address>()).Result).Callback<Address>(
                (ad) => { ad.Id = addressesDbMock.Count + 1; addressesDbMock.Add(ad);
            });
            addressRepositoryMock.Setup(mock => mock.UpdateAsync(It.IsAny<Address>()).Result).Callback<Address>(
                (ad) => {
                    var index = addressesDbMock.FindIndex(a => a.Id == ad.Id);
                    addressesDbMock[index].Update(ad.FirstName, ad.LastName, ad.CompanyName, ad.Email, ad.Phone);
                });
            _addressRepository = addressRepositoryMock.Object;

            //Services
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AddressProfile>()));
            _addressManager = new AddressManager(_addressRepository, _mapper);
        }
        [Fact]
        public async Task GetAddresses_ReturnsAllAddresses()
        {
            var records = await _addressManager.GetAddresses();
            var mockDtos = _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDto>>(addressesDbMock)
                .OrderBy(m => m.FirstName);

            Assert.Equal(mockDtos.Count(), records.Count());
            records.Should().BeEquivalentTo(mockDtos);
            records.Should().BeInAscendingOrder(x => x.FirstName);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAddress_ReturnSelected(int id)
        {
            var record = await _addressManager.GetAddress(id);
            var mock = addressesDbMock.FirstOrDefault(a => a.Id == id);
            var mockDto = _mapper.Map<Address, AddressDto>(mock);
            record.Should().BeEquivalentTo(mockDto);
        }
        [Fact]
        public async Task GetAddressInvalidId_ThrowException()
        {
            await Assert.ThrowsAsync<NullReferenceException>(() => _addressManager.GetAddress(3));
        }
        [Theory]
        [InlineData("Krishan", "Lovell", "Techyx", "amir.mila430@nproxi.com", 7345873903)]
        [InlineData("Amal", "Simmonds", "SavvyTech", "9khalil.bnamorg@gmailya.com", 9792189280)]
        public async Task CreateAddress_SaveAddress(string firstName, string lastName,
            string company, string email, long phone)
        {
            CreateAddressDto dto = new CreateAddressDto(firstName, lastName, company, email, phone);
            await _addressManager.CreateAddress(dto);
            var records = await _addressManager.GetAddresses();
            var lastAdded = records.OrderBy(a=> a.Id).Last();
            Assert.Equal(3, records.Count());
            Assert.Equal(3, lastAdded.Id);
            Assert.Equal(firstName, lastAdded.FirstName);
            Assert.Equal(lastName, lastAdded.LastName);
            Assert.Equal(company, lastAdded.CompanyName);
            Assert.Equal(email, lastAdded.Email);
            Assert.Equal(phone, lastAdded.Phone);
        }
        [Theory]
        [InlineData(1, "KrishanUpdated", "LovellUpdated", "TechyxUpdated", "amir.mila430Updated@nproxi.com", 7345873903)]
        public async Task UpdateAddres_SaveAddress(int id, string firstName, string lastName,
            string company, string email, long phone)
        {
            UpdateAddressDto dto = new UpdateAddressDto(id, firstName, lastName, company, email, phone);
            await _addressManager.UpdateAddress(dto);
            var record = await _addressManager.GetAddress(id);
            Assert.Equal(firstName, record.FirstName);
            Assert.Equal(lastName, record.LastName);
            Assert.Equal(company, record.CompanyName);
            Assert.Equal(email, record.Email);
            Assert.Equal(phone, record.Phone);
        }
        [Theory]
        [InlineData("Krishan123#!", "Lovell", "Techyx", "amir.mila430@nproxi.com", 7345873903)]
        [InlineData("Krishan", "Lovell*&^", "Techyx", "amir.mila430@nproxi.com", 7345873903)]
        [InlineData("Krishan", "Lovell", "Techyx............................................................", "amir.mila430@nproxi.com", 7345873903)]
        [InlineData("Krishan", "Lovell", "Techyx", "amir.mila430@@-$@nproxi.com", 7345873903)]
        [InlineData("Krishan", "Lovell", "Techyx", "amir.mila430@nproxi.com", 7345873903999956759)]
        public void ValidateIncorrectArgs_ThrowException(string firstName, string lastName,
            string company, string email, long phone)
        {
            Assert.Throws<Exception>(() => _addressManager.ValidateFields(firstName, lastName, company, email, phone));
        }
    }
}
