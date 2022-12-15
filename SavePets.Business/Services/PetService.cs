using System.Linq.Expressions;
using System.Net.Http.Headers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using SavePets.Business.Exceptions;
using SavePets.Business.Infrastructure.Expressions;
using SavePets.Business.Interfaces;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;
using SavePets.Data.Entities;
using SavePets.Data.Entities.Identity;
using SavePets.Data.Enums;
using SavePets.Data.Interfaces;
using Location = SavePets.Data.Entities.Location;

namespace SavePets.Business.Services
{
    public class PetService : IPetService
    {
        private readonly IMapper _mapper;
        private readonly IAnimalRepository _animalRepository;
        private readonly IContactsRepository _contactsRepository;
        private readonly ILocationRepository _locationRepository;

        private readonly UserManager<User> _userManager;

        public PetService(IMapper mapper, IAnimalRepository animalRepository, UserManager<User> user, IContactsRepository contactsRepository, ILocationRepository locationRepository)
        {
            _mapper = mapper;
            _animalRepository = animalRepository;
            _userManager = user;
            _contactsRepository = contactsRepository;
            _locationRepository = locationRepository;
        }

        public async Task<List<PetResponse>> GetAllPetsByRequest(PetAllRequest request)
        {
            var predicate = CreateFilterPredicate(request);
            var source = await _animalRepository.GetAsync(predicate, includes: new List<Expression<Func<Animal, object>>>()
            {
                x => x.User,
                x => x.Contacts,
                x => x.Photos,
                x => x.Location
            });

            var result = _mapper.Map<List<Animal>, List<PetResponse>>(source);
            return result;
        }

        public async Task<PetResponse> GetPetById(string requestId)
        {
            var source = await _animalRepository.GetByIdAsync(requestId);

            if (source == null)
                throw new NotFoundException(nameof(Animal),requestId);

            var result = _mapper.Map<Animal, PetResponse>(source);
            return result;
        }

        public async Task<PetResponse> CreateAsync(PetRequest request, string UserId,  IFormFileCollection files, string directoryToSave)
        {
            var owner = await _userManager.FindByIdAsync(UserId);

            if (owner == null)
            {
                throw new NotFoundException(nameof(owner), UserId);
            }

            if (request.TelegramUrl == null 
                && request.InstagramUrl == null 
                && request.FacebookUrl == null 
                && request.ViberUrl == null 
                && request.Phone == null)
            {
                throw new ValidationException($"{nameof(owner)} with such id {UserId} tried to create page without any contact information.");
            }

            var date = DateTime.Now;

            var contacts = _mapper.Map<PetRequest, Contacts>(request);
            contacts.CreatedDate = date;

            var location = _mapper.Map<PetRequest, Location>(request);
            location.Point = new Point(request.Longitude, request.Latitude) { SRID = 4326 }; ;
            location.CreatedDate = date;

            var animal = _mapper.Map<PetRequest, Animal>(request);
            animal.UserId = owner.Id;
            animal.IsFounded = false;
            animal.ContactsId = contacts.Id;
            animal.LocationId = location.Id;
            animal.CreatedDate = date;

            if (files?.Any() == true)
            {
                animal.Photos = new List<Photo>();

                foreach (var file in files)
                {
                    var folderName = Path.Combine("Resources", "Documents", animal.Id);
                    var pathToSave = Path.Combine(directoryToSave, folderName);

                    if (!Directory.Exists(pathToSave))
                    {
                        var dirInfo = new DirectoryInfo(pathToSave);
                        dirInfo.Create();
                    }

                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);

                        await using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        animal.Photos.Add(new Photo()
                        {
                            PhotoPath = dbPath,
                            AnimalId = animal.Id,
                        });
                    }
                }
            }

            await _contactsRepository.AddAsync(contacts);
            await _locationRepository.AddAsync(location);
            await _animalRepository.AddAsync(animal);
   
            animal.Location = location;
            animal.Contacts = contacts;
            var result = _mapper.Map<Animal, PetResponse>(animal);

            return result;
        }


        public async Task DeleteByIdAsync(string userId, string animalId)
        {
            var animal = await _animalRepository.GetByIdAsync(animalId);

            if (animal == null)
            {
                throw new NotFoundException(nameof(animal), animalId);
            }

            if (animal.UserId != userId)
            {
                throw new NotFoundException($"{nameof(animal)} with such id {animalId} is not found with user with such id {userId}.");
            }

            await _animalRepository.DeleteAsync(animal);
        }

        public async Task<PetResponse> UpdateByIdAsync(string userId, string animalId, UpdateRequest request)
        {
            var animal = await _animalRepository.GetByIdAsync(animalId);
            var location = await _locationRepository.GetByIdAsync(animal.LocationId);
            var contacts = await _contactsRepository.GetByIdAsync(animal.ContactsId);


            if (animal == null || location == null || contacts == null)
            {
                throw new NotFoundException($"Some of objects is not found.");
            }

            if (!animal.UserId.Equals(userId))
            {
                throw new ValidationException("UserId is not same in model and Guid");
            }

            animal = _mapper.Map<UpdateRequest, Animal>(request, animal);
            contacts = _mapper.Map<UpdateRequest, Contacts>(request, contacts);
            location = _mapper.Map<UpdateRequest, Location>(request, location);

            var date = DateTime.Now;

            animal.LastModifiedDate = date;
            contacts.LastModifiedDate = date;
            location.LastModifiedDate = date;

            location.Point = new Point(request.Latitude, request.Longitude) { SRID = 4326 };

            animal.Location = location;
            animal.Contacts = contacts;

            await _animalRepository.UpdateAsync(animal);

            var result = _mapper.Map<Animal, PetResponse>(animal);

            return result;
        }

        private Expression<Func<Animal, bool>>? CreateFilterPredicate(PetAllRequest request)
        {
            Expression<Func<Animal, bool>>? predicate = null;

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                Expression<Func<Animal, bool>> searchStringExpression = x =>
                    x.AnimalDescription != null && x.AnimalDescription.Contains(request.SearchString) ||
                    x.AnimalName.Contains(request.SearchString);

                predicate = ExpressionsHelper.And(predicate, searchStringExpression);
            }

            if (request.Status.HasValue && Enum.IsDefined(request.Status.Value))
            {
                Expression<Func<Animal, bool>> statusPredicate = x => x.AnimalType == request.Status.Value;
                predicate = ExpressionsHelper.And(predicate, statusPredicate);
            }

            if (request.StartDate.HasValue && request.EndDate.HasValue && request.StartDate < request.EndDate)
            {
                Expression<Func<Animal, bool>> dateExpression = x => x.CreatedDate > request.StartDate.Value
                                                                     && x.CreatedDate < request.EndDate.Value;
                predicate = ExpressionsHelper.And(predicate, dateExpression);
            }

            return predicate;
        }


    }
}
