using Microsoft.AspNetCore.Mvc;
using EventDrivenDesign.Rest1.Dtos;
using EventDrivenDesign.Rest1.Interfaces;

namespace EventDrivenDesign.Rest1.Controllers
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
        public async Task<ActionResult<List<UserDto>>> GetUsers(
            CancellationToken cancellationToken) => Ok(await _userService.ListUsers(cancellationToken));

        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUsers(
                    [FromRoute] Guid Id, [FromBody] UserDto UserDto, CancellationToken cancellationToken) => Ok(
                        await _userService.UpdateUser(Id, UserDto, cancellationToken));

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUsers(
                  [FromBody] UserDto UserDto, CancellationToken cancellationToken) => Ok(
                      await _userService.CreateUser(UserDto, cancellationToken));

        [HttpGet("{Id}")]
        public async Task<ActionResult<UserDto>> GetUserById(
            [FromRoute] Guid Id, CancellationToken cancellationToken) => Ok(
                await _userService.GetUserById(Id, cancellationToken));

        [HttpDelete]
        public async Task<ActionResult<UserDto>> DeleteUsers(
            [FromRoute] Guid Id, CancellationToken cancellationToken) => Ok(
                await _userService.DeleteUser(Id, cancellationToken));

    }
}