using System;
using System.Collections.Generic;
using System.Text.Json;
using UsersApi.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserManager _userManager;
        private IMemoryCache _memoryCache;

        public UserController( IUserManager userManager, IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _memoryCache = memoryCache;
         
        }

        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            return _userManager.GetAll();
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserModel newUser)
        {
            var result = _userManager.Add(newUser);

            var resultString = JsonSerializer.Serialize(result.ToString());

            return Ok(resultString);
        }
    }
}
