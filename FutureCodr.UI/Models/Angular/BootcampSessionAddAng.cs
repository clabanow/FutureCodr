namespace FutureCodr.UI.Models.Angular
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BootcampSessionAddAng
    {
        public BootcampSessionAddAng()
        {
            Technologies = new List<TechnologyAng>();
            Locations = new List<LocationAng>();
        }

        public string BootcampName { get; set; }

        public int BootcampSessionId { get; set; }

        public List<LocationAng> Locations { get; set; }

        public List<TechnologyAng> Technologies { get; set; }
    }
}
