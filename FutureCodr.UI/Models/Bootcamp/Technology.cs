namespace FutureCodr.UI.Models.Bootcamp
{
    using System;
    using System.Runtime.CompilerServices;

    public class Technology
    {
        public string GetUrl()
        {
            return Name.Replace(" ", "-");
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
