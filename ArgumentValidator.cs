using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable enable

namespace RockPaperScissors
{
    public class ArgumentValidator
    {
        private string[] args;
        private List<string> claims = new List<string>();

        public ArgumentValidator(string[]? args)
        {
            this.args = args ?? new string[0];
        }

        public ReadOnlyCollection<string> Claims => new(claims);

        public bool Validate()
        {
            if (args.Length % 2 == 0)
            {
                claims.Add("Number of names must be odd.");
            }

            if (args.Length < 3)
            {
                claims.Add("There must be at least 3 names.");
            }

            var dublicates = args.GroupBy(arg => arg)
                                 .Where(group => group.Count() > 1)
                                 .Select(dublicate => $"{dublicate.Key} is repeated {dublicate.Count()} times");

            if (dublicates.Count() != 0)
            {
                claims.Add("All the name must be unique:\n" + string.Join(",\n", dublicates) + ".");
            }

            return claims.Count == 0;
        }
    }
}