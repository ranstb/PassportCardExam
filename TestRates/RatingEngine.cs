using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using TestRating.Policies;

namespace TestRating
{
    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {
        public decimal Rating { get; set; }
        public void Rate()
        {
            // log start rating
            Console.WriteLine("Starting rate.");
            Console.WriteLine("Loading policy.");

            try
            {
                // load policy - open file policy.json
                string policyJson = File.ReadAllText("policy.json");

                PolicyType pType = GetPolicyType(policyJson);
                
                BasePolicy policy = CreatePolicyObject(pType, policyJson);

                if (Enum.IsDefined(typeof(PolicyType), policy.Type))
                {
                    policy.WritingPolicyType();
                    policy.WriteValidatingPolicy();
                    Rating = policy.ValidatePolicy();
                }
                else
                {
                    Console.WriteLine("Unknown policy type");
                }

                Console.WriteLine("Rating completed.");
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Application error: {ex.Message}");
            }
        }

        private BasePolicy CreatePolicyObject(PolicyType pType, string policyJson)
        {
            if (pType == PolicyType.Travel)
            {
                return JsonConvert.DeserializeObject<TravelPolicy>(policyJson, new StringEnumConverter());
            }
            else if (pType == PolicyType.Health)
            {
                return JsonConvert.DeserializeObject<HealthPolicy>(policyJson, new StringEnumConverter());
            }
            else if (pType == PolicyType.Life)
            {
                return JsonConvert.DeserializeObject<LifePolicy>(policyJson, new StringEnumConverter());
            }

            throw new Exception("Cannot create a policy object");
        }

        private PolicyType GetPolicyType(string policyJson)
        {
            if (policyJson.IndexOf(PolicyType.Travel.ToString(), StringComparison.OrdinalIgnoreCase) != -1)
            {
                return PolicyType.Travel;
            }
            if (policyJson.IndexOf(PolicyType.Health.ToString(), StringComparison.OrdinalIgnoreCase) != -1)
            {
                return PolicyType.Health;
            }
            if (policyJson.IndexOf(PolicyType.Life.ToString(), StringComparison.OrdinalIgnoreCase) != -1)
            {
                return PolicyType.Life;
            }

            throw new Exception("Json file is invalid");
        }
    }
}
