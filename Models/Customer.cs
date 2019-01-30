using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        // Overriding Coventions
        [Required (ErrorMessage = "Please Enter customer's name")] // No nullable string 
        [StringLength(255)] // maximum number of characters 255
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        // Nav property
        public MembershipType MembershipType { get; set; }

        // MembershipTypeId implicity required because it is byte 
        [Display(Name = "Membership Type")]
        //[Required (ErrorMessage = "Please select a Membership type")]
        public byte MembershipTypeId { get; set; } // Entity frameWork treats this property as foreign key

        [Display (Name = "Date of Birth")]
        [Min18YearsIfAmember]
        public DateTime? Birthdate { get; set; }
    }
}