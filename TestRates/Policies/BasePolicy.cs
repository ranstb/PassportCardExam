using System;

namespace TestRating.Policies
{

    public abstract class BasePolicy
    {
        public BasePolicy(PolicyType type)
        {
            Type = type;
        }
        public PolicyType Type { get; private set; }

        #region General Policy Prop
        public string FullName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        #endregion

        public void WritingPolicyType()
        {
            Console.WriteLine($"Rating {Type} policy...");
        }

        public void WriteValidatingPolicy()
        {
            Console.WriteLine("Validating policy.");
        }
        public abstract decimal ValidatePolicy();
    }
}
