using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteAsync(Product product)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            //return await _context.Products.FindAsync(id);
            return await _context.Products.Include(c=>c.Category)
                                          .SingleOrDefaultAsync(p => p.Id == id);
        }

        //public async Task<Product> GetProductCategoryAsync(int? id)
        //{
        //    return await _context.Products.Include(c => c.Category)
        //                                  .SingleOrDefaultAsync(p => p.Id==id);
        //}

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
