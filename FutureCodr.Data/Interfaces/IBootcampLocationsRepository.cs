using FutureCodr.Models;
using System;
using System.Collections.Generic;

namespace FutureCodr.Data.Interfaces
{
    public interface IBootcampLocationsRepository
    {
        List<BootcampLocation> GetAllBootcampLocations();
        List<BootcampLocation> GetAllBootcampLocationsByBootcampId(int id);
        List<BootcampLocation> GetAllBootcampLocationsByLocationId(int id);
        BootcampLocation GetBootcampLocationById(int id);
        BootcampLocation AddBootcampLocation(BootcampLocation location);
        void DeleteBootcampLocation(int id);
    }
}
