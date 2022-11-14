using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavePets.Data.Entities;
using SavePets.Data.Interfaces;

namespace SavePets.Data.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository

    {
        private readonly ILogger<LocationRepository> _logger;

        public LocationRepository(ApplicationDbContext db, ILogger<LocationRepository> logger) : base(db)
        {
            _logger = logger;
        }

        public override async Task<Location> AddAsync(Location entity)
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

        public override Task<Location?> GetByIdAsync(string id)
        {
            return _db.Locations.AsNoTracking()
                .Include(x => x.Animal)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task UpdateAsync(Location entity)
        {

            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                _db.Locations.Update(entity);
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
