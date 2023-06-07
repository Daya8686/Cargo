using CargoManagerSystem.Modules;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoManagerSystem.Models
{
    public class CargoOrder
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CargoorderId { get; set; }
        //public CargoOderDetails CargoOderDetails { get; set; }  

        public int CustomerId { get; set; } 

       // public string CustomerName { get; set; } //tried this

        //public  Customer Customer { get; set; } // customer for one to many relation  // Commented after migration


            public int CargoCategoryId { get; set; }
         //public CargoCategory CargoCategory { get; set; }  // Commented after migration



        // public CargoOrderDetails CargoOrderDetails { get; set; }  //tried this 




    }
}
