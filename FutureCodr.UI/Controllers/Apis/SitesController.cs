namespace FutureCodr.UI.Controllers.Apis
{
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using FutureCodr.UI.Models.Angular.sites;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class SitesController : ApiController
    {
        private ISiteRepository _siteRepo;

        public SitesController(ISiteRepository siteRepo)
        {
            _siteRepo = siteRepo;
        }

        public void Delete(int id)
        {
            _siteRepo.DeleteSite(id);
        }

        //returns all sites from db in correct, converted format
        public IEnumerable<SiteAng> Get()
        {
            List<SiteAng> list = new List<SiteAng>();
            foreach (Site site in _siteRepo.GetAllSites())
            {
                SiteAng item = new SiteAng
                {
                    SiteId = site.SiteID,
                    SiteName = site.SiteName,
                    SiteLogoUrl = site.SiteLogoURL
                };
                list.Add(item);
            }
            return list;
        }

        public HttpResponseMessage Post(Site site)
        {
            if (base.ModelState.IsValid)
            {
                _siteRepo.AddSite(site);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return base.Request.CreateErrorResponse(HttpStatusCode.BadRequest, base.ModelState);
        }
    }
}
