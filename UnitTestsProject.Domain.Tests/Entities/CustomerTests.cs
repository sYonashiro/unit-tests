using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestsProject.Domain.Entities;
using UnitTestsProject.Domain.Enums;
using UnitTestsProject.Domain.ValueObjects;

namespace UnitTestsProject.Domain.Tests.Entities
{
    [TestClass]
    public class CustomerTests
    {
        private const string VALID_PHONE = "(15) 98111-0000";

        private Guid GetValidId() => 
            Guid.Parse("57a428db-adbb-40a3-b6a5-91d1478e8092");

        private Name GetValidName() => 
            new Name("João", "Silva");

        private Document GetValidDocument() => 
            new Document("78.536.709/0001-01", EDocType.CNPJ);

        private Email GetValidEmail() => 
            new Email("joao@gmail.com");

        private Customer GetValidCustomer() =>
            new Customer(
                GetValidId(), 
                GetValidName(), 
                GetValidDocument(), 
                GetValidEmail(), 
                VALID_PHONE);
        
        [TestMethod]
        public void ShouldBeValidWhenCustomerIsValid()
        {
            var customer = GetValidCustomer();
            Assert.IsTrue(customer.Valid);
        }

        [TestMethod]
        public void ShouldBeInvalidWhenPhoneIsInvalid()
        {
            var customer = new Customer(GetValidId(), GetValidName(), GetValidDocument(), GetValidEmail(), "098788");
            Assert.IsTrue(customer.Invalid);
        }
    }
}
