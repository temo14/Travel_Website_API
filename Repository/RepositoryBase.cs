using Contracts;
using Entities;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public RepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext ?? throw new ArgumentNullException(nameof(repositoryContext));
        }


        public virtual void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            RepositoryContext?.Set<T>().Update(entity);
        }
    }
}