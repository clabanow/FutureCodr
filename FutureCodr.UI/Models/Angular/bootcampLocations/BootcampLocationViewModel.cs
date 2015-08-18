namespace FutureCodr.UI.Models.Angular.bootcampLocations
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BootcampLocationViewModel
    {
        public BootcampLocationViewModel()
        {
            BootcampLocations = new List<BootcampLocationAng>();
            Bootcamps = new List<BootcampAng>();
            Locations = new List<LocationAng>();
        }

        public List<BootcampLocationAng> BootcampLocations { get; set; }

        public List<BootcampAng> Bootcamps { get; set; }

        public List<LocationAng> Locations { get; set; }
    }
}
