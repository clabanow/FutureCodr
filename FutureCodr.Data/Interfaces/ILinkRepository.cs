using FutureCodr.Models;
using System;
using System.Collections.Generic;

namespace FutureCodr.Data.Interfaces
{
    public interface ILinkRepository
    {
        List<Link> GetAllLinks();
        List<Link> GetAllLinksByBootcampId(int bootcampId);
        Link GetLinkById(int linkId);
        Link AddLink(Link link);
        void EditLink(Link link);
        void DeleteLink(int linkId);
    }
}
