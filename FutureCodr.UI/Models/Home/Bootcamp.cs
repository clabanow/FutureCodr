namespace FutureCodr.UI.Models.Home
{
    using System;
    using System.Runtime.CompilerServices;

    public class Bootcamp
    {
        public string GetUrlName()
        {
            return Name.Replace(" ", "-");
        }

        public int BootcampId { get; set; }

        public string Location { get; set; }

        public string LogoLinkUrl { get; set; }

        public string Name { get; set; }
    }
}
