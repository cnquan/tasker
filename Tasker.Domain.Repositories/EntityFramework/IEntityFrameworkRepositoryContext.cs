using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Repositories;
using System.Data.Entity;

namespace Tasker.Domain.Repositories.EntityFramework
{
    /// <summary>
    /// EF 上下文
    /// </summary>
    public interface IEntityFrameworkRepositoryContext : IRepositoryContext
    {
        DbContext Context { get; }
    }
}
