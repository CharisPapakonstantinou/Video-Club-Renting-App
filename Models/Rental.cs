using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Rental
    {
        public int Id { get; set; }

        // nav property
        public Customer Customer { get; set; }

        //nav property
        public Movie Movie { get; set; }

        public DateTime DateRended { get; set; }

        public DateTime? DateReturned { get; set; }
    }
}