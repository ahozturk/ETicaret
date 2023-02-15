using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDbContext _context;

        public WriteRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }
        public async Task<bool> AddRangeAsync(List<T> data)
        {
            await Table.AddRangeAsync(data);
            return true;

        }
        public bool Remove(T model)
        {
            var entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }
        public async Task<bool> RemoveAsync(string id)
        {
            var data = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            return Remove(data);
        }
        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }
        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }
        public async Task<int> SaveChanges()
            => await _context.SaveChangesAsync();

        
    }
}
