using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutureCodr.Models;

namespace FutureCodr.Data.Interfaces
{
    public interface ILocationRepository
    {
        List<Location> GetAllLocations();
        Location GetLocationById(int id);
        Location AddLocation(Location location);
        void DeleteLocation(int id);
        int? GetLocationIDByCity(string city);
    }
}
