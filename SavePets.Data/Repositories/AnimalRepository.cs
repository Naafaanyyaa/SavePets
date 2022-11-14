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
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        private readonly ILogger<AnimalRepository> _logger;

        public AnimalRepository(ApplicationDbContext db, ILogger<AnimalRepository> logger) : base(db)
        {
            _logger = logger;
        }

        public override async Task<Animal> AddAsync(Animal entity)
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

        public override Task<Animal?> GetByIdAsync(string id)
        {
            return _db.Animals.AsNoTracking()
                .Include(x => x.Photos)
                .Include(x => x.Location)
                .Include(x => x.Contacts)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task DeleteById(string id)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var animal = _db.Animals.Single(x => x.Id == id);
                _db.Remove(animal);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while deleting a new animal: {EMessage}", e.Message);
            }
        }

        public override async Task DeleteAsync(Animal animal)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                _db.Remove(animal);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while deleting a new animal: {EMessage}", e.Message);
            }
        }

        public override async Task UpdateAsync(Animal entity)
        {

            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                _db.Animals.Update(entity);
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
