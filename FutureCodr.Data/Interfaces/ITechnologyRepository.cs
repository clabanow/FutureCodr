using FutureCodr.Models;
using System;
using System.Collections.Generic;

namespace FutureCodr.Data.Interfaces
{
    public interface ITechnologyRepository
    {
        List<Technology> GetAllTechnologies();
        Technology GetTechnologyById(int id);
        Technology AddTechnology(Technology technology);
        void DeleteTechnology(int id);
        int? GetTechnologyIdByName(string name);
    }
}
