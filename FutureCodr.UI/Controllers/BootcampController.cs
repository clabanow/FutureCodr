namespace FutureCodr.UI.Controllers
{
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using FutureCodr.UI.Models;
    using FutureCodr.UI.Models.Bootcamp;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class BootcampController : Controller
    {
        //dependency injection using Ninject
        private IBootcampLocationsRepository _bootcampLocationsRepo;
        private IBootcampRepository _bootcampRepo;
        private IBootcampTechnologyRepository _bootcampTechnologyRepo;
        private ILinkRepository _linkRepo;
        private ILocationRepository _locationRepo;
        private IBootcampSessionRepository _sessionRepo;
        private ISiteRepository _siteRepo;
        private ITechnologyRepository _technologyRepo;

        public BootcampController(IBootcampRepository bootcampRepo, 
            ITechnologyRepository technologyRepo, 
            ILocationRepository locationRepo, 
            IBootcampLocationsRepository bootcampLocationsRepo, 
            IBootcampTechnologyRepository bootcampTechnologyRepo, 
            ILinkRepository linkRepo, 
            IBootcampSessionRepository sessionRepo, 
            ISiteRepository siteRepo)
        {
            _bootcampRepo = bootcampRepo;
            _technologyRepo = technologyRepo;
            _locationRepo = locationRepo;
            _bootcampLocationsRepo = bootcampLocationsRepo;
            _bootcampTechnologyRepo = bootcampTechnologyRepo;
            _linkRepo = linkRepo;
            _sessionRepo = sessionRepo;
            _siteRepo = siteRepo;
        }

        [OutputCache(CacheProfile = "BootcampCache1Hour")]
        public ActionResult Index(string id)
        {
            //parse URL to get bootcamp ID
            string bootcampName = id.Replace("-", " ");
            int? bootcampID = _bootcampRepo.GetBootcampIDByName(bootcampName);

            //if bootcamp does not exist, redirect to the home page
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //get bootcampID and locationID
            var bootcampByID = _bootcampRepo.GetBootcampByID((int)bootcampID);
            var locationById = _locationRepo.GetLocationById(bootcampByID.LocationID);

            //insert basic bootcamp info into view model
            var model = new BootcampViewModel
            {
                BootcampID = (int)bootcampID,
                Location = locationById.City + ", " + locationById.Country,
                Name = bootcampByID.Name,
                LogoLink = bootcampByID.LogoLink,
                Url = bootcampByID.Website,
                Price = Helper.GetPriceString(bootcampByID.Price, _locationRepo.GetLocationById(bootcampByID.LocationID).Country)
            };


            //get all links info for bootcamp and add to view model
            foreach (var link in _linkRepo.GetAllLinksByBootcampId((int)bootcampID))
            {
                Site siteById = _siteRepo.GetSiteById((int)link.SiteID);
                string linkText = link.LinkText;
                if (linkText.Length > 60)
                {
                    linkText = linkText.Substring(0, 60) + "...";
                }
                FutureCodr.UI.Models.Bootcamp.Link item = new FutureCodr.UI.Models.Bootcamp.Link
                {
                    URL = link.URL,
                    SiteName = siteById.SiteName,
                    SiteLogoURL = siteById.SiteLogoURL,
                    Title = linkText,
                    Date = link.Date
                };
                model.Links.Add(item);
            }

            //order links by most recent
            model.Links = (from m in model.Links
                           orderby m.Date descending
                           select m).ToList();

            //add bootcamp locations to view model
            foreach (var location2 in _bootcampLocationsRepo.GetAllBootcampLocationsByBootcampId((int)bootcampID))
            {
                FutureCodr.Models.Location location3 = _locationRepo.GetLocationById(location2.LocationID);
                FutureCodr.UI.Models.Bootcamp.Location location4 = new FutureCodr.UI.Models.Bootcamp.Location
                {
                    City = location3.City,
                    Name = location3.City + ", " + location3.Country,
                    Id = location2.LocationID
                };
                model.Locations.Add(location4);
            }

            //add bootcamp sessions to view model if they start in the future
            foreach (var session in _sessionRepo.GetBootcampSessionsByBootcampId((int)bootcampID))
            {
                if (session.StartDate > DateTime.Now)
                {
                    Session session2 = new Session
                    {
                        Technology = _technologyRepo.GetTechnologyById(session.TechnologyID.Value).Name,
                        Location = _locationRepo.GetLocationById((int)session.LocationID).City + 
                                    ", " + 
                                    _locationRepo.GetLocationById(session.LocationID.Value).Country,
                        StartDate = session.StartDate,
                        EndDate = session.EndDate
                    };
                    model.Sessions.Add(session2);
                }
            }

            //order sessions by soonest to start
            model.Sessions = (from m in model.Sessions
                              orderby m.StartDate
                              select m).ToList();

            //add bootcamp technologies to the view model
            foreach (var technology in _bootcampTechnologyRepo.GetAllBootcampTechnologiesByBootcampId((int)bootcampID))
            {
                FutureCodr.UI.Models.Bootcamp.Technology technology2 = new FutureCodr.UI.Models.Bootcamp.Technology
                {
                    Name = _technologyRepo.GetTechnologyById(technology.TechnologyID.Value).Name,
                    Id = technology.TechnologyID.Value
                };
                model.Technologies.Add(technology2);
            }

            //return the strongly typed view with the view model passed in
            return View(model);
        }
    }
}
