using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorsTest.Core.Colors;
using ColorsTest.Core.Repositories.People;
using ColorsTest.Infrastructure.Mappers;
using ColorsTest.Infrastructure.Repositories.Colors;
using ColorsTest.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ColorsTest.Infrastructure.Repositories.People
{
    public class PersonRepository : IPersonRepository
    {
        public PersonRepository(IConfiguration config, IConnectionFactory factory)
        {
            this.Config = config;
            this.Factory = factory;
        }

        public IConfiguration Config { get; }

        private IConnectionFactory Factory { get; }

        public async Task<Person> Get(int id)
        {
            var connectionString = this.Config.GetConnectionString("TechTest");

            using (var connection = this.Factory.Get(connectionString))
            {
                var person = 
                    await connection.QueryFirstOrDefaultAsync<PersonEntity>(
                        "SELECT * FROM People WHERE PersonId = @id", 
                        new { id }
                    );

                if (person == null)
                {
                    return null;
                }

                var colors = await connection.QueryAsync<Color>(
                    @"                    
                        SELECT 
                            c.ColourId AS Id,
                            c.Name,
                            c.IsEnabled
                        FROM 
                            Colours c 
                        INNER JOIN 
                            FavouriteColours fc ON c.ColourId = fc.ColourId
                        WHERE 
                            fc.PersonId = @id
                    ",
                    new { id }
                );

                var result = person.FromEntity(colors.ToList());

                return result;
            }
        }

        public async Task<IEnumerable<Person>> Get()
        {
            var connectionString = this.Config.GetConnectionString("TechTest");

            using (var connection = this.Factory.Get(connectionString))
            {
                var peopleQuery = await connection.QueryAsync<PeopleColorQueryResult>(
                    @"
                    SELECT
                        p.*,
                        c.ColourId  AS ColorId,
                        c.Name      AS ColorName,
                        c.IsEnabled AS ColorEnabled
                    FROM
                        People p
                    LEFT JOIN
                        FavouriteColours fc on p.PersonId = fc.PersonId
                    LEFT JOIN
                        Colours c ON fc.ColourId = c.ColourId
                    "
                );

                var people =
                    peopleQuery.GroupBy(
                        x => new { x.PersonId, x.FirstName, x.LastName, x.IsAuthorised, x.IsEnabled, x.IsValid },
                        x => new { x.ColorId, x.ColorName, x.ColorEnabled },
                        (key, grp) => new Person
                        {
                            Id = key.PersonId,
                            FirstName = key.FirstName,
                            LastName = key.LastName,
                            IsAuthorised = key.IsAuthorised,
                            IsEnabled = key.IsEnabled,
                            IsValid = key.IsValid,
                            Colors = grp.Select(x => new Color
                            {
                                Id = x.ColorId,
                                Name = x.ColorName,
                                IsEnabled = x.ColorEnabled
                            }).ToList()
                        });

                return people;
            }
        }

        public async Task Update(Person person)
        {
            var entity = person.ToEntity();

            var connectionString = this.Config.GetConnectionString("TechTest");

            using (var connection = this.Factory.Get(connectionString))
            {
                await connection.ExecuteAsync(
                    @"
                    UPDATE People
                    SET 
                        IsAuthorised = @IsAuthorised,
                        IsEnabled = @IsEnabled,
                        IsValid = @IsValid
                    WHERE
                        PersonId = @PersonId;
                    ",
                    entity
                );

                await connection.ExecuteAsync(
                    "DELETE FROM FavouriteColours WHERE PersonId = @PersonId",
                    new { entity.PersonId }
                );

                await connection.ExecuteAsync(
                    "INSERT INTO FavouriteColours(PersonId, ColourId) VALUES(@PersonId, @ColourId)",
                    person.Colors.Select(x => new { PersonId = person.Id, ColourId = x.Id })
                );
            }
        }

        private class PeopleColorQueryResult
        {
            public int PersonId { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public bool IsAuthorised { get; set; }

            public bool IsValid { get; set; }

            public bool IsEnabled { get; set; }

            public int ColorId { get; set; }

            public string ColorName { get; set; }

            public bool ColorEnabled { get; set; }
        }
    }
}
