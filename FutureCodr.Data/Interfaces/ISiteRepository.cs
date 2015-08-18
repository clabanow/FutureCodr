using FutureCodr.Models;
using System;
using System.Collections.Generic;

namespace FutureCodr.Data.Interfaces
{
    public interface ISiteRepository
    {
        Site AddSite(Site site);
        void DeleteSite(int id);
        List<Site> GetAllSites();
        Site GetSiteById(int id);
    }
}
