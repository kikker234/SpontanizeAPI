using Data.Enums;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Spontanize;

[Route("/api/[controller]")]
public class AuthController(UserManager<User> userManager) : Controller
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
    {
        if (string.IsNullOrEmpty(request.OldPassword) || string.IsNullOrEmpty(request.NewPassword) || string.IsNullOrEmpty(request.RepeatNewPassword))
        {
            return BadRequest("All fields are required.");
        }

        if (request.NewPassword != request.RepeatNewPassword)
        {
            return BadRequest("The new passwords do not match.");
        }
        var user = await userManager.GetUserAsync(User);
        var result = await userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

        if (result.Succeeded)
        {
            return Ok("Password updated successfully.");
        }

        return BadRequest("Failed to update the password.");
    }
    
    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        // Gebruik Include om Organization in te laden
        var user = await userManager.Users
            .Include(u => u.Organization)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return NotFound("User not found");
        
        // Stel het object samen met benodigde gegevens
        var userInfo = new
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            UserType = user.UserType.ToString(),
            Organization = user.Organization == null ? null : new
            {
                Id = user.Organization.Id,
                Name = user.Organization.Name
            }
        };
        return Ok(userInfo);
    }

}

public class UpdatePasswordRequest
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string RepeatNewPassword { get; set; }
}