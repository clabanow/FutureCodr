namespace FutureCodr.UI.Controllers.Apis
{
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using FutureCodr.UI.Models.Angular;
    using FutureCodr.UI.Models.Angular.bootcampLocations;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class BootcampLocationController : ApiController
    {
        private readonly IBootcampLocationsRepository _bootcampLocationsRepo;
        private readonly IBootcampRepository _bootcampRepo;
        private readonly ILocationRepository _locationRepo;

        public BootcampLocationController(IBootcampLocationsRepository bootcampLocationsRepo, 
            IBootcampRepository bootcampRepo, 
            ILocationRepository locationRepo)
        {
            _bootcampLocationsRepo = bootcampLocationsRepo;
            _bootcampRepo = bootcampRepo;
            _locationRepo = locationRepo;
        }

        public void Delete(int id)
        {
            _bootcampLocationsRepo.DeleteBootcampLocation(id);
        }

        public BootcampLocationViewModel Get()
        {
            BootcampLocationViewModel model = new BootcampLocationViewModel();

            //get all bootcamp locations to be displayed in overview table
            foreach (BootcampLocation location in _bootcampLocationsRepo.GetAllBootcampLocations())
            {
                //for each bootcamp location, convert format and add to view model
                Location locationById = _locationRepo.GetLocationById(location.LocationID);
                BootcampLocationAng item = new BootcampLocationAng
                {
                    Id = location.BootcampLocationID,
                    Bootcamp = _bootcampRepo.GetBootcampByID(location.BootcampID).Name,
                    Location = locationById.City + ", " + locationById.Country
                };
                model.BootcampLocations.Add(item);
            }

            //get all bootcamps with their Ids for drop down list
            foreach (Bootcamp bootcamp in _bootcampRepo.GetAllBootcamps())
            {
                //convert format
                BootcampAng ang2 = new BootcampAng
                {
                    BootcampID = bootcamp.BootcampID,
                    Name = bootcamp.Name
                };

                //add converted technology to the list
                model.Bootcamps.Add(ang2);
            }

            //get all locations with their Ids for drop down list
            foreach (Location location3 in _locationRepo.GetAllLocations())
            {
                //convert format
                LocationAng ang3 = new LocationAng
                {
                    LocationId = location3.LocationID,
                    Name = location3.City + ", " + location3.Country
                };

                //add converted location to list
                model.Locations.Add(ang3);
            }
            return model;
        }

        public HttpResponseMessage Post(BootcampLocation model)
        {
            if (base.ModelState.IsValid)
            {
                _bootcampLocationsRepo.AddBootcampLocation(model);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return base.Request.CreateErrorResponse(HttpStatusCode.BadRequest, base.ModelState);
        }
    }
}
