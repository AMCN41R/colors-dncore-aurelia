using ColorsTest.Core;
using ColorsTest.Core.Colors;
using ColorsTest.Infrastructure.Repositories.Colors;

namespace ColorsTest.Infrastructure.Mappers
{
    public static class ColorMapper
    {
        public static ColorEntity ToEntity(this Color color)
        {
            Guard.AgainstNullArgument(color, nameof(color));

            return new ColorEntity
            {
                ColourId = color.Id,
                Name = color.Name,
                IsEnabled = color.IsEnabled
            };
        }

        public static Color FromEntity(this ColorEntity color)
        {
            Guard.AgainstNullArgument(color, nameof(color));

            return new Color
            {
                Id = color.ColourId,
                Name = color.Name,
                IsEnabled = color.IsEnabled
            };
        }
    }
}