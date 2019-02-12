using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //Detalhes sobre as modificacões de sicrono para assícrona no DepartmentService
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();          
        }

        public async Task InsertAsync(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            //_context.SaveChanges();
           await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            //_context.Seller.where(x => x.Id == id).FirstOrDefault();

            //Sem o uso de using Microsoft.EntityFrameworkCore;
            //return _context.Seller.FirstOrDefault(x => x.Id == id); 
            //Faz um join na tabela Departamento o Include pertence ao frameworkEntity
            //return _context.Seller.Include(x => x.Department).FirstOrDefault(x => x.Id == id);

            //Assíncrono
            return await _context.Seller.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException("Não foi possível deletar o vendedor pois o mesmo tem vendas");
            }           
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            //if (!_context.Seller.Any(x => x.Id == obj.Id))
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
          
        }
    }
}
