using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Marketplace.Core.Models;
using Marketplace.Core.DTOs; 

// Доступ к этому контроллеру только для пользователей с ролями "Admin" или "Manager"
[Authorize(Roles = "Admin,Owner")]
[Route("api/admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    // Пример: Отображение списка всех пользователей и их ролей
    [Authorize(Roles = "Admin")]
    [HttpGet("users")]
    public async Task<IActionResult> UserList()
    {
        try
        {
            var users = _userManager.Users.ToList();

            var userDtos = new List<UserDataDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userDtos.Add(new UserDataDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles
                });
            }

            var result = new CustomList
            {
                Count = userDtos.Count,
                Results = userDtos.Cast<object>().ToList()
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditUserRoles(string id, List<string> selectedRoles)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound($"Пользователь с ID {id} не найден.");
        }

        var currentRoles = await _userManager.GetRolesAsync(user); // Получаем текущие роли

        // Удаляем роли, которые больше не выбраны
        var rolesToRemove = currentRoles.Except(selectedRoles).ToList();
        if (rolesToRemove.Any())
        {
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
        }

        // Добавляем новые выбранные роли
        var rolesToAdd = selectedRoles.Except(currentRoles).ToList();
        if (rolesToAdd.Any())
        {
            await _userManager.AddToRolesAsync(user, rolesToAdd);
        }

        //TempData["Message"] = "Роли пользователя обновлены успешно.";
        return RedirectToAction(nameof(Index));
    }
}