using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTOs.AccountUsersDto
{
    public class AddressDto
    {
        [Required(ErrorMessage ="The First Name is Required")]
        public string FName { get; set; }
        [Required(ErrorMessage ="The Last Name is Required")]
        public string LName { get; set; }
        [Required(ErrorMessage ="The Street Name is Required")]
        public string Street { get; set; }
        [Required(ErrorMessage ="The City Name is Required")]
        public string City { get; set; }
        [Required(ErrorMessage ="The Country Name is Required")]
        public string Country { get; set; }
    }
}
