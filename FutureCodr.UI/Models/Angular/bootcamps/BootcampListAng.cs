namespace FutureCodr.UI.Models.Angular.bootcamps
{
    using System;
    using System.Runtime.CompilerServices;

    public class BootcampListAng
    {
        public int BootcampID { get; set; }

        public int LengthInWeeks { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public string PriceString { get; set; }

        public string Technology { get; set; }
    }
}
