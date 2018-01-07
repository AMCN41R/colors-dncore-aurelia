using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColorsTest.Core.Colors
{
    public interface IColorService
    {
        Task<Color> GetColor(int id);

        Task<IEnumerable<Color>> GetColors();

        Task<bool> CanAdd(string name);

        Task AddColor(string name);

        Task<bool> CanDelete(int id);
        
        Task DeleteColor(int id);
    }
}
