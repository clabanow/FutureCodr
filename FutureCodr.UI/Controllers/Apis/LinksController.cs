namespace FutureCodr.UI.Controllers.Apis
{
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using FutureCodr.UI.Models.Angular;
    using FutureCodr.UI.Models.Angular.sites;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class LinksController : ApiController
    {
        private readonly IBootcampRepository _bootcampRepo;
        private readonly ILinkRepository _linkRepo;
        private readonly ISiteRepository _siteRepo;

        public LinksController(ILinkRepository linkRepo, 
            IBootcampRepository bootcampRepo, 
            ISiteRepository siteRepo)
        {
            _linkRepo = linkRepo;
            _bootcampRepo = bootcampRepo;
            _siteRepo = siteRepo;
        }

        public void Delete(int id)
        {
            _linkRepo.DeleteLink(id);
        }

        public LinksAng Get()
        {
            LinksAng ang = new LinksAng();

            //get all links from db and loop through them
            foreach (Link link in _linkRepo.GetAllLinks())
            {
                //convert to correct format for the angular view model
                LinkAng item = new LinkAng
                {
                    LinkID = link.LinkID,
                    URL = link.URL,
                    LinkText = link.LinkText,
                    LinkSiteName = _siteRepo.GetSiteById(link.SiteID.Value).SiteName,
                    Date = link.Date.ToString("d")
                };
                item.BootcampName = _bootcampRepo.GetBootcampByID(link.BootcampID.Value).Name;

                //add each converted object to the view model
                ang.Links.Add(item);
            }

            //get all sites in correct format and add to model
            foreach (Site site in _siteRepo.GetAllSites())
            {
                SiteAng ang4 = new SiteAng
                {
                    SiteId = site.SiteID,
                    SiteName = site.SiteName
                };
                ang.Sites.Add(ang4);
            }

            //get all bootcamps in correct format and add to view model
            foreach (Bootcamp bootcamp in _bootcampRepo.GetAllBootcamps())
            {
                BootcampAng ang5 = new BootcampAng
                {
                    BootcampID = bootcamp.BootcampID,
                    Name = bootcamp.Name
                };
                ang.Bootcamps.Add(ang5);
            }
            return ang;
        }

        public HttpResponseMessage Post(Link link)
        {
            if (base.ModelState.IsValid)
            {
                _linkRepo.AddLink(link);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return base.Request.CreateErrorResponse(HttpStatusCode.BadRequest, base.ModelState);
        }
    }
}
