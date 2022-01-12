using WebStore.Services.Interfaces;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;
using WebStore.Data;
namespace WebStore.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly WebStoreDB _dataBase;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(WebStoreDB dataBase, ILogger<DbInitializer> logger)
        {
            _dataBase = dataBase;
            _logger = logger;
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
    }
}
