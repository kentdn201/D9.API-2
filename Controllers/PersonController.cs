using D9.Models;
using D9.Services;
using Microsoft.AspNetCore.Mvc;

namespace D9.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;

    private readonly IPersonService _personService;

    public PersonController(ILogger<PersonController> logger, IPersonService personService)
    {
        _logger = logger;
        _personService = personService;
    }

    [HttpGet]
    public List<Person> Get()
    {
        return _personService.GetAll();
    }

    [HttpGet("{index:int}")]
    public IActionResult GetOne(int index)
    {
        try
        {
            var person = _personService.GetOne(index);
            return new JsonResult(person);
        }
        catch (IndexOutOfRangeException ex)
        {
            // return NotFound(ex);
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost]
    public Person Create(PersonCreateModel model)
    {
        var person = new Person
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Gender = model.Gender,
            DateOfBirth = model.DateOfBirth,
            BirthPlace = model.BirthPlace
        };
        return _personService.Create(person);
    }

    [HttpPut("{index:int}")]
    public IActionResult Update(int index, PersonUpdateModel model)
    {
        try
        {
            var person = _personService.GetOne(index);

            person.FirstName = model.FirstName;
            person.LastName = model.LastName;
            person.Gender = model.Gender;
            person.BirthPlace = model.BirthPlace;

            _personService.Update(index, person);
            return new JsonResult(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{index:int}")]
    public IActionResult Delete(int index)
    {
        try
        {
            _personService.Delete(index);
            return Ok();
        }
        catch (IndexOutOfRangeException ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("filter-by-name")]
    public List<Person> FilterByName(string keyword)
    {
        var people = _personService.GetAll();
        var result = from person in people
                     where person.FirstName != null && person.FirstName.Contains(keyword, StringComparison.CurrentCulture) ||
                     person.LastName != null && person.LastName.Contains(keyword, StringComparison.CurrentCulture)
                     select person;
        return result.ToList();
    }

    [HttpGet("filter-by-gender")]
    public List<Person> FilterByGender(string gender)
    {
        var people = _personService.GetAll();
        var result = from person in people
                     where person.Gender != null && person.Gender.Equals(gender, StringComparison.CurrentCultureIgnoreCase)
                     select person;
        return result.ToList();
    }

    [HttpGet("filter-by-birthplace")]
    public List<Person> FilterByBirthPlace(string birthPlace)
    {
        var people = _personService.GetAll();
        var result = from person in people
                     where person.BirthPlace != null && person.BirthPlace.Equals(birthPlace, StringComparison.CurrentCultureIgnoreCase)
                     select person;
        return result.ToList();
    }
}
