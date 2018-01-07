using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorsTest.Core.Colors;
using ColorsTest.Infrastructure.Mappers;
using ColorsTest.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ColorsTest.Infrastructure.Repositories.Colors
{
    public class ColorRepository : IColorRepository
    {
        public ColorRepository(IConfiguration config, IConnectionFactory factory)
        {
            this.Config = config;
            this.Factory = factory;
        }

        private IConfiguration Config { get; }

        public IConnectionFactory Factory { get; }

        public async Task<Color> Get(int id)
        {
            var connectionString = this.Config.GetConnectionString("TechTest");

            using (var connection = this.Factory.Get(connectionString))
            {
                var color = await connection.QuerySingleOrDefaultAsync<ColorEntity>(
                    "SELECT * FROM Colours WHERE ColourId = @id",
                    new { id }
                );

                return color.FromEntity();
            }
        }

        public async Task<IEnumerable<Color>> Get()
        {
            var connectionString = this.Config.GetConnectionString("TechTest");

            using (var connection = this.Factory.Get(connectionString))
            {
                var colors = await connection.QueryAsync<ColorEntity>(
                    "SELECT * FROM Colours"
                );

                return colors.Select(x => x.FromEntity());
            }
        }

        public async Task AddOrUpdate(Color color)
        {
            var connectionString = this.Config.GetConnectionString("TechTest");

            using (var connection = this.Factory.Get(connectionString))
            {
                var existing = await connection.QueryFirstOrDefaultAsync<ColorEntity>(
                    "SELECT * FROM Colours WHERE ColourId = @id",
                    new { id = color.Id }
                );

                var sql = 
                    existing == null
                        ? "INSERT INTO Colours(Name, IsEnabled) VALUES(@Name, @IsEnabled)"
                        : "UPDATE Colours SET IsEnabled = @IsEnabled WHERE ColourId = @ColourId";

                await connection.ExecuteAsync(sql, color);
            }
        }

        public async Task Delete(int id)
        {
            var connectionString = this.Config.GetConnectionString("TechTest");

            using (var connection = this.Factory.Get(connectionString))
            {
                await connection.ExecuteAsync(
                    "DELETE FROM Colours WHERE ColourId = @Id",
                    new { Id = id }
                );
            }
        }

        public async Task<IEnumerable<Color>> GetFavourites()
        {
            var connectionString = this.Config.GetConnectionString("TechTest");

            using (var connection = this.Factory.Get(connectionString))
            {
                var colors = await connection.QueryAsync<ColorEntity>(
                    @"
                        SELECT DISTINCT
                            c.*
                        FROM
                            Colours c
                        LEFT JOIN
                            FavouriteColours fc ON fc.ColourId = c.ColourId
                        WHERE
                            fc.ColourId IS NOT NULL
                    "
                );

                return colors.Select(x => x.FromEntity());
            }
        }
    }
}
