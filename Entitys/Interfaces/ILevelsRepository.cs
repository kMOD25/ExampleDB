using ModelEntity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entitys.Interfaces
{
    public interface ILevelsRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<Levels>> GetLevelsList();
        Task<Levels> GetLevel(int id);
    }
}
