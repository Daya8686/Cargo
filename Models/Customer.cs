using CargoManagerSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoManagerSystem.Modules
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }



        [Required(ErrorMessage = "Name is Mandatory")]
        public string CustomerName { get; set; }


        [Required(ErrorMessage="Email Can't be empty")]
        [EmailAddress(ErrorMessage ="Email is not Valid")]
        public string CustomerEmail { get; set;}


        [Required(ErrorMessage ="Phone number is mandatory")]
        [Phone(ErrorMessage ="Must be 10 Digits")]
        public string CustomerPhone { get; set;}



        [Required(ErrorMessage ="Address Field can't be blank")]
        [MinLength(15,ErrorMessage ="Address can't be less then 15 characters ")]
        [MaxLength(60, ErrorMessage = "Address can't be More then 60 characters")]
        public string CustomerAddress { get;set;}



        [Required(ErrorMessage ="Pincode is Requried")]
        public int CustomerZip { get;set;}


        //[Required(ErrorMessage ="State Identity number field can't be left empty")]
        //public string CustomerStateId { get; set; }

        //[Required(ErrorMessage ="State of customer is Mandatory")]
        //public string CustomerState { get; set; }


        //[Required(ErrorMessage ="City of Customer is Mandatory")]
        //public string CustomerCity { get; set; }



        public int CityId { get; set; }
        //public City City { get; set; } // Commented after migration

        //public int CargooderId { get; set; } //commented to see

        //public CargoOrder CardoOrder { get; set; } //tried this wrongly

         // public  ICollection<CargoOrder> CargoOrders { get; set; } //customer has many orders | one to many // Commented after migration

    }
}
