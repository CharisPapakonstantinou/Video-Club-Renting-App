using AutoMapper;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }


        // GET /api/customers/1
        public IHttpActionResult GetCustomer (int Id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (customer == null)
                return NotFound();
                //throw new HttpResponseException(HttpStatusCode.NotFound); // if the given response not found we return the standard http notfound respone


          // var customerToReturn = Mapper.Map<Customer, CustomerDto>(customer);

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        public /*CustomerDto*/ IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(); // it is a class that implements IHttpActionResult
               // throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            //return customerDto;

            // as part of the restful convention we need to return the uri of the newly created resource to the client, uri stands for Unified Resource Identifier
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //[HttpPost]
        //public Customer CreateCustomer(Customer customer)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    _context.Customers.Add(customer);
        //    _context.SaveChanges();

        //    return customer;
        //}

        // PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int Id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (customerInDb == null)
                return NotFound();

            // Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);

            customerDto.Id = Id;
            Mapper.Map(customerDto, customerInDb);

            //customerInDb.Name = customer.Name;
            //customerInDb.MembershipTypeId = customer.MembershipTypeId;
            //customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            //customerInDb.Birthdate = customer.Birthdate;

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomers (int id)
        {
            var customer =_context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok();
        }

        

    }
}
