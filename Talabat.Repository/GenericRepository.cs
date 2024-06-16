using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Data;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
           

        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if(typeof(T)==typeof(Product))
            //    return (IReadOnlyList<T>) await _dbContext.Set<Product>().Include(p=>p.Brand).Include(p => p.Category).ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
        }

       
        public async Task<T?> GetByIdAsync(int id)
        {
            //if (typeof(T) == typeof(Product))
            //    return await _dbContext.Set<Product>().Where(p=>p.Id==id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T;
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }


        public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationsEvalutor<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

        public async Task<int> GetCountAync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }

        public async Task AddAsync(T entity)
            => await _dbContext.AddAsync(entity);
        
        public void Update(T entity)
            => _dbContext.Update(entity);

        public void Delete(T entity)
             =>  _dbContext.Remove(entity);
    }
}
