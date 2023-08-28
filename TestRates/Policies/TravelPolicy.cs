using Microsoft.VisualBasic;
using System;

namespace TestRating.Policies
{

    public class TravelPolicy : BasePolicy
    {
        public TravelPolicy(string country, int days) : base(PolicyType.Travel)
        {
            Country = country;
            Days = days;
        }

        #region Travel
        public string Country { get; private set; }
        public int Days { get; private set; }

        public override decimal ValidatePolicy()
        {
            decimal Rating;

            if (Days <= 0)
            {
                string msg = "Travel policy must specify Days.";
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
            if (Days > 180)
            {
                string msg = "Travel policy cannot be more then 180 Days.";
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
            if (String.IsNullOrEmpty(Country))
            {
                string msg = "Travel policy must specify country.";
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
            Rating = Days * 2.5m;
            if (Country == "Italy")
            {
                Rating *= 3;
            }

            return Rating;
        }

        #endregion
    }
}
