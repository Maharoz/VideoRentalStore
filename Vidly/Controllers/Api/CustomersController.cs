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
        public CustomerDtos GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            //return customer;
            return Mapper.Map<Customer, CustomerDtos>(customer);
        }

        //Post /api/customers
        [HttpPost]
        public CustomerDtos CreateCustomer(CustomerDtos customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customer = Mapper.Map<CustomerDtos, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return customerDto;
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
