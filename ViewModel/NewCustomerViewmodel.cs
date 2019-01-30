using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class NewCustomerViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; } // IEnumerable does not provide fuctionality like adding an object, removing an object, accessing an object by index .. We use it when we want only to iterate the object
        public Customer Customer { get; set; }
    }
}