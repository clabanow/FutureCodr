namespace FutureCodr.UI.Models.Home
{
    using FutureCodr.Models;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class SearchParams
    {
        public SearchParams()
        {
            Prices = new List<SelectListItem>();
            Technologies = new List<SelectListItem>();
            Locations = new List<SelectListItem>();
        }

        //populates the 3 search dropdown lists with technologies,
        //locations, and price ranges
        public void Populate(List<Location> locations, List<Technology> technologies)
        {
            //populate locations
            Locations = new List<SelectListItem>();
            foreach (Location location in locations)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = location.City + ", " + location.Country,
                    Value = location.LocationID.ToString()
                };
                Locations.Add(item);
            }

            //populate technologies
            Technologies = new List<SelectListItem>();
            foreach (Technology technology in technologies)
            {
                SelectListItem item2 = new SelectListItem
                {
                    Text = technology.Name,
                    Value = technology.TechnologyID.ToString()
                };
                Technologies.Add(item2);
            }

            //populate price ranges
            Prices = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "USD 0 - 4,999",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "USD 5,000 - 9,999",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "USD 10,000 - 14,999",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = "USD 15,000+",
                    Value = "4"
                }
            };
        }

        public List<SelectListItem> Locations { get; set; }

        public List<SelectListItem> Prices { get; set; }

        public int? SelectedLocationId { get; set; }

        public int? SelectedPriceRange { get; set; }

        public int? SelectedTechnologyId { get; set; }

        public List<SelectListItem> Technologies { get; set; }
    }
}
