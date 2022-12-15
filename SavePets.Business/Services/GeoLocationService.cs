using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using NetTopologySuite.Geometries;
using SavePets.Business.Exceptions;
using SavePets.Business.Interfaces;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;
using SavePets.Data.Entities;
using SavePets.Data.Interfaces;
using Location = SavePets.Data.Entities.Location;

namespace SavePets.Business.Services
{
    public class GeoLocationService : IGeoLocation
    {
        private readonly IMapper _mapper;
        private readonly IAnimalRepository _animalRepository;
        private readonly ILocationRepository _locationRepository;

        public GeoLocationService(IMapper mapper, IAnimalRepository animalRepository, ILocationRepository locationRepository)
        {
            _mapper = mapper;
            _animalRepository = animalRepository;
            _locationRepository = locationRepository;
        }

        public async Task<PetResponse> UpdateGeolocation(string animalId, GeoLocationRequest geolocation)
        {

            var animal = await _animalRepository.GetByIdAsync(animalId);
            
            if (animal == null)
            {
                throw new NotFoundException(nameof(animal), animalId);
            }

            var location = await _locationRepository.GetByIdAsync(animal.LocationId);

            if (location == null)
            {
                throw new NotFoundException(nameof(location));
            }

            var date = DateTime.Now;

            location.LastModifiedDate = date;

            location.Point = new Point(geolocation.Latitude, geolocation.Longitude) { SRID = 4326 };

            animal.Location = location;

            await _animalRepository.UpdateAsync(animal);

            var result = _mapper.Map<Animal, PetResponse>(animal);

            return result;
        }
    }
}
