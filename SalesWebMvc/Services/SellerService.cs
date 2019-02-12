using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();          
        }
        public void Insert(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }
        public Seller FindById(int id)
        {
            //_context.Seller.where(x => x.Id == id).FirstOrDefault();
            //Sem o uso de using Microsoft.EntityFrameworkCore;
            //return _context.Seller.FirstOrDefault(x => x.Id == id); 

            //Faz um join na tabela Departamento
            return _context.Seller.Include(x => x.Department).FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
          
        }
    }
}
