using CustomersAPI.Models;
using CustomersAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerRepository _customerRespoistory;
        public CustomersController(ICustomerRepository customerRepository) 
        {
            _customerRespoistory = customerRepository;
        }
        [HttpGet]
        public IActionResult GetAllCustomers() 
        {
            return Ok(_customerRespoistory.GetAllCustomers());
        }
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id) 
        {
            return Ok(_customerRespoistory.GetCustomerById(id));
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer) 
        {
            var isCustomerAdded=_customerRespoistory.AddCustomer(customer);
            if(isCustomerAdded)
            {
                return Ok("Customer Added successfully!!");
            }
            return StatusCode(505);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id,Customer customer)
        {
            var isCustomerUpdated=_customerRespoistory.UpdateCustomer(id, customer);
            if(isCustomerUpdated)
            {
                return Ok("Customer updated Successfully!!");
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var isCustomerDeleted = _customerRespoistory.DeleteCustomer(id);
            if( isCustomerDeleted)
            {
                return Ok("Customer deleted successfully!!1");
            }
            return NotFound();
        }

    }
}
