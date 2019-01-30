using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;


namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context; // we need a DBcontext to access the database

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // DBContext is a disposable object so we need to dispose this object
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New ()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new NewCustomerViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save (Customer customer/*or NewCustomerViewModel viewModel*/)
        {

            if (!ModelState.IsValid)
            {

                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;


            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }


        [AllowAnonymous]
        // outputcache caches the reder html
       // [OutputCache (Duration = 50, Location = System.Web.UI.OutputCacheLocation.Server, VaryByParam = "*")] // this page will be saved to cache for 50 seconds, the second time the user request the page will be sent from the cache
       // [OutputCache (Duration = 0, VaryByParam = "*", NoStore = true)] // this disable caching
        public ActionResult Index()
        {

            //////////////////////  Data caching example ////////////////////////////////////////////////
            
            //if (System.Runtime.Caching.MemoryCache.Default["Genres"] == null)
            //{
            //    System.Runtime.Caching.MemoryCache.Default["Genres"] = _context.Genres.ToList();
            //}

            //var genres = System.Runtime.Caching.MemoryCache.Default["Genres"] as IEnumerable<Genre>;

            ////////////////////////////////////////////////////////////////////////////////////////////////

            if (User.IsInRole(RoleName.CanManageMovies))
                return View();



            return RedirectToAction("Login", "Account");
        }

        public ActionResult Edit(int? Id)
        {

            if (Id == null)
                return new HttpNotFoundResult();

            // because of singleordefault a query will be immediately executed
            Customer customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == Id);
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = membershipTypes,
                Customer = customer
            };

            return View("CustomerForm", viewModel);
        }

        //public ActionResult Edit (int Id)
        //{
        //    var membershipTypes = _context.MembershipTypes.ToList();

        //    var viewModel = new NewCustomerViewModel
        //    {
        //        MembershipTypes = membershipTypes,
        //    };

        //    return View ("CustomerForm", viewModel);
        //}

    }
}