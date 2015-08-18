namespace FutureCodr.UI.Models.Angular
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using FutureCodr.UI.Models.Angular.sites;

    public class LinksAng
    {
        public LinksAng()
        {
            Links = new List<LinkAng>();
            Bootcamps = new List<BootcampAng>();
            Sites = new List<SiteAng>();
        }

        public List<BootcampAng> Bootcamps { get; set; }

        public List<LinkAng> Links { get; set; }

        public List<SiteAng> Sites { get; set; }
    }
}
