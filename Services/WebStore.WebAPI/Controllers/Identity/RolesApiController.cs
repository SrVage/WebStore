using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers.Identity
{
    [Route(WebAPIAddresses.Identity.Roles)]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly RoleStore<Role> _roleStore;

        public RolesApiController(WebStoreDB dataBase)
        {
            _roleStore = new RoleStore<Role>(dataBase);
        }
        [HttpGet("all")]
        public async Task<IEnumerable<Role>> GetAll() => await _roleStore.Roles.ToArrayAsync();

    }
}
