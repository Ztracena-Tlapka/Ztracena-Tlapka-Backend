using Microsoft.AspNetCore.Mvc;
using Ztracena_Tlapka_Backend.Application.Common;
using Ztracena_Tlapka_Backend.Application.DTOs;
using Ztracena_Tlapka_Backend.Application.Interfaces;
using Ztracena_Tlapka_Backend.Api.Filters;

namespace Ztracena_Tlapka_Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [RequireAuth]
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        StatusCode(200, ApiResponse.Success(new { message = "List of users", users = await userService.GetAllAsync() }, ResCodes.Users.Get));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await userService.GetByIdAsync(id)
            ?? throw new AppException<object>(404, new { message = "User is not exists" }, ResCodes.Users.NotFound);

        return StatusCode(200, ApiResponse.Success(new { message = "User has been found", user }, ResCodes.Users.Get));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        var updated = await userService.UpdateAsync(id, request)
            ?? throw new AppException<object>(404, new { message = "User is not exists" }, ResCodes.Users.NotFound);

        return StatusCode(200, ApiResponse.Success(new { message = "User has been updated", user = updated }, ResCodes.Users.Updated));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await userService.DeleteAsync(id);
        
        if (!deleted) throw new AppException<object>(404, new { message = "User is not exists" }, ResCodes.Users.NotFound);

        return StatusCode(200, ApiResponse.Success(new { message = "User has been deleted", user = deleted }, ResCodes.Users.Deleted));
    }
}
