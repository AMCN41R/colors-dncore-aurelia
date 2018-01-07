using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorsTest.Core;
using ColorsTest.Core.Colors;
using ColorsTest.Core.Repositories.People;
using Microsoft.AspNetCore.Mvc;

namespace ColorsTest.Controllers
{
    [Route("api/people")]
    public class PeopleController : Controller
    {
        public PeopleController(IPersonRepository People)
        {
            this.People = People;
        }

        private IPersonRepository People { get; }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var people = (await 
                this.People
                    .Get())
                    .OrderBy(x => x.FirstName)
                    .ToList() ?? new List<Person>();

            return this.Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var person = await this.People.Get(id);

            return this.Ok(person);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePerson([FromBody] Person person)
        {
            if (person == null)
            {
                return this.BadRequest();
            }

            var existing = await this.People.Get(person.Id);

            if (existing == null)
            {
                return this.NotFound();
            }

            existing.IsAuthorised = person.IsAuthorised;
            existing.IsEnabled = person.IsEnabled;
            existing.IsValid = person.IsValid;

            existing.Colors = person.Colors ?? new List<Color>();

            await this.People.Update(existing);

            return this.Ok();
        }

        [HttpPost("{id}/colors/add/{colorId}")]
        public async Task<IActionResult> AddFavouriteColor(int id, int colorId)
        {
            var person = await this.People.Get(id);

            if (person == null)
            {
                return this.NotFound();
            }

            if (!person.Colors.Select(x => x.Id).Contains(colorId))
            {
                person.Colors.Add(new Color { Id = colorId });
                await this.People.Update(person);
            }

            return this.Ok();
        }

        [HttpPost("{id}/colors/remove/{colorId}")]
        public async Task<IActionResult> RemoveFavouriteColor(int id, int colorId)
        {
            var person = await this.People.Get(id);

            if (person == null)
            {
                return this.NotFound();
            }

            if (person.Colors.Select(x => x.Id).Contains(colorId))
            {
                person.Colors.RemoveWhere(x => x.Id == colorId);
                await this.People.Update(person);
            }

            return this.Ok();
        }
    }
}
