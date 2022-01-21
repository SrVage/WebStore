using WebStore.Services.Interfaces;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly WebStoreDB _dataBase;
        private readonly ILogger<DbInitializer> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DbInitializer(WebStoreDB dataBase, ILogger<DbInitializer> logger, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _dataBase = dataBase;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync(bool RemoveBefore = false, CancellationToken cancel = default)
        {
            if (RemoveBefore)
                await RemoveAsync(cancel);
            var pendingMigration = await _dataBase.Database.GetPendingMigrationsAsync(cancel);
            if (pendingMigration.Any())
            {
                _logger.LogInformation("Выполнение миграции");
                await _dataBase.Database.MigrateAsync(cancel).ConfigureAwait(false);
            }
            
            await InitializeProductAsync(cancel).ConfigureAwait(false);
            await InitializeEmployersAsync(cancel).ConfigureAwait(false) ;
            await InitializeIdentityAsync(cancel).ConfigureAwait(false);
            _logger.LogInformation("Инициализация базы данных");
        }

        public async Task<bool> RemoveAsync(CancellationToken cancel = default)
        {
            var task = await _dataBase.Database.EnsureDeletedAsync(cancel).ConfigureAwait(false);
            if (task)
                _logger.LogInformation("Удаление базы данных");
            else
                _logger.LogInformation("Удаление базы не требуется");
            return task;
        }

        private async Task InitializeProductAsync(CancellationToken cancel)
        {
            if (_dataBase.Sections.Any())
            {
                _logger.LogInformation("Инициализация тестовых данных не трубется");
                return; 
            }
            await using (await _dataBase.Database.BeginTransactionAsync(cancel))
            {
                await _dataBase.Sections.AddRangeAsync(TestData.Sections, cancel);
                await _dataBase.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON", cancel);
                await _dataBase.SaveChangesAsync(cancel);
                await _dataBase.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF", cancel);
                await _dataBase.Database.CommitTransactionAsync(cancel);
            }

            await using (await _dataBase.Database.BeginTransactionAsync(cancel))
            {
                await _dataBase.Brands.AddRangeAsync(TestData.Brands, cancel);
                await _dataBase.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON", cancel);
                await _dataBase.SaveChangesAsync(cancel);
                await _dataBase.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF", cancel);
                await _dataBase.Database.CommitTransactionAsync(cancel);
            }
            await using (await _dataBase.Database.BeginTransactionAsync(cancel))
            {
                await _dataBase.Products.AddRangeAsync(TestData.Products, cancel);
                await _dataBase.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON", cancel);
                await _dataBase.SaveChangesAsync(cancel);
                await _dataBase.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF", cancel);
                await _dataBase.Database.CommitTransactionAsync(cancel);
            }
            _logger.LogInformation("Инициализация тестовых данных завершена");
        }

        private async Task InitializeEmployersAsync(CancellationToken cancel)
        {
            if (await _dataBase.Employers.AnyAsync(cancel))
            {
                _logger.LogInformation("Инициализация сотрудников не требуется");
                return;
            }

            _logger.LogInformation("Инициализация сотрудников...");
            await using var transaction = await _dataBase.Database.BeginTransactionAsync(cancel);

            TestData.Employers.ForEach(e => e.ID = 0);

            await _dataBase.Employers.AddRangeAsync(TestData.Employers, cancel);
            await _dataBase.SaveChangesAsync(cancel);

            await transaction.CommitAsync(cancel);
            _logger.LogInformation("Инициализация сотрудников выполнена успешно");
        }

        private async Task InitializeIdentityAsync(CancellationToken cancel)
        {
            _logger.LogInformation("Инициализация Identity");
            var timer = Stopwatch.StartNew();
            async Task CheckRole(string roleName)
            {
                if (await _roleManager.RoleExistsAsync(roleName))
                {
                    _logger.LogInformation("Роль {0} существует", roleName);
                }
                else
                {
                    _logger.LogInformation("Создание роли {0}", roleName);
                    await _roleManager.CreateAsync(new Role { Name = roleName });
                    _logger.LogInformation("Роль {0} создана", roleName);
                }
            }
            await CheckRole(Role.Administrators);
            await CheckRole(Role.Users);
            
            if (await _userManager.FindByNameAsync(User.Administrator) is null)
            {
                _logger.LogInformation("Создание позьзователя {0}", User.Administrator);
                var admin = new User
                {
                    UserName = User.Administrator
                };
                var creationResult = await _userManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creationResult.Succeeded)
                {
                    _logger.LogInformation("Пользователь {0} создан", User.Administrator);
                    await _userManager.AddToRoleAsync(admin, Role.Administrators);
                    _logger.LogInformation("Пользователь {0} готов", User.Administrator);
                }
                else
                {
                    var errors = creationResult.Errors.Select(e => e.Description);
                    _logger.LogError("Учетная запись администратора не создана. Ошибки: {0}", string.Join(", ", errors));
                    throw new InvalidOperationException($"Невозможно создать пользователя {User.Administrator} по причиние {string.Join(", ", errors)}");
                }
            }
        }
    }
}
