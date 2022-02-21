using System.ComponentModel.DataAnnotations;
using D9.Models;

namespace D9.Models
{
    public class PersonCreateModel
    {
        [Required, MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(10)]
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? BirthPlace { get; set; }
        public string Gender { get; set; }
    }

    public class PersonUpdateModel
    {
        [Required, MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(10)]
        public string? LastName { get; set; }
        public string? BirthPlace { get; set; }
        public string Gender { get; set; }
    }

    public class Person : PersonCreateModel
    {
        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }

        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName}";
            }
        }
    }
}