using Domain.Interface.Generic;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infra.Repository.Generic
{
    public class RepositoryGeneric<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<Contexto> _dbContextOptios;
        public RepositoryGeneric()
        {
            _dbContextOptios = new DbContextOptions<Contexto>();
        }
        public async Task Add(T objeto)
        {
           using(var data = new Contexto(_dbContextOptios))
            {
                await data.Set<T>().AddAsync(objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T objeto)
        {
            using (var data = new Contexto(_dbContextOptios))
            {
                 data.Set<T>().Remove(objeto);
                await data.SaveChangesAsync();
            }
        }
        public async Task<T> GetEntityById(int Id)
        {
            using (var data = new Contexto(_dbContextOptios))
            {
                return await data.Set<T>().FindAsync(Id);
               
            }
        }

        public async Task<List<T>> List()
        {
            using (var data = new Contexto(_dbContextOptios))
            {
                return await data.Set<T>().ToListAsync();
                
            }
        }

        public async Task Update(T objeto)
        {
            using (var data = new Contexto(_dbContextOptios))
            {
                 data.Set<T>().Update(objeto);
                await data.SaveChangesAsync();
            }
        }
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
