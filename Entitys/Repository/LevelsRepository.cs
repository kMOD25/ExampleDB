using Entitys.Interfaces;
using Entitys.ModelEntity;
using Microsoft.EntityFrameworkCore;
using ModelEntity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entitys.Repository
{
    public class LevelsRepository : ILevelsRepository<Levels>
    {
        private readonly ApplicationContext _db;

        public LevelsRepository()
        {
            _db = new ApplicationContext();
        }
        public async Task<IEnumerable<Levels>> GetLevelsList()
        {
           return await _db.Levels.ToListAsync();
        }

        public async Task<Levels> GetLevel(int id)
        {
            return await _db.Levels.FindAsync(id);           
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Check
    }
}
