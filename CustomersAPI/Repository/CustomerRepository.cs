using CustomersAPI.Data;
using CustomersAPI.Models;

namespace CustomersAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        CustomerDbContext _dbContext;
        public CustomerRepository(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool AddCustomer(Customer customer)
        {
            if (customer != null)
            {
                _dbContext.customers.Add(customer);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteCustomer(int id)
        {
            var customer=_dbContext.customers.FirstOrDefault(x => x.Id == id);
            if (customer != null)
            {
                _dbContext.customers.Remove(customer);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _dbContext.customers;
        }

        public Customer GetCustomerById(int id)
        {
            var customer= _dbContext.customers.FirstOrDefault(x=>x.Id == id);
            return customer;
        }

        public bool UpdateCustomer(int id, Customer customer)
        {
            var existingCustomer=GetCustomerById(id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Age = customer.Age;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
