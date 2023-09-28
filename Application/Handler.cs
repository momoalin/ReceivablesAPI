using Application.Interfaces;
using Domain.DTOs;

namespace Application;
public class Handler : IReceivablesService
{
    public async Task<Domain.Entities.Customer?> HandlePost(int id, List<ReceivablesDTO> receivables, ICustomersRepository repo)
    {
        Domain.Entities.Customer? customer = await repo.GetCustomerById(id);
        if (customer is null)
            return null;

        customer.Recieveables?.AddRange(receivables.Select(r => r.ToReceiveable()));

        await repo.Update(customer);
        return customer;
    }

    public async Task<object> GetClosedInvoices(ICustomersRepository repo)
    {
        var customers = (await repo.GetCustomers()).ToList();
        var openAmount = customers.Where(w => w.Recieveables is not null && w.Recieveables.Any())
        .Select(a => new { customer_id = a.Id, receivables = a.Recieveables.Where(r => r.ClosedDate is not null).Sum(b => b.PaidValue) });
        return openAmount;
    }

    public async Task<object> GetOpenInvoices(ICustomersRepository repo)
    {
        var customers = (await repo.GetCustomers()).ToList();
        var openAmount = customers.Where(w => w.Recieveables is not null && w.Recieveables.Any())
        .Select(a => new { customer_id = a.Id, receivables = a.Recieveables.Where(r => r.ClosedDate is null).Sum(b => b.PaidValue) });
        return openAmount;
    }
}
public interface IReceivablesService
{
    public Task<Domain.Entities.Customer?> HandlePost(int id, List<ReceivablesDTO> receivables, ICustomersRepository repo);


    public Task<object> GetClosedInvoices(ICustomersRepository repo);
    

    public Task<object> GetOpenInvoices(ICustomersRepository repo);
    
}