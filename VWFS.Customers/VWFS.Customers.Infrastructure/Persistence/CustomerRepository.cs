using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VWFS.Customers.Domain.Entities;
using VWFS.Customers.Domain.Interfaces;
using MongoDB.Driver;

namespace VWFS.Customers.Infrastructure.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _collection;

        public CustomerRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Customer>("customers");
        }

        public async Task AddAsync(Customer customer) =>
            await _collection.InsertOneAsync(customer);

        public async Task DeleteAsync(Guid id) =>
            await _collection.DeleteOneAsync(c => c.Id == id);

        public async Task<IEnumerable<Customer>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<Customer?> GetByDocumentAsync(string document) =>
            await _collection.Find(c => c.Document == document).FirstOrDefaultAsync();

        public async Task<Customer?> GetByIdAsync(Guid id) =>
            await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(Customer customer) =>
            await _collection.ReplaceOneAsync(c => c.Id == customer.Id, customer);
    }
}
