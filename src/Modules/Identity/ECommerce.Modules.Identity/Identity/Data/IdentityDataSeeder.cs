using BuildingBlocks.Abstractions.Persistence;
using ECommerce.Modules.Identity.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Modules.Identity.Identity.Data;

public class IdentityDataSeeder : IDataSeeder
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityDataSeeder(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAllAsync()
    {
        await SeedRoles();
        await SeedUsers();
    }

    private async Task SeedRoles()
    {
        if (!await _roleManager.RoleExistsAsync(ApplicationRole.Admin.Name))
            await _roleManager.CreateAsync(ApplicationRole.Admin);

        if (!await _roleManager.RoleExistsAsync(ApplicationRole.User.Name))
            await _roleManager.CreateAsync(ApplicationRole.User);
    }

    private async Task SeedUsers()
    {
        if (await _userManager.FindByEmailAsync("divit@test.com") == null)
        {
            var user = new ApplicationUser
            {
                UserName = "divit",
                FirstName = "Divit",
                LastName = "Test",
                Email = "divit@test.com",
            };

            var result = await _userManager.CreateAsync(user, "123456");

            if (result.Succeeded) await _userManager.AddToRoleAsync(user, ApplicationRole.Admin.Name);
        }

        if (await _userManager.FindByEmailAsync("user@test.com") == null)
        {
            var user = new ApplicationUser
            {
                UserName = "user", FirstName = "User", LastName = "Test", Email = "user@test.com"
            };

            var result = await _userManager.CreateAsync(user, "123456");

            if (result.Succeeded) await _userManager.AddToRoleAsync(user, ApplicationRole.User.Name);
        }
    }
}
