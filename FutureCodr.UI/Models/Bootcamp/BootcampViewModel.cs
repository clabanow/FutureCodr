namespace FutureCodr.UI.Models.Bootcamp
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BootcampViewModel
    {
        public BootcampViewModel()
        {
            Links = new List<Link>();
            Locations = new List<FutureCodr.UI.Models.Bootcamp.Location>();
            Sessions = new List<Session>();
            Technologies = new List<Technology>();
        }

        public int BootcampID { get; set; }

        public List<Link> Links { get; set; }

        public string Location { get; set; }

        public List<FutureCodr.UI.Models.Bootcamp.Location> Locations { get; set; }

        public string LogoLink { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public List<Session> Sessions { get; set; }

        public List<Technology> Technologies { get; set; }

        public string Url { get; set; }
    }
}
