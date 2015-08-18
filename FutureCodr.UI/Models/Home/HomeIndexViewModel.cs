namespace FutureCodr.UI.Models.Home
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            Bootcamps = new List<Bootcamp>();
            SearchParams = new FutureCodr.UI.Models.Home.SearchParams();
        }

        public List<Bootcamp> Bootcamps { get; set; }

        public FutureCodr.UI.Models.Home.SearchParams SearchParams { get; set; }
    }
}
