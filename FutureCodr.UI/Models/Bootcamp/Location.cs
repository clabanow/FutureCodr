namespace FutureCodr.UI.Models.Bootcamp
{
    using System;
    using System.Runtime.CompilerServices;

    public class Location
    {
        public string GetUrl()
        {
            return City.Replace(" ", "-");
        }

        public string City { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
