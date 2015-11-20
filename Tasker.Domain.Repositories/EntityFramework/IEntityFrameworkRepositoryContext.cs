using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Tasker.Domain.Repositories;

namespace Tasker.Domain.Repositories.EntityFramework
{
    /// <summary>
    /// EF上下文接口
    /// </summary>
    public interface IEntityFrameworkRepositoryContext: IRepositoryContext
    {
        /// <summary>
        /// EF Data Context.
        /// </summary>
        DbContext Context { get; }
    }
}
