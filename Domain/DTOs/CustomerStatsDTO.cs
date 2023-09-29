using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CustomerStatsDTO
    {
        public int CustomerId { get; set; }
        public decimal Receivables { get; set; }
    }
}
