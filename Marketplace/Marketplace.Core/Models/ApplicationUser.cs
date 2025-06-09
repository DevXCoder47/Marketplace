using Marketplace.Core.Helpers;
using Marketplace.Core.Interfaces;
using Microsoft.AspNetCore.Identity; // Необходимо для IdentityUser
using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Core.Models
{
    // Ваш пользовательский класс, наследующий от IdentityUser
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        // IdentityUser уже предоставляет Id (string), UserName, Email, PhoneNumber, PasswordHash и т.д.
        // Вам не нужно добавлять поле Password, так как Identity управляет им через PasswordHash.

        [Required]
        [StringLength(50)] // Максимальная длина для никнейма
        public string Nickname { get; set; } = null!;

        public string? Description { get; set; }

        public OnlineStatus Status { get; set; } = OnlineStatus.Inactive; // Значение по умолчанию

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Время создания (по умолчанию UTC)

        // Вы можете добавить другие поля, специфичные для вашего Marketplace, например:
        public string? ProfilePictureUrl { get; set; }
    }
}
