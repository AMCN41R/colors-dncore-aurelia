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
    }
}