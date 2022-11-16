using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavePets.Data.Entities;
using SavePets.Data.Interfaces;

namespace SavePets.Data.Repositories
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        private readonly ILogger<Subscription> _logger;

        public SubscriptionRepository(ApplicationDbContext db, ILogger<Subscription> logger) : base(db)
        {
            _logger = logger;
        }

        public override async Task<Subscription> AddAsync(Subscription entity)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                await _db.AddAsync(entity);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return entity;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while creating a new animal: {EMessage}", e.Message);
            }

            return null;
        }

        public override Task<Subscription?> GetByIdAsync(string id)
        {
            return _db.Subscriptions.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task UpdateAsync(Subscription entity)
        {

            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                _db.Subscriptions.Update(entity);
                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                _db.Entry(entity).State = EntityState.Detached;
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while deleting a new animal: {EMessage}", e.Message);
            }
        }
    }
}
