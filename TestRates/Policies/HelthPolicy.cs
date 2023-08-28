using System;

namespace TestRating.Policies
{

    public class HealthPolicy : BasePolicy
    {
        public HealthPolicy(string gender, decimal deductible) : base(PolicyType.Health)
        {
            Gender = gender;
            Deductible = deductible;
        }

        #region Health
        public string Gender { get; private set; }
        public decimal Deductible { get; private set; }

        public override decimal ValidatePolicy()
        {
            decimal Rating;

            if (String.IsNullOrEmpty(Gender))
            {
                string msg = "Health policy must specify Gender";
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
            if (Gender == "Male")
            {
                if (Deductible < 500)
                {
                    Rating = 1000m;
                }
                else
                {
                    Rating = 900m;
                }
            }
            else
            {
                if (Deductible < 800)
                {
                    Rating = 1100m;
                }
                else
                {
                    Rating = 1000m;
                }
            }
            return Rating;
        }

        #endregion
    }
}
