using Microsoft.AspNetCore.Mvc;
using Ztracena_Tlapka_Backend.Application.Common;
using Ztracena_Tlapka_Backend.Application.DTOs;
using Ztracena_Tlapka_Backend.Application.Interfaces;

namespace Ztracena_Tlapka_Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(ApiResponse.Success(new { message = "List of users", users = await userService.GetAllAsync() }, ResCodes.Users.Get));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await userService.GetByIdAsync(id)
            ?? throw new AppException<object>(404, new { message = "User is not exists" }, ResCodes.Users.NotFound);

        return Ok(ApiResponse.Success(new { message = "User has been found", user }, ResCodes.Users.Get));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        if (await userService.EmailExistsAsync(request.Email))
            throw new AppException<object>(409, new { message = "Email is already taken" }, ResCodes.Users.EmailTaken);

        if (await userService.PhoneExistsAsync(request.Phone))
            throw new AppException<object>(409, new { message = "Phone number is already taken" }, ResCodes.Users.PhoneTaken);

        var created = await userService.CreateAsync(request);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, ApiResponse.Success(new { message = "User has been created", user = created }, ResCodes.Users.Added));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        var updated = await userService.UpdateAsync(id, request)
            ?? throw new AppException<object>(404, new { message = "User is not exists" }, ResCodes.Users.NotFound);

        return Ok(ApiResponse.Success(new { message = "User has been updated", user = updated }, ResCodes.Users.Updated));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await userService.DeleteAsync(id);
        
        if (!deleted) throw new AppException<object>(404, new { message = "User is not exists" }, ResCodes.Users.NotFound);

        return Ok(ApiResponse.Success(new { message = "User has been deleted", user = deleted }, ResCodes.Users.Deleted));
    }
}
