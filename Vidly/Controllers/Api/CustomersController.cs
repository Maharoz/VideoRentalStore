using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
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
        //Get /api/customers
        public IEnumerable<CustomerDtos> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDtos>);
        }
        //Get /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();
            //return customer;
            return Ok(Mapper.Map<Customer, CustomerDtos>(customer));
        }

        //Post /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDtos customerDto)
        {
            if (!ModelState.IsValid)
            
                return BadRequest();

            var customer = Mapper.Map<CustomerDtos, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" +customer.Id),customerDto);
        }

        //PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDtos customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id ==id);

            if(customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.Found);

            Mapper.Map(customerDto, customerInDb);

            //customerInDb.Name = customerDto.Name;
            //customerInDb.Birthdate = customerDto.Birthdate;
            //customerInDb.IsSubscribeToNewsLetter = customerDto.IsSubscribeToNewsLetter;
            //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;
            _context.SaveChanges();
        }
        //DELETE /api/customer/1
        [HttpDelete]
        public void DelteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.Found);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
