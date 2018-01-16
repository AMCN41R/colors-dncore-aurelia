using System.Threading.Tasks;
using ColorsTest.Core.Colors;
using Microsoft.AspNetCore.Mvc;

namespace ColorsTest.Controllers
{
    [Route("api/colors")]
    public class ColorsController : Controller
    {
        public ColorsController(IColorService colors)
        {
            this.Colors = colors;
        }

        private IColorService Colors { get; }

        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            var colors = await this.Colors.GetColors();

            return this.Ok(colors);
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> AddColor(string name)
        {
            var canAdd = await this.Colors.CanAdd(name);

            if (!canAdd)
            {
                return this.BadRequest($"Cannot add duplicate color: {name}");
            }

            await this.Colors.AddColor(name);

            return this.Ok();
        }

        [HttpGet("{id}/can-delete")]
        public async Task<IActionResult> CanDeleteColor(int id)
        {
            var canDelete = await this.Colors.CanDelete(id);

            return this.Ok(canDelete);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            if(!(await this.Colors.CanDelete(id)))
            {
                return this.StatusCode(412); // precondition failed
            }

            await this.Colors.DeleteColor(id);

            return this.Ok();
        }
    }
}