using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CargoManagerSystem.Modules
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }


        [Required(ErrorMessage ="City is Mandatory Field")]
            public string CityName { get; set; }


        
            public int StateId { get; set; }
         //public State State { get; set; } // Commented after migration


        // public ICollection<Employee> Employees { get; set; } // Commented after migration

        //public int CustomerId { get; set; } //commented to see
        // public ICollection<Customer> Customers { get; set; } //Cities have many customers // Commented after migration


        //public int CargoorderDetailsId { get; set; }  //tried this
        // public CargoOrderDetails CargoOderDetails { get; set; }  // tried this

    }
}
