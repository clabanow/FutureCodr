using FutureCodr.Models;
using System;
using System.Collections.Generic;

namespace FutureCodr.Data.Interfaces
{
    public interface IBootcampRepository
    {
        List<Bootcamp> GetAllBootcamps();
        Bootcamp GetBootcampByID(int bootcampID);
        Bootcamp AddBootcamp(Bootcamp bootcamp);
        void EditBootcamp(Bootcamp bootcamp);
        void DeleteBootcamp(int bootcampID);
        int? GetBootcampIDByName(string bootcampName);
    }
}
