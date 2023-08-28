using System;

namespace TestRating.Policies
{

    public class LifePolicy : BasePolicy
    {
        public LifePolicy(bool isSmoker, decimal amount) : base(PolicyType.Life)
        {
            IsSmoker = isSmoker;
            Amount = amount;
        }

        #region Life Insurance
        public bool IsSmoker { get; private set; }
        public decimal Amount { get; private set; }

        public override decimal ValidatePolicy()
        {
            decimal Rating;

            if (DateOfBirth == DateTime.MinValue)
            {
                string msg = "Life policy must include Date of Birth.";
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
            if (DateOfBirth < DateTime.Today.AddYears(-100))
            {
                string msg = "Max eligible age for coverage is 100 years.";
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
            if (Amount == 0)
            {
                string msg = "Life policy must include an Amount.";
                Console.WriteLine(msg);
                throw new Exception(msg);
            }

            int age = DateTime.Today.Year - DateOfBirth.Year;
            if (DateOfBirth.Month == DateTime.Today.Month &&
                DateTime.Today.Day < DateOfBirth.Day ||
                DateTime.Today.Month < DateOfBirth.Month)
            {
                age--;
            }
            decimal baseRate = Amount * age / 200;
            if (IsSmoker)
            {
                Rating = baseRate * 2;
            }
            else
            {
                Rating = baseRate;
            }

            return Rating;
        }

        #endregion
    }
}
