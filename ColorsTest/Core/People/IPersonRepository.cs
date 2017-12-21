using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColorsTest.Core.Repositories.People
{
    public interface IPersonRepository
    {
        Task<Person> Get(int id);

        Task<IEnumerable<Person>> Get();

        Task Update(Person person);
    }
}