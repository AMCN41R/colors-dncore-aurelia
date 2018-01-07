using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColorsTest.Core.Colors
{
    public interface IColorRepository
    {
        Task<Color> Get(int id);

        Task<IEnumerable<Color>> Get();

        Task<IEnumerable<Color>> GetFavourites();

        Task AddOrUpdate(Color color);

        Task Delete(int id);
    }
}