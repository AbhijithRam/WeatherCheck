using System.Collections.Concurrent;
using MyDotnet8Api.Interfaces;
using MyDotnet8Api.Models;

namespace MyDotnet8Api.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly ConcurrentDictionary<int, T> _store = new();
        private int _nextId = 1;

        public Task<IEnumerable<T>> GetAllAsync()
        {
            var items = _store.Values.ToList();
            return Task.FromResult<IEnumerable<T>>(items);
        }

        public Task<T?> GetByIdAsync(int id)
        {
            _store.TryGetValue(id, out var item);
            return Task.FromResult(item);
        }

        public Task AddAsync(T entity)
        {
            if (entity.Id == 0)
            {
                entity.Id = Interlocked.Increment(ref _nextId);
            }
            _store[entity.Id] = entity;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            if (entity.Id == 0)
            {
                throw new ArgumentException("Entity must have an Id to be updated.");
            }
            _store[entity.Id] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _store.TryRemove(id, out _);
            return Task.CompletedTask;
        }
    }
}