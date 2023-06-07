using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoManagerSystem.Modules
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get;set; }



        [Required(ErrorMessage = "Enter First Name!!!..")]
        public string EmployeeFName { get; set; }



        [Required(ErrorMessage ="Enter Last Name!!!...")]
        public string EmployeeLName { get; set; }

        
        [Required(ErrorMessage ="Date of Birth is Mandatory")]
        public DateTime BirthDate { get; set; }



        [Required(ErrorMessage = "Email Can't be empty")]
        [EmailAddress(ErrorMessage = "Email is not Valid")]
        public string EmployeeEmail { get; set; }



        [Required(ErrorMessage ="Street feild can't be left Empty")]
        public string Street { get; set; }


        //[Required(ErrorMessage ="City feild can't be left Empty")]
        //public string CityId  { get; set; }



        [Required(ErrorMessage = "Phone number is mandatory")]
        [Phone(ErrorMessage = "Must be 10 Digits")]
        public string Phone { get; set; }



        [Required(ErrorMessage = "Date of Joining is Mandatory")]
        public DateTime JoiningDate { get; set; }



        [Required(ErrorMessage = "Date of Left is Mandatory")]
        public DateTime DateOfResign { get; set; }


        [Required(ErrorMessage ="Admin Feild can't be Blank")]
        public bool IsAdmin { get; set; }


        
        public int CityId { get;set; }
        // public City City { get; set; } // Commented after migration
    }
}
