using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(20, MinimumLength=5)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|(A-Za-z]+))$", ErrorMessage = "Please enter atleast 5 characters for name. They should be alphabets only with one space")]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{2}-\d{3} \d{4}$", ErrorMessage = "Contact Number should be in format xx-xxx xxxx")]
        public string Contact { get; set; }
        
        [Required]
        [DisplayAttribute(Name = "National ID")]
        [RegularExpression(@"^\d{8,11}$", ErrorMessage = "National ID should be atleast 8 to maximum 11 digits long")]
        public string NationalID { get; set; }
    }
}