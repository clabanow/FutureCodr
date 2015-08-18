namespace FutureCodr.UI.Controllers.Apis
{
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using FutureCodr.UI.Models;
    using FutureCodr.UI.Models.Angular;
    using FutureCodr.UI.Models.Angular.bootcamps;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class BootcampsController : ApiController
    {
        private IBootcampRepository _bootcampRepo;
        private ILocationRepository _locationRepo;
        private ITechnologyRepository _techRepo;

        public BootcampsController(IBootcampRepository bootcampRepo, 
            ILocationRepository locationRepo, 
            ITechnologyRepository techRepo)
        {
            _bootcampRepo = bootcampRepo;
            _locationRepo = locationRepo;
            _techRepo = techRepo;
        }

        public void Delete(int id)
        {
            _bootcampRepo.DeleteBootcamp(id);
        }

        public AdminBootcampListViewModel Get()
        {
            AdminBootcampListViewModel model = new AdminBootcampListViewModel();

            //loop through each bootcamp and convert format for view model
            foreach (Bootcamp bootcamp in _bootcampRepo.GetAllBootcamps())
            {
                BootcampListAng item = new BootcampListAng
                {
                    BootcampID = bootcamp.BootcampID,
                    Name = bootcamp.Name,
                    LengthInWeeks = bootcamp.LengthInWeeks,
                    PriceString = Helper.GetPriceString(bootcamp.Price, _locationRepo.GetLocationById(bootcamp.LocationID).Country)
                };


                Location locationById = _locationRepo.GetLocationById(bootcamp.LocationID);
                item.Location = locationById.City + ", " + locationById.Country;
                item.Technology = _techRepo.GetTechnologyById(bootcamp.PrimaryTechnologyID).Name;

                //add each converted bootcamp object to the view model
                model.Bootcamps.Add(item);
            }

            //get each location and convert format for view model (drop down list)
            foreach (Location location2 in _locationRepo.GetAllLocations())
            {
                LocationAng ang2 = new LocationAng
                {
                    LocationId = location2.LocationID,
                    Name = location2.City + ", " + location2.Country
                };
                model.Locations.Add(ang2);
            }

            //get each technology and convert format for view model (drop down list)
            foreach (Technology technology in _techRepo.GetAllTechnologies())
            {
                TechnologyAng ang3 = new TechnologyAng
                {
                    TechnologyId = technology.TechnologyID,
                    Name = technology.Name
                };
                model.Technologies.Add(ang3);
            }
            return model;
        }

        //get specific bootcamp for editing
        public BootcampEditAng Get(int id)
        {
            Bootcamp bootcampByID = _bootcampRepo.GetBootcampByID(id);

            return new BootcampEditAng 
            { 
                Name = bootcampByID.Name, 
                Price = bootcampByID.Price, 
                LengthInWeeks = bootcampByID.LengthInWeeks, 
                Website = bootcampByID.Website, 
                LogoLink = bootcampByID.LogoLink, 
                LocationID = bootcampByID.LocationID, 
                PrimaryTechnologyID = bootcampByID.PrimaryTechnologyID, 
                BootcampID = bootcampByID.BootcampID 
            };
        }

        public HttpResponseMessage Post(Bootcamp bootcamp)
        {
            if (base.ModelState.IsValid)
            {
                _bootcampRepo.AddBootcamp(bootcamp);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return base.Request.CreateErrorResponse(HttpStatusCode.BadRequest, base.ModelState);
        }

        public HttpResponseMessage Put(Bootcamp bootcamp)
        {
            if (base.ModelState.IsValid)
            {
                _bootcampRepo.EditBootcamp(bootcamp);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return base.Request.CreateErrorResponse(HttpStatusCode.BadRequest, base.ModelState);
        }
    }
}
