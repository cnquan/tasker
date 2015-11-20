using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Repositories;

namespace Tasker.Domain.Services
{
    /// <summary>
    /// 应用层服务类
    /// </summary>
    public abstract class ApplicationService : DisposableObject
    {
        private readonly IRepositoryContext _Context;

        public ApplicationService(IRepositoryContext context)
        {
            this._Context = context;
        }

        protected IRepositoryContext Context { get { return _Context; } }

        protected override void Dispose(bool disposing) { }
    }
}
