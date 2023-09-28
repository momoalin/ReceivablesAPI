using TenantAPI.Infrastructure;
using TenantAPI.Models.PrespectiveClients;

namespace TenantAPI.Repositories
{
    public class CustomersRepository : DataRepository<Customer>, ICustomersRepository
    {
        public CustomersRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await GetById(id);
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await GetAll().Include(a => a.Recieveables).ToListAsync();
        }


        public async Task UpdateCustomer(int id, Customer entity)
        {
            await Update(id, entity);
        }
    }
}
