using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VWFS.Customers.Domain.Entities;

namespace VWFS.Customers.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id);
        Task<Customer?> GetByDocumentAsync(string document); // CPF ou CNPJ
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Guid id);
    }
}
