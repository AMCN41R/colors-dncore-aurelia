using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorsTest.Core.Colors
{
    public class ColorService : IColorService
    {
        public ColorService(IColorRepository colors)
        {
            this.Colors = colors;
        }

        private IColorRepository Colors { get; }

        public async Task AddColor(string name)
        {
            Guard.AgainstNullOrWhitespaceArgument(name, nameof(name));

            var color = new Color
            {
                Name = name.Trim(),
                IsEnabled = true
            };

            await this.Colors.AddOrUpdate(color);
        }

        public async Task<bool> CanAdd(string name)
        {
            Guard.AgainstNullOrWhitespaceArgument(name, nameof(name));

            var colors = await this.Colors.Get();

            return !colors.Select(x => x.Name.ToLower()).Contains(name.ToLower());
        }

        public async Task<bool> CanDelete(int id)
        {
            var favourites = await this.Colors.GetFavourites();

            return !favourites.Select(x => x.Id).Contains(id);
        }

        public Task DeleteColor(int id)
        {
            return this.Colors.Delete(id);
        }

        public Task<Color> GetColor(int id)
        {
            return this.Colors.Get(id);
        }

        public Task<IEnumerable<Color>> GetColors()
        {
            return this.Colors.Get();
        }
    }
}