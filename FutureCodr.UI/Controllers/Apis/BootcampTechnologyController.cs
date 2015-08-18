namespace FutureCodr.UI.Controllers.Apis
{
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using FutureCodr.UI.Models.Angular;
    using FutureCodr.UI.Models.Angular.technology;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class BootcampTechnologyController : ApiController
    {
        private readonly IBootcampRepository _bootcampRepo;
        private readonly IBootcampTechnologyRepository _bootcampTechRepo;
        private readonly ITechnologyRepository _techRepo;

        public BootcampTechnologyController(IBootcampTechnologyRepository bootcampTechRepo, 
            ITechnologyRepository techRepo, 
            IBootcampRepository bootcampRepo)
        {
            _bootcampTechRepo = bootcampTechRepo;
            _techRepo = techRepo;
            _bootcampRepo = bootcampRepo;
        }

        public void Delete(int id)
        {
            _bootcampTechRepo.DeleteBootcampTechnology(id);
        }

        public AdminBootcampTechnologyViewModel Get()
        {
            AdminBootcampTechnologyViewModel model = new AdminBootcampTechnologyViewModel();

            //get all bootcamp technologies, and convert each to correct format
            //before adding each to the angular view model
            foreach (BootcampTechnology technology in _bootcampTechRepo.GetAllBootcampTechnologies())
            {
                BootcampTechnologyAng item = new BootcampTechnologyAng
                {
                    BootcampTechnologyId = technology.BootcampTechnologyID,
                    BootcampName = _bootcampRepo.GetBootcampByID(technology.BootcampID.Value).Name,
                    Technology = _techRepo.GetTechnologyById(technology.TechnologyID.Value).Name
                };
                model.BootcampTechnologies.Add(item);
            }

            //get all bootcamps (name and id) and add to the model
            foreach (Bootcamp bootcamp in _bootcampRepo.GetAllBootcamps())
            {
                BootcampAng ang2 = new BootcampAng
                {
                    BootcampID = bootcamp.BootcampID,
                    Name = bootcamp.Name
                };
                model.Bootcamps.Add(ang2);
            }

            //get all technologies (name and id) and add to the view model
            foreach (Technology technology2 in _techRepo.GetAllTechnologies())
            {
                TechnologyAng ang3 = new TechnologyAng
                {
                    TechnologyId = technology2.TechnologyID,
                    Name = technology2.Name
                };
                model.Technologies.Add(ang3);
            }
            return model;
        }

        public HttpResponseMessage Post(BootcampTechnology bootcampTechnology)
        {
            if (!_bootcampTechRepo.GetAllBootcampTechnologiesByBootcampId(bootcampTechnology.BootcampID.Value).Any<BootcampTechnology>(m => ((m.TechnologyID == bootcampTechnology.TechnologyID) && ModelState.IsValid)))
            {
                _bootcampTechRepo.AddBootcampTechnology(bootcampTechnology);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return base.Request.CreateErrorResponse(HttpStatusCode.BadRequest, base.ModelState);
        }
    }
}
