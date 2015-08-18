namespace FutureCodr.UI.Models.Angular.sessions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AdminBootcampSessionListViewModel
    {
        public AdminBootcampSessionListViewModel()
        {
            Sessions = new List<BootcampSessionAng>();
            Bootcamps = new List<BootcampAng>();
        }

        public List<BootcampAng> Bootcamps { get; set; }

        public List<BootcampSessionAng> Sessions { get; set; }
    }
}
