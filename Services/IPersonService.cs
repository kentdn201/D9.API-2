using D9.Models;

namespace D9.Services;

public interface IPersonService
{
    List<Person> GetAll();

    Person GetOne(int index);
    Person Create(Person person);
    Person Update(int index, Person person);
    void Delete(int index);
}
