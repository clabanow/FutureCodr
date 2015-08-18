using FutureCodr.Models;
using System;
using System.Collections.Generic;

namespace FutureCodr.Data.Interfaces
{
    public interface IBootcampTechnologyRepository
    {
        List<BootcampTechnology> GetAllBootcampTechnologies();
        BootcampTechnology GetBootcampTechnologyById(int id);
        List<BootcampTechnology> GetAllBootcampTechnologiesByBootcampId(int id);
        List<BootcampTechnology> GetAllBootcampTechnologiesByTechnologyId(int id);
        BootcampTechnology AddBootcampTechnology(BootcampTechnology obj);
        void DeleteBootcampTechnology(int id);
    }
}
