using CustomersAPI.Models;

namespace CustomersAPI.Repository
{
    public interface ICustomerRepository
    {
        public IEnumerable<Customer> GetAllCustomers();
        public Customer GetCustomerById(int id);
        public bool AddCustomer(Customer customer);
        public bool UpdateCustomer(int id, Customer customer);
        public bool DeleteCustomer(int id);
    }
}
