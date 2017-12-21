using System.Collections.Generic;
using ColorsTest.Core;
using ColorsTest.Core.Colors;
using ColorsTest.Core.Repositories.People;
using ColorsTest.Infrastructure.Repositories.People;

namespace ColorsTest.Infrastructure.Mappers
{
    public static class PersonMapper
    {
        public static Person FromEntity(this PersonEntity entity)
        {
            Guard.AgainstNullArgument(entity, nameof(entity));

            return entity.FromEntity(new List<Color>());
        }

        public static Person FromEntity(this PersonEntity entity, IList<Color> colors)
        {
            Guard.AgainstNullArgument(entity, nameof(entity));

            return new Person
            {
                Id = entity.PersonId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                IsAuthorised = entity.IsAuthorised,
                IsValid = entity.IsValid,
                IsEnabled = entity.IsEnabled,
                Colors = colors ?? new List<Color>()
            };
        }

        public static PersonEntity ToEntity(this Person person)
        {
            Guard.AgainstNullArgument(person, nameof(person));

            return new PersonEntity
            {
                PersonId = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                IsAuthorised = person.IsAuthorised,
                IsValid = person.IsValid,
                IsEnabled = person.IsEnabled
            };
        }
    }
}