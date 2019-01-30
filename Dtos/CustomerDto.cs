using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        // Overriding Coventions
        [Required] // No nullable string 
        [StringLength(255)] // maximum number of characters 255
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        public byte MembershipTypeId { get; set; } 

       // [Min18YearsIfAmember]
        public DateTime? Birthdate { get; set; }
    }
}