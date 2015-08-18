namespace FutureCodr.UI.Controllers
{
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class TechnologyController : ApiController
    {
        private ITechnologyRepository _techRepo;

        public TechnologyController(ITechnologyRepository techRepo)
        {
            _techRepo = techRepo;
        }

        public void Delete(int id)
        {
            _techRepo.DeleteTechnology(id);
        }

        public IEnumerable<Technology> Get()
        {
            return _techRepo.GetAllTechnologies();
        }

        public HttpResponseMessage Post(Technology technology)
        {
            if (base.ModelState.IsValid)
            {
                _techRepo.AddTechnology(technology);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return base.Request.CreateErrorResponse(HttpStatusCode.BadRequest, base.ModelState);
        }
    }
}
