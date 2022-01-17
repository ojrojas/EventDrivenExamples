using EventDrivenDesign.Rest2.Dtos;
using EventDrivenDesign.Rest2.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventDrivenDesign.Rest2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _userService.ListUser(cancellationToken));
        }
    }


}