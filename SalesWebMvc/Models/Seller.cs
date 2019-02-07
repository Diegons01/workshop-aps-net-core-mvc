using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime bithDate { get; set; }
        public double BaseSalary { get; set; }
        public Department department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime bithDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            this.bithDate = bithDate;
            BaseSalary = baseSalary;
            this.department = department;
        }

        public void AddSelses(SalesRecord sales)
        {
            Sales.Add(sales);
        }
        public void RemoveSales(SalesRecord sales)
        {
            Sales.Remove(sales);
        }

        public double TotalSales(DateTime initial, DateTime Final)
        {
            return Sales.Where(x => x.Date >= initial && x.Date <= Final).Sum(x => x.Amount);
        }
    }
}
