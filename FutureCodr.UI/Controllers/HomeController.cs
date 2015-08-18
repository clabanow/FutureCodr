namespace FutureCodr.UI.Controllers
{
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using FutureCodr.UI.Models.Home;
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        //dependency injection with Ninject
        private IBootcampLocationsRepository _bootcampLocationsRepo;
        private IBootcampRepository _bootcampRepo;
        private IBootcampTechnologyRepository _bootcampTechnologyRepo;
        private IContactFormRepository _contactFormRepo;
        private ILocationRepository _locationRepo;
        private ITechnologyRepository _technologyRepo;

        public HomeController(IContactFormRepository contactFromRepo, 
            IBootcampRepository bootcampRepo, 
            ITechnologyRepository technologyRepo, 
            ILocationRepository locationRepo, 
            IBootcampLocationsRepository bootcampLocationsRepo, 
            IBootcampTechnologyRepository bootcampTechnologyRepo)
        {
            _contactFormRepo = contactFromRepo;
            _bootcampRepo = bootcampRepo;
            _technologyRepo = technologyRepo;
            _locationRepo = locationRepo;
            _bootcampLocationsRepo = bootcampLocationsRepo;
            _bootcampTechnologyRepo = bootcampTechnologyRepo;
        }

        public ActionResult Contact()
        {
            return View(new HomeContactViewModel());
        }

        [HttpPost]
        public ActionResult Contact(HomeContactViewModel model)
        {
            //return user to form if model is not valid
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //if valid, add to db and redirect to success page
            var contactInfoForm = new ContactForm
            {
                Email = model.Email,
                Name = model.Name,
                Message = model.Message
            };

            _contactFormRepo.AddContactForm(contactInfoForm);
            TempData["ContactInfo"] = contactInfoForm;

            return RedirectToAction("Success");
        }

        //creates a text string out of bootcamp's locations to be 
        //displayed on home page (i.e. 3 locations would be converted
        //to "<Location 1> + 2 others"
        public Bootcamp ConvertBootcampFormat(Bootcamp bootcamp)
        {
            var bootcamp2 = new Bootcamp
            {
                BootcampId = bootcamp.BootcampID,
                Name = bootcamp.Name,
                LogoLinkUrl = bootcamp.LogoLink
            };

            int locationsCount = _bootcampLocationsRepo.GetAllBootcampLocationsByBootcampId(bootcamp.BootcampID).Count;
            string str = "";
            if (locationsCount > 1)
            {
                if (locationsCount == 2)
                {
                    str = " + 1 other";
                }
                else
                {
                    str = " + " + ((locationsCount - 1)).ToString() + " others";
                }
            }
            var locationById = _locationRepo.GetLocationById(bootcamp.LocationID);
            bootcamp2.Location = locationById.City + ", " + locationById.Country + str;
            return bootcamp2;
        }

        //filters bootcamps when user searches on home page
        public List<Bootcamp> FilterBootcamps(SearchParams searchParams)
        {
            //get all the bootcamps
            var allBootcamps = _bootcampRepo.GetAllBootcamps();

            //create empty list of bootcamps to which the ones which 
            //pass the filter test will be added
            var filteredBootcamps = new List<Bootcamp>();

            //get the price range based on user selection
            PriceRange priceRange = new PriceRange();
            if (searchParams.SelectedPriceRange.HasValue)
            {
                priceRange = GetPriceRange(searchParams.SelectedPriceRange.Value);
            }

            //loop through each bootcamp and check each user selection against it
            foreach (FutureCodr.Models.Bootcamp bootcamp in allBootcamps)
            {
                //first get all of the bootcamp's location and technology IDs
                //to compare to the user's selection
                List<int> bootcampLocationIds = _bootcampLocationsRepo
                                                .GetAllBootcampLocationsByBootcampId(bootcamp.BootcampID)
                                                .Select(m => m.LocationID).ToList();
                List<int> bootcampTechnologyIds = _bootcampTechnologyRepo
                                                .GetAllBootcampTechnologiesByBootcampId(bootcamp.BootcampID)
                                                .Select(m => m.TechnologyID).ToList();

                //then check each bootcamp against price, location and technology
                if ((!searchParams.SelectedPriceRange.HasValue || ((bootcamp.Price >= priceRange.Min) && (bootcamp.Price <= priceRange.Max)))
                 && (!searchParams.SelectedLocationId.HasValue || !bootcampLocationIds.Contains(searchParams.SelectedLocationId))
                 && (!searchParams.SelectedTechnologyId.HasValue || !bootcampTechnologyIds.Contains(searchParams.SelectedTechnologyId))
                {
                    filteredBootcamps.Add(bootcamp);
                }
            }

            //convert all of the bootcamps that passed the filter into the
            //correct format and return the list
            var filteredAndConvertedBootcampList = new List<FutureCodr.UI.Models.Home.Bootcamp>();
            foreach (FutureCodr.Models.Bootcamp bootcamp in list2)
            {
                filteredAndConvertedBootcampList.Add(ConvertBootcampFormat(bootcamp));
            }
            return filteredAndConvertedBootcampList;
        }

        //takes a user selection and gets the associated price range
        public PriceRange GetPriceRange(int selection)
        {
            switch (selection)
            {
                case 1:
                    return new PriceRange { Min = 0, Max = 0x1387 };

                case 2:
                    return new PriceRange { Min = 0x1388, Max = 0x270f };

                case 3:
                    return new PriceRange { Min = 0x2710, Max = 0x3a97 };

                case 4:
                    return new PriceRange { Min = 0x3a98, Max = 0x186a0 };
            }
            return new PriceRange { Min = 0, Max = 0xc350 };
        }

        [Inject]
        [OutputCache(CacheProfile = "IndexCache1Hour", Location = OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var model = new HomeIndexViewModel();

            //cycle through each bootcamp
            foreach (FutureCodr.Models.Bootcamp bootcamp in _bootcampRepo.GetAllBootcamps())
            {
                //convert each into correct format for view model and add to model
                FutureCodr.UI.Models.Home.Bootcamp item = ConvertBootcampFormat(bootcamp);
                model.Bootcamps.Add(item);
            }
            
            //populate search dropdowns with all locations, technologies, and price ranges
            model.SearchParams.Populate(_locationRepo.GetAllLocations(), _technologyRepo.GetAllTechnologies());
            return View(model);
        }

        //when user submits search query form, filter the bootcamps
        //and repopulate the dropdown menus
        [HttpPost, Inject]
        public ActionResult Index(HomeIndexViewModel model)
        {
            model.Bootcamps = FilterBootcamps(model.SearchParams);
            model.SearchParams.Populate(_locationRepo.GetAllLocations(), _technologyRepo.GetAllTechnologies());
            return View(model);
        }

        //users can search using URL i.e. http://www.futurecodr/home/location/houston
        public ActionResult Location(string id)
        {
            //parse the id and get the correct locationId
            string city = id.Replace("-", " ");
            int? locationIDByCity = _locationRepo.GetLocationIDByCity(city);

            //if the city does not exist, send user back to home page
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            //if it does, filter based on city
            var model = new HomeIndexViewModel
            {
                Bootcamps = FilterBootcamps(new SearchParams { SelectedLocationId = locationIDByCity})
            };

            //repopulate drop down lists with the city passed in
            model.SearchParams.Populate(_locationRepo.GetAllLocations(), _technologyRepo.GetAllTechnologies());
            model.SearchParams.SelectedLocationId = locationIDByCity;

            return View("Index", model);
        }

        public ActionResult Success()
        {
            ContactForm form = new ContactForm();
            //check if there is info stored in TempData
            //from when user filled out form
            if (TempData["ContactInfo"] != null)
            {
                //if so, transfer data to form variable
                form = (ContactForm)TempData["ContactInfo"];
            }

            //add info from form to the view model
            HomeSuccessViewModel model = new HomeSuccessViewModel
            {
                Name = form.Name,
                Email = form.Email
            };
            return View(model);
        }

        //allows users to search for bootcamps in URL i.e. futurecodr.com/home/technolgy/ruby-on-rails
        public ActionResult Technology(string id)
        {
            HomeIndexViewModel model;

            //parse the id string and get the technology's id
            string name = id.Replace("-", " ");
            int? technologyIdByName = _technologyRepo.GetTechnologyIdByName(name);

            //if it does not exist, send user back to home page
            if (id == null)
            {
                RedirectToAction("Index");
            }

            //otherwise, filter based on technology 
            model = new HomeIndexViewModel
            {
                Bootcamps = FilterBootcamps(new SearchParams { SelectedTechnologyId = technologyIdByName})
            };

            //repopulate drop down search boxes, passing in searched-for technology
            model.SearchParams.Populate(_locationRepo.GetAllLocations(), _technologyRepo.GetAllTechnologies());
            model.SearchParams.SelectedTechnologyId = technologyIdByName;

            return View("Index", model);
        }
    }
}
