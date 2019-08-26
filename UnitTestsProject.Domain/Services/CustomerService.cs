using Flunt.Notifications;
using System.Linq;
using UnitTestsProject.Domain.Entities;
using UnitTestsProject.Domain.Interfaces;

namespace UnitTestsProject.Domain.Services
{
    public class CustomerService : Notifiable, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void RegisterCustomer(Customer customer)
        {
            if (_customerRepository.CustomerExists(customer.Id))
            {
                AddNotification($"{nameof(Customer)}", "Cliente já existe.");
                return;
            }

            if (!customer.Valid)
            {
                AddNotification(customer.Notifications.First().Property, customer.Notifications.First().Message);
                return;
            }

            _customerRepository.Create(customer);
        }
    }
}
