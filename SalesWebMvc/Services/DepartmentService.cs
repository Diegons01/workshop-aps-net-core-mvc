using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //public List<Department> FindAllA() Método sincrona
        //{
        //    return _context.Department.OrderBy(x => x.Name).ToList();
        //}

        //async e Task antes do list e Async no final do nome do método é opcional mas é uma boa pratica
        public async Task<List<Department>> FindAllAsync() // assíncrona
        {
            //_context.Department.OrderBy(x => x.Name).ToList(); o Tolist() uma chamada sincrona
            //Microsoft.EntityFrameworkCore -> com o uso do Entity podemos usar o ToListAsync()
            //await esperar.
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
