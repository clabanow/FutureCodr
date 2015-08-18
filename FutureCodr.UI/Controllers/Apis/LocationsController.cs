namespace FutureCodr.UI.Controllers.Apis
{
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class LocationsController : ApiController
    {
        private readonly ILocationRepository _locationsRepo;

        public LocationsController(ILocationRepository locationsRepo)
        {
            _locationsRepo = locationsRepo;
        }

        public void Delete(int id)
        {
            _locationsRepo.DeleteLocation(id);
        }

        public IEnumerable<Location> Get()
        {
            return _locationsRepo.GetAllLocations();
        }

        public HttpResponseMessage Post(Location location)
        {
            if (base.ModelState.IsValid)
            {
                _locationsRepo.AddLocation(location);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return base.Request.CreateErrorResponse(HttpStatusCode.BadRequest, base.ModelState);
        }
    }
}
