namespace FutureCodr.UI.Models.Angular.bootcamps
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AdminBootcampListViewModel
    {
        public AdminBootcampListViewModel()
        {
            Bootcamps = new List<BootcampListAng>();
            Locations = new List<LocationAng>();
            Technologies = new List<TechnologyAng>();
        }

        public List<BootcampListAng> Bootcamps { get; set; }

        public List<LocationAng> Locations { get; set; }

        public List<TechnologyAng> Technologies { get; set; }
    }
}
