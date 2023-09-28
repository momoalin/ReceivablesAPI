﻿using System.Collections.Generic;
using TenantAPI;
using TenantAPI.DTOs;
using TenantAPI.Models;
using TenantAPI.Models.PrespectiveClients;

public class DataSeeder
{
    private readonly DataContext tenantDbContext;

    public DataSeeder(DataContext employeeDbContext)
    {
        this.tenantDbContext = employeeDbContext;
    }

    public void Seed()
    {
        if (!tenantDbContext.Tenants.Any())
        {
            List<Receiveable> receiveables1 = GenerateReceiveables(5);
            List<Receiveable> receiveables2 = GenerateReceiveables(10);
            List<Receiveable> receiveables3 = GenerateReceiveables(15);
            var tenants = new List<Tenant>()
                {
                        new Tenant()
                        {
                            Customers = new List<Customer>()
                            {
                                new Customer()
                                {
                                    Recieveables = receiveables1
                                },
                                new Customer()
                                {
                                    Recieveables = receiveables2
                                },
                                new Customer()
                                {
                                    Recieveables = receiveables3
                                },
                            }
                        }
                };

            tenantDbContext.Tenants.AddRange(tenants);
            tenantDbContext.SaveChanges();
        }
    }

    private List<Receiveable> GenerateReceiveables(int no)
    {

        Random rnd = new Random();
        rnd.NextDouble();
        List<Receiveable> receiveables = new List<Receiveable>();
        for (int i = 0; i < no; i++)
        {
            decimal paid = (decimal)(rnd.Next(10) + rnd.NextDouble());
            receiveables.Add(new ReceivablesDTO()
            {
                Reference = rnd.Next(5000000).ToString(),
                IssueDate = DateTime.Parse("28/09/2020"),
                DueDate = DateTime.Now.AddMonths(rnd.Next(5)),
                PaidValue = paid,
                OpeningValue = 0,
                DebtorName = "B&Q",
                DebtorReference = "B&Q Reference",
                DeptorCountryCode = "5000",
                CurrencyCode = "GBP",
                DeptorRegistrationNumber = "1",
            }.ToReceiveable());

        }
        return receiveables;
    }
}
