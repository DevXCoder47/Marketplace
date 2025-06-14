using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Marketplace.Core.Models;
using Marketplace.Core.DTOs; // Ваш ApplicationUser

// Доступ к этому контроллеру только для пользователей с ролями "Admin" или "Manager"
[Authorize(Roles = "Admin,Manager")]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // Пример: Отображение списка всех пользователей и их ролей
    /*public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList(); // Получаем всех пользователей
        var userRoles = new Dictionary<string, IList<string>>();

        // Для каждого пользователя получаем его роли
        foreach (var user in users)
        {
            userRoles[user.Id] = await _userManager.GetRolesAsync(user);
        }

        //ViewBag.UserRoles = userRoles; // Передаем роли в представление
        //return View(users);
    }*/

    // Пример: Метод для изменения ролей пользователя
    //[HttpGet]
    /*public async Task<IActionResult> EditUserRoles(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound($"Пользователь с ID {id} не найден.");
        }

        //ViewBag.AllRoles = _roleManager.Roles.ToList(); // Все доступные роли
        //ViewBag.UserCurrentRoles = await _userManager.GetRolesAsync(user); // Текущие роли пользователя

        //return View(user);
    }*/

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

    // Пример: Создание нового пользователя и назначение ему роли "Client"
    //[HttpPost]
    //[AllowAnonymous] // Разрешить доступ без аутентификации (например, для регистрации)
    /*public async Task<IActionResult> RegisterClient(UserSignUpDTO model) // Предполагаем, что RegisterViewModel содержит нужные поля
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email, // Email часто используется как UserName для входа
                Email = model.Email,
                Nickname = model.Nickname,
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                Status = ApplicationUser.OnlineStatus.Offline // Или другое начальное значение
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Назначаем роль "Client" новому пользователю
                await _userManager.AddToRoleAsync(user, "Client");

                // Возможно, сразу войти в систему или перенаправить на страницу подтверждения
                // await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok("Пользователь успешно зарегистрирован и ему назначена роль Клиент.");
            }
            else
            {
                // Добавляем ошибки в ModelState для отображения в представлении
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        //return View(model); // Возвращаем представление с ошибками валидации
    }*/
}