namespace FutureCodr.UI.Controllers.Apis
{
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using FutureCodr.UI.Models.Angular;
    using FutureCodr.UI.Models.Angular.sessions;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class BootcampSessionController : ApiController
    {
        private readonly IBootcampLocationsRepository _bootcampLocationsRepo;
        private readonly IBootcampRepository _bootcampRepo;
        private readonly IBootcampSessionRepository _bootcampSessionRepo;
        private readonly IBootcampTechnologyRepository _bootcampTechRepo;
        private readonly ILocationRepository _locationRepo;
        private readonly ITechnologyRepository _techRepo;

        public BootcampSessionController(IBootcampRepository bootcampRepo, 
            ILocationRepository locationRepo, 
            ITechnologyRepository techRepo, 
            IBootcampSessionRepository bootcampSessionRepo, 
            IBootcampLocationsRepository bootcampLocationsRepo, 
            IBootcampTechnologyRepository bootcampTechRepo)
        {
            _bootcampRepo = bootcampRepo;
            _locationRepo = locationRepo;
            _techRepo = techRepo;
            _bootcampSessionRepo = bootcampSessionRepo;
            _bootcampLocationsRepo = bootcampLocationsRepo;
            _bootcampTechRepo = bootcampTechRepo;
        }

        public void Delete(int id)
        {
            _bootcampSessionRepo.DeleteBootcampSession(id);
        }

        public AdminBootcampSessionListViewModel Get()
        {
            AdminBootcampSessionListViewModel model = new AdminBootcampSessionListViewModel();

            //get all bootcamp sessions and convert their format for view model
            foreach (BootcampSession session in _bootcampSessionRepo.GetAllBootcampSessions())
            {
                BootcampSessionAng item = new BootcampSessionAng
                {
                    BootcampSessionId = session.BootcampSessionID,
                    BootcampName = _bootcampRepo.GetBootcampByID(session.BootcampID.Value).Name
                };
                Location locationById = _locationRepo.GetLocationById(session.LocationID.Value);
                item.Location = locationById.City + ", " + locationById.Country;
                item.Technology = _techRepo.GetTechnologyById(session.TechnologyID.Value).Name;
                item.StartDate = session.StartDate.ToString("d");
                item.EndDate = session.EndDate.ToString("d");

                //add converted bootcamp session to model
                model.Sessions.Add(item);
            }

            //get a list of all bootcamps (in converted format) for drop down list
            foreach (Bootcamp bootcamp in _bootcampRepo.GetAllBootcamps())
            {
                BootcampAng ang2 = new BootcampAng
                {
                    BootcampID = bootcamp.BootcampID,
                    Name = bootcamp.Name
                };
                model.Bootcamps.Add(ang2);
            }
            return model;
        }

        //
        public BootcampSessionAddAng Get(int id)
        {
            //create a new view model, passing in the desired
            //session id and name
            BootcampSessionAddAng ang = new BootcampSessionAddAng
            {
                BootcampSessionId = id
            };
            ang.BootcampName = _bootcampRepo.GetBootcampByID(id).Name;

            //add all of the bootcamp's locations to the view model
            foreach (BootcampLocation location in _bootcampLocationsRepo.GetAllBootcampLocationsByBootcampId(id))
            {
                Location locationById = _locationRepo.GetLocationById(location.LocationID);
                LocationAng item = new LocationAng
                {
                    LocationId = location.LocationID,
                    Name = locationById.City + ", " + locationById.Country
                };
                ang.Locations.Add(item);
            }

            //add all of the bootcamp's technologies to the view model
            foreach (BootcampTechnology technology in _bootcampTechRepo.GetAllBootcampTechnologiesByBootcampId(id))
            {
                TechnologyAng ang3 = new TechnologyAng
                {
                    TechnologyId = technology.TechnologyID.Value,
                    Name = _techRepo.GetTechnologyById(technology.TechnologyID.Value).Name
                };
                ang.Technologies.Add(ang3);
            }
            return ang;
        }

        public HttpResponseMessage Post(BootcampSession session)
        {
            if (base.ModelState.IsValid)
            {
                _bootcampSessionRepo.AddBootcampSession(session);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return base.Request.CreateErrorResponse(HttpStatusCode.BadRequest, base.ModelState);
        }
    }
}
