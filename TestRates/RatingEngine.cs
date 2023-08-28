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

                var policy = JsonConvert.DeserializeObject<BasePolicy>(policyJson, new StringEnumConverter());

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
                Console.WriteLine($"Application failed: {ex.Message}");
            }
        }
    }
}
