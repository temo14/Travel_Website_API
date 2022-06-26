using Contracts;
using Entities;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public RepositoryContext Context { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            Context = repositoryContext ?? throw new ArgumentNullException(nameof(repositoryContext));
        }

        public virtual void Create(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            Context?.Set<T>().Update(entity);
        }
    }
}