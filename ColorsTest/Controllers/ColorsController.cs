using System.Threading.Tasks;
using ColorsTest.Core.Colors;
using Microsoft.AspNetCore.Mvc;

namespace ColorsTest.Controllers
{
    [Route("api/colors")]
    public class ColorsController : Controller
    {
        public ColorsController(IColorRepository colors)
        {
            this.Colors = colors;
        }

        public IColorRepository Colors { get; }

        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            var colors = await this.Colors.Get();

            return this.Ok(colors);
        }
    }
}