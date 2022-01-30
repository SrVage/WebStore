using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers.Identity
{
    [Route(WebAPIAddresses.Identity.Users)]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDB> _userStore;

        public UsersApiController(WebStoreDB dataBase)
        {
            _userStore = new UserStore<User, Role, WebStoreDB>(dataBase);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAll() => await _userStore.Users.ToArrayAsync();
    }
}
