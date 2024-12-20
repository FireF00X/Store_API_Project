﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTOs.OrdersDtos
{
    public class OrderAddressDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
