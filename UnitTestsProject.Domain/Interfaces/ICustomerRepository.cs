using System;
using UnitTestsProject.Domain.Entities;

namespace UnitTestsProject.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        bool CustomerExists(Guid id);
        void Create(Customer customer);
    }
}
