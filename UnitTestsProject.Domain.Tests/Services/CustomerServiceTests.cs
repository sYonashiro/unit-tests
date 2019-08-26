using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using UnitTestsProject.Domain.Entities;
using UnitTestsProject.Domain.Enums;
using UnitTestsProject.Domain.Interfaces;
using UnitTestsProject.Domain.Services;
using UnitTestsProject.Domain.ValueObjects;

namespace UnitTestsProject.Domain.Tests.Services
{
    [TestClass]
    public class CustomerServiceTests
    {
        private readonly ICustomerRepository _fakeCustomerRepository;
        private readonly CustomerService _customerService;
        
        public CustomerServiceTests()
        {
            _fakeCustomerRepository = Substitute.For<ICustomerRepository>();
            _customerService = new CustomerService(_fakeCustomerRepository);
        }

        private Customer GetValidCustomer()
        {
            var id = Guid.Parse("57a428db-adbb-40a3-b6a5-91d1478e8092");
            var name = new Name("João", "Silva");
            var document = new Document("78.536.709/0001-01", EDocType.CNPJ);
            var email = new Email("joao@gmail.com");
            var phone = "(15) 98111-0000";
            
            return new Customer(id, name, document, email, phone);
        }

        [TestMethod]
        public void ShouldRegisterWhenCustomerIsValid()
        {
            var customer = GetValidCustomer();
            
            _fakeCustomerRepository.CustomerExists(customer.Id).Returns(false);
            _customerService.RegisterCustomer(customer);

            Assert.IsTrue(_customerService.Valid);
        }

        [TestMethod]
        public void ShouldNotRegisterWhenCustomerAlreadyExists()
        {
            var customer = GetValidCustomer();
            
            _fakeCustomerRepository.CustomerExists(customer.Id).Returns(true);
            _customerService.RegisterCustomer(customer);

            Assert.IsTrue(_customerService.Invalid);
        }

        [TestMethod]
        public void ShouldNotRegisterWhenCustomerIsInvalid()
        {
            var id = Guid.Parse("730ef695-ce8a-4c4a-94a0-0c9ba46fbe50");
            var name = new Name("José", "Silva");
            var document = new Document("1203809", EDocType.CNPJ);
            var email = new Email("joao@gmail.com");
            var customer = new Customer(id, name, document, email, phone: "(15) 94777-4444");
            
            _fakeCustomerRepository.CustomerExists(id).Returns(false);
            _customerService.RegisterCustomer(customer);

            Assert.IsTrue(_customerService.Invalid);
        }
    }
}
