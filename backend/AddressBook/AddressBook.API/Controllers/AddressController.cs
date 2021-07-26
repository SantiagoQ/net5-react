using AddressBook.Core.Addresses.Managers;
using AddressBook.Core.Addresses.Managers.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.API.Controllers
{
    [ApiController]
    [Route("address")]
    public class AddressController: ControllerBase
    {
        private readonly IAddressManager _addressManager;
        public AddressController(IAddressManager addressManager)
        {
            _addressManager = addressManager;
        }
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            return new JsonResult(await _addressManager.GetAddresses());
        }
    }
}
