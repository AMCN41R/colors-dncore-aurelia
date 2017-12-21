using System.Collections.Generic;
using System.Linq;
using ColorsTest.Core.Colors;

namespace ColorsTest.Core.Repositories.People
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAuthorised { get; set; }

        public bool IsValid { get; set; }

        public bool IsEnabled { get; set; }

        public string FullName => $"{this.FirstName?.Trim()} {this.LastName?.Trim()}".Trim();

        public bool IsPalindrome => this.FullName.IsPalindrome();

        public IList<Color> Colors { get; set; }
    }
}