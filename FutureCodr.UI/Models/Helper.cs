namespace FutureCodr.UI.Models
{
    using System;

    public static class Helper
    {
        //generates price string to be displayed in each indiv
        //bootcamp info page
        public static string GetPriceString(int price, string location)
        {
            if (price == 0)
            {
                return "Free";
            }
            switch (location)
            {
                case "USA":
                    return ("USD " + string.Format("{0:n0}", price));

                case "Canada":
                    return ("CAD " + string.Format("{0:n0}", price));

                case "UK":
                    return ("GBP " + string.Format("{0:n0}", price));

                case "Spain":
                    return ("EUR " + string.Format("{0:n0}", price));
            }
            return "Price not found!";
        }
    }
}
