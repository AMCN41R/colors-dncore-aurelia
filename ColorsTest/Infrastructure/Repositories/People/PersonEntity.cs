namespace ColorsTest.Infrastructure.Repositories.People
{
    public class PersonEntity
    {
        public int PersonId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAuthorised { get; set; }

        public bool IsValid { get; set; }

        public bool IsEnabled { get; set; }
    }
}