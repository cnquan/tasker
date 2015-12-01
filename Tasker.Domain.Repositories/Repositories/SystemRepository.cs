using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Repositories;

namespace Tasker.Domain.Repositories.Repositories
{
    public class SystemRepository : EntityFramework.EntityFrameworkRepository<Model.Node>,
                                  ISystemRepository
    {
        public SystemRepository(IRepositoryContext context) : base(context) { }

        public DateTime GetSysTime()
        {
            DateTime dbServerTime = (base.Context as EntityFramework.IEntityFrameworkRepositoryContext)
                                   .Context
                                   .Database
                                   .SqlQuery<DateTime>("SELECT GETDATE() ")
                                   .First();
            return dbServerTime;
        }
    }
}
