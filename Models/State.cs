using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CargoManagerSystem.Modules
{
    public class State
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
             public int StateId { get; set; }


        [Required(ErrorMessage ="State is Mandatory Field")]
            public string StateName { get; set; }


         //public ICollection<City> Cities { get; set; }  // Commented after migration

    }
}
