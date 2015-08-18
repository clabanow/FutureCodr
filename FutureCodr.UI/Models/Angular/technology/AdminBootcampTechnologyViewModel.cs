namespace FutureCodr.UI.Models.Angular.technology
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AdminBootcampTechnologyViewModel
    {
        public AdminBootcampTechnologyViewModel()
        {
            BootcampTechnologies = new List<BootcampTechnologyAng>();
            Bootcamps = new List<BootcampAng>();
            Technologies = new List<TechnologyAng>();
        }

        public List<BootcampAng> Bootcamps { get; set; }

        public List<BootcampTechnologyAng> BootcampTechnologies { get; set; }

        public List<TechnologyAng> Technologies { get; set; }
    }
}
