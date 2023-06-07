using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CargoManagerSystem.Models;

namespace CargoManagerSystem.Modules
{
    public class CargoOrderDetails
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CargoorderDetailsId { get; set; }


       // public int CargoorderId { get; set; }
       // public CargoOrder CargoOrder { get; set; }


        [Required(ErrorMessage ="Date is Mandatory")]
        public DateTime CargoorderDate { get; set; }



        [Required(ErrorMessage ="Amount field Required")]
        public int Amount { get; set; }

        [Required(ErrorMessage ="Quantity is Required")]
        public int Quantity { get; set; }

        public string status { get; set; }
        public string ToCity { get; set; }
        public string FromCity { get; set;  }

       // public City City { get; set; } //tried this

        public int CargoorderId { get; set; }
         // public CargoOrder CargoOrder { get; set; } // Commented after migration


        #region this is written to try the connections but this may be wrong or may be commented to remove the relation
        //////public int CustomerId { get;set; }
        //////public Customer Customer { get; set; }


        // public int CargoCategoryId { get; set; }
        // public CargoCategory CargoCategory { get; set; }
        #endregion

    }
}
